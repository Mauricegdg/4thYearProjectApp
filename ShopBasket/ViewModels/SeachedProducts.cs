using Newtonsoft.Json;
using ShopBasket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ShopBasket.ViewModels
{
   public class SeachedProducts
    {
        ObservableCollection<ProductListModelDisplay> _prodList;


        public ObservableCollection<ProductListModelDisplay> PList
        {
            get
            {
                return _prodList;
            }

            set
            {
                _prodList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SeachedProducts(string Search)
        {
            GetSearchedProducts(Search);
        }




        public async void GetSearchedProducts(string search)
        {
            //var Url = "ttp://shopbasket.azurewebsites.net/api/search";                 //Used When Deploying API
            var Url = "http://10.0.2.2:5000/api/search";                                  //used while local hosting API
            HttpClient httpClient = new HttpClient();
            //Temp List

            if (search != "")                                                                       //StoredID is string that gets store ID's from prefrences                   
            {
                

                
                    var response = await httpClient.GetAsync(Url + "/" + search);                //Gets response from API

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();                     //Extact data from response 
                        if (content == "")
                        {
                            // DO NOTHING!!!!!!!!!!!
                        }
                        else
                        {
                            var prodInfo = JsonConvert.DeserializeObject<ObservableCollection<ProductListModelDisplay>>(content);  //Extact data to list of products

                            for (int k = 0; k < prodInfo.Count; k++)                                    // Loop through List of products given from API         
                            {
                                if (PList == null)
                                {
                                    PList = prodInfo;

                                    for (int p = 0; p < prodInfo.Count; p++)
                                    {

                                        MemoryStream ms = new MemoryStream(prodInfo[p].ProdImg);
                                        Image image = new Image();
                                        image.Source = ImageSource.FromStream(() => ms);
                                        PList[p].ProdImage = image;
                                    }




                                }

                                else
                                {
                                    bool duplicate = false;
                                    for (int j = 0; j < PList.Count; j++)
                                    {
                                        if (prodInfo[k].Barcode == PList[j].Barcode)                  // Loops through temp list and elinates duplicate products
                                        {
                                            duplicate = true;
                                            break;
                                        }

                                    }
                                    if (duplicate == false)
                                    {
                                        MemoryStream ms = new MemoryStream(prodInfo[k].ProdImg);
                                        Image img = new Image();
                                        img.Source = ImageSource.FromStream(() => ms);

                                        prodInfo[k].ProdImage = img;
                                        PList.Add(prodInfo[k]);
                                    }
                                }
                            }
                            //foreach (var Product in ProdList)
                            // {
                            //  MemoryStream ms = new MemoryStream(Product.ProdImg);
                            //  Product.ProdImg = ImageSource.FromStream(() => ms);
                            //  }
                            //Assign temp list to Displayed list.
                        }

                    }

                    else
                    {
                        Debug.WriteLine("An error occured while loading data"); // Sever ERROR 

                    }
                }
                // ProdList = new ObservableCollection<ProductListModel>(preProdList);

            }
           

        }
 }

