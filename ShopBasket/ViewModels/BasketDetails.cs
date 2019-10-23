using Newtonsoft.Json;
using ShopBasket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace ShopBasket.ViewModels
{
    public class BasketDetails : INotifyPropertyChanged
    {
        string StoreID = Preferences.Get("Store_IDs", "");
        float totalPrice = 999999999999;
        

        ObservableCollection<BasketStores> _storeList;
        List<BasketStores> prestoreList = new List<BasketStores>();

        public string _lowestTotal;

        public string LowestTotal
        {
            get
            {
                return _lowestTotal;
            }

            set
            {
                _lowestTotal = value;
                onPropertyChanged();
            }
        }

        public string totalItems;

        public string TotalItems
        {
            get
            {
                return totalItems;
            }

            set
            {
                totalItems = value;
                onPropertyChanged();
            }
        }

        public ObservableCollection<BasketStores> StoreList
        {
            get
            {
                return _storeList;
            }

            set
            {
                _storeList = value;
                onPropertyChanged();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void onPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BasketDetails(string username)
        {
            getTotalPrice(username, StoreID);
            getTotalItems(username);
        }

        public async void getTotalItems(string username)
        {
            if (username == "")
            {
                //error code
            }
            else
            {
                var Url = "http://10.0.2.2:5000/api/BasketItems";
                //var Url = "http://3d05b49d.ngrok.io/api/BasketItems";
                //var Url = "http://shopbasket.azurewebsites.net/api/BasketItems";

                HttpClient httpClient = new HttpClient();



                var response = await httpClient.GetAsync(Url + "/" + username);                          //  +"/"+username+"/"+StoreIDs[i]);

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    if (content == "")
                    {
                        TotalItems = "0";
                    }
                    else
                    {
                        var info = JsonConvert.DeserializeObject<int>(content);

                        TotalItems = info.ToString();

                    }
                }
            }
        }

        public async void getTotalPrice(string username, string StoreID)
        {
            string[] StoreIDs = StoreID.Split(',');
            float tempTotal = 0;

           

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var Currentlocation = await Geolocation.GetLocationAsync(request);

            var Url = "http://10.0.2.2:5000/api/Basket";
            //var Url = "http://3d05b49d.ngrok.io/api/Basket";
            //var Url = "http://shopbasket.azurewebsites.net/api/Basket";

            HttpClient httpClient = new HttpClient();

           

            var response = await httpClient.GetAsync(Url +"/"+username);                          //  +"/"+username+"/"+StoreIDs[i]);

            if (response.IsSuccessStatusCode)
            {

                var content2 = await response.Content.ReadAsStringAsync();
                if (content2 == "")
                {

                }
                else
                {
                    var StoreInfo = JsonConvert.DeserializeObject<List<BasketStores>>(content2);
                    if (StoreInfo.Count == 0)
                    {
                        //error
                    }
                    else
                    {
                        foreach (var store in StoreInfo)
                        {
                            for (int i = 0; i < StoreIDs.Length - 1; i++)
                            {
                                if (store.StoreID == int.Parse(StoreIDs[i]))
                                {
                                    prestoreList.Add(StoreInfo[i]);

                                }
                            }
                        }

                    }
                }





                StoreList = new ObservableCollection<BasketStores>(prestoreList);

                string storeName = "";
                string tempStoreName = "";

                foreach (var Store in StoreList)
                {
                    tempTotal = Store.Total_Price;
                    tempStoreName = Store.Store_Name;
                    var storeLocation = new Location(double.Parse(Store.Latitude), double.Parse(Store.longitude));
                    var testLocation = new Location(-33.96842050869081, 25.62738453084694); // test****

                    double distance = Math.Round(testLocation.CalculateDistance(storeLocation, DistanceUnits.Kilometers), 2);

                    Store.TotalPrice = "R " + tempTotal.ToString("n2");
                    Store.Distance = distance.ToString("n2") + " Km";

                    if (tempTotal < totalPrice)
                    {
                        totalPrice = tempTotal;
                        storeName = tempStoreName;
                    }



                }
                if (totalPrice == 999999999999)
                {
                    LowestTotal = "R0.00";
                }
                else
                {
                    LowestTotal = "R" + totalPrice.ToString("n2") + " At " + storeName;
                }
                

                //prestoreList = new List<BasketStores>(StoreList);

                //prestoreList.Sort();
            }
            else
            {
                //error
            }
        }

    }
}
