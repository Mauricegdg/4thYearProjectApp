using Java.Lang;
using Newtonsoft.Json;
using ShopBasket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Math = System.Math;

namespace ShopBasket.ViewModels
{


    public class StoreDetailViewModel : INotifyPropertyChanged
    {
        ObservableCollection<StoreDetailModel> _storeList;

        public ObservableCollection<StoreDetailModel> StoreList
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

        public StoreDetailViewModel(ProductListModel productListModel)
        {
            GetStoreDetails(productListModel);
        }


        public async void GetStoreDetails(ProductListModel productListModel)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var Currentlocation = await Geolocation.GetLocationAsync(request);

            //StoreList = new List<StoreDetailModel>();


            //var Url = "http://shopbasket.azurewebsites.net/api/Search";
            var Url = "http://10.0.2.2:5000/api/Search";

            HttpClient httpClient = new HttpClient();



            //var user = new User();

            //var jsonObject = JsonConvert.SerializeObject(productListModel);
            //var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.GetAsync(Url + "/" + productListModel.Barcode);

            if (response.IsSuccessStatusCode)
            {

                var content2 = await response.Content.ReadAsStringAsync();
                if (content2 == "")
                {

                }
                else
                {
                    var StoreInfo = JsonConvert.DeserializeObject<List<StoreDetailModel>>(content2);
                    StoreList = new ObservableCollection<StoreDetailModel>(StoreInfo);

                    foreach (var Store in StoreList)
                    {
                        var storeLocation = new Location(double.Parse(Store.Latitude), double.Parse(Store.longitude));
                        var testLocation = new Location(-33.96842050869081, 25.62738453084694); // test****

                        double distance = Math.Round(testLocation.CalculateDistance(storeLocation, DistanceUnits.Kilometers),2);

                        Store.Distance = distance.ToString() +" Km";
                    }


                }

                


                //var user = new User();
                //user = jsonObject.

            }
            else
            {
                Debug.WriteLine("An error occured while loading data");
            }

        }
    }
}
