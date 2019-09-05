using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.Models.sp_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace ShopBasket.ViewModels
{
    
    public class ProducListViewModel : INotifyPropertyChanged
    {
        string StoreID = Preferences.Get("Store_IDs", "");

        ObservableCollection<ProductListModel>_prodList;

        public ObservableCollection<ProductListModel> ProdList
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

            //var Url = "ttp://shopbasket.azurewebsites.net/api/prodOnSpl";
            var Url = "http://10.0.2.2:5000/api/ProdOnSpl";
            HttpClient httpClient = new HttpClient();
            var preProdList = new List<ProductListModel>();
            
            if (StoreID != "")
            {
                string[] StoreIDs = StoreID.Split(',');
               

                for (int i = 0; i < StoreIDs.Length-1; i++)
                {
                    var response = await httpClient.GetAsync(Url + "/" + StoreIDs[i]);

                    if (response.IsSuccessStatusCode)
                    {

                        var content2 = await response.Content.ReadAsStringAsync();
                        if (content2 == "")
                        {
                            // DO NOTHING!!!!!!!!!!!
                        }
                        else
                        {
                            
                            var prodInfo = JsonConvert.DeserializeObject<ObservableCollection<ProductListModel>>(content2);

                            for (int k = 0; k < prodInfo.Count ; k++)
                            {
                                if (preProdList.Count == 0)
                                {
                                    preProdList.Add(prodInfo[k]);

                                }
                                else
                                {
                                    for (int j = 0; j < preProdList.Count; j++)
                                    {
                                        if (prodInfo[k].Barcode == preProdList[j].Barcode)
                                        {
                                            preProdList.RemoveAt(j);
                                            preProdList.Add(prodInfo[k]);

                                        }
                                        else
                                        {
                                            preProdList.Add(prodInfo[k]);

                                        }
                                    }
                                }
                                
                            }

                            ProdList = new ObservableCollection<ProductListModel>(preProdList);

                        }

                    }

                    else
                    {
                        Debug.WriteLine("An error occured while loading data"); // Sever ERROR 
                        
                    }
                }

            }
            else
            {
                    // Debug  message no stores found.
            }
            
        }

    }
}
