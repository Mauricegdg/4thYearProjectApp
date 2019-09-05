using Newtonsoft.Json;
using ShopBasket.ViewModels.sp_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace ShopBasket.Models
{
    public class StoresInRange : INotifyPropertyChanged
    {
        ObservableCollection<StoreLocations> _storeList;

        public ObservableCollection<StoreLocations> StoreList
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

        public StoresInRange()
        {
            
        }


        public async void SetStoresInRange()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var Currentlocation = await Geolocation.GetLocationAsync(request);
            string StoreIDs = "";
            Preferences.Set("Store_IDs", StoreIDs);
            


            //var Url = "http://shopbasket.azurewebsites.net/api/storeLocations";
            var Url = "http://10.0.2.2:5000/api/storeLocations";

            HttpClient httpClient = new HttpClient();


            var response = await httpClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {

                var content2 = await response.Content.ReadAsStringAsync();
                if (content2 == "")
                {
                    Preferences.Set("Store_IDs", "1,"); //for mean time*********
                    //debug code MISSING
                }
                else
                {
                    var StoreInfo = JsonConvert.DeserializeObject<List<StoreLocations>>(content2);
                    StoreList = new ObservableCollection<StoreLocations>(StoreInfo);

                    foreach (var Store in StoreList)
                    {
                        var storeLocation = new Location(double.Parse(Store.Latitude), double.Parse(Store.Longitude));
                        var testLocation = new Location(-33.96842050869081, 25.62738453084694); // test****

                        double distance = Math.Round(testLocation.CalculateDistance(storeLocation, DistanceUnits.Kilometers), 2);

                        if (distance <= double.Parse(Preferences.Get("Store_Range", "15")))
                        {
                            StoreIDs += Store.StoreID.ToString() + ",";
                        }
                    }

                    if (StoreIDs != "")
                    {
                        Preferences.Set("Store_IDs", StoreIDs);
                    }
                    else
                    {
                        Preferences.Set("Store_IDs", "");
                    }
                    

                }



            }
            else
            {
                Preferences.Set("Store_IDs", "1,"); //for mean time*********
                //Debug Code Missing
            }

        }
    }
}
