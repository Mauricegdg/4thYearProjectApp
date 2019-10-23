using Newtonsoft.Json;
using Prism.Navigation;
using ShopBasket.Models;
using ShopBasket.View.DetailViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShopBasket.ViewModels
{
   public class SeachedProducts : INotifyPropertyChanged
    {
        ObservableCollection<StoreDetailModel> _prodList;
        List<StoreDetailModel> pre_prodList = new List<StoreDetailModel>();
        
        public ObservableCollection<StoreDetailModel> PList
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
            string StoreID = Preferences.Get("Store_IDs", "");
            string[] StoreIDs = StoreID.Split(',');

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var Currentlocation = await Geolocation.GetLocationAsync(request);

            //var Url = "http://shopbasket.azurewebsites.net/api/search";                 //Used When Deploying API
            //var Url = "http://3d05b49d.ngrok.io/api/search";
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
                            var prodInfo = JsonConvert.DeserializeObject<List<StoreDetailModel>>(content);  //Extact data to list of products

                                foreach (var prod in prodInfo)
                                 {
                                     

                                     for (int i = 0; i < StoreIDs.Length - 1; i++)
                                     {
                                              if (prod.StoreID == i + 1)
                                              {
                                                 if (pre_prodList.Count == 0)
                                                 {
                                                      pre_prodList.Add(prod);
                                                 }
                                                 else
                                                 {
                                                        bool duplicate = false;
                                                        for (int j = 0; j < pre_prodList.Count; j++)
                                                          {
                                                             if (prod.Barcode == pre_prodList[j].Barcode)                  // Loops through temp list and elinates duplicate products
                                                                {
                                                                    duplicate = true;
                                                                      break;
                                                                 }

                                                           }


                                                       if (duplicate == false)
                                                        {
                                                            //MemoryStream ms = new MemoryStream(prodInfo[k].ProdImg);
                                                             //        //Image img = new Image();
                                                           //        //img.Source = ImageSource.FromStream(() => ms);
                    
                                                            //        //prodInfo[k].ProdImage = img;
                                                               pre_prodList.Add(prod);
                                                        }
                                                 }

                                              }
                                     }

                                       

                                        
                                 }
                       
                         
                                  PList = new ObservableCollection<StoreDetailModel>(pre_prodList);

                                    for (int q = 0; q < PList.Count; q++)
                                     {
                                             MemoryStream ms = new MemoryStream(PList[q].ProdImg);
                                            PList[q].ProdImage = ImageSource.FromStream(() => ms);
                                     }
                          

                        }


                    }

                    else
                    {
                        Debug.WriteLine("An error occured while loading data"); // Sever ERROR 

                    }

            }
            

        }
           

   }
 }

