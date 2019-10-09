using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.Models.sp_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShopBasket.ViewModels
{
    
    public class ProducListViewModel : INotifyPropertyChanged
    {
        string StoreID = Preferences.Get("Store_IDs", "");

        ObservableCollection<ProductListModelDisplay>_prodList;
       

        public ObservableCollection<ProductListModelDisplay> ProdList
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

        public ProducListViewModel()
        {

           GetProductsOnSpecial(StoreID);

        }




        public async void GetProductsOnSpecial(string StoreID)
        {
            //var Url = "ttp://shopbasket.azurewebsites.net/api/prodOnSpl";                 //Used When Deploying API
            var Url = "http://10.0.2.2:5000/api/ProdOnSpl";                                  //used while local hosting API
            HttpClient httpClient = new HttpClient();
                                                  //Temp List
            
            if (StoreID != "")                                                                       //StoredID is string that gets store ID's from prefrences                   
            {
                string[] StoreIDs = StoreID.Split(',');                                                 
               

                for (int i = 0; i < StoreIDs.Length-1; i++)                                           //Loop for each store ID in StoreIDs array
                {
                    var response = await httpClient.GetAsync(Url + "/" + StoreIDs[i+1]);                //Gets response from API

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

                            for (int k = 0; k < prodInfo.Count ; k++)                                    // Loop through List of products given from API         
                            {
                                if (ProdList == null)                                              
                                {
                                        ProdList = prodInfo;

                                    for (int p = 0; p < prodInfo.Count; p++)
                                    {

                                        MemoryStream ms = new MemoryStream(prodInfo[p].ProdImg);
                                        Image image = new Image();
                                        image.Source = ImageSource.FromStream(() => ms);
                                        
                                        ProdList[p].ProdImage = image;
                                    }
                                    
                                        

                                   
                                }

                                else
                                {
                                    bool duplicate = false;
                                    for (int j = 0; j < ProdList.Count; j++)                            
                                    {
                                        if (prodInfo[k].Barcode == ProdList[j].Barcode)                  // Loops through temp list and elinates duplicate products
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
                                        ProdList.Add(prodInfo[k]);
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
            else
            {
                    // Debug  message no stores found.
            }
            
        }

        public List<ProductListModelDisplay> GetSearchedProducts(string keyword)
        {
            if (_prodList != null)
            {
                var suggestions = _prodList.Where(p => p.ProdName.ToLower().Contains(keyword.ToLower()));
                List<ProductListModelDisplay> suggList = suggestions.ToList<ProductListModelDisplay>();
                return suggList;
            }
            else
            {
                return null;
            }

            
        }

    }
}
