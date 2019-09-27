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

        public async void SetStoresInRange()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);           // Request the location
            var Currentlocation = await Geolocation.GetLocationAsync(request);          //Emulator does not give current location
            string StoreIDs = "";
            Preferences.Set("Store_IDs", StoreIDs);                                     //Sets the stores found in range to 0 because this methos is only called when user opens application

            //var Url = "http://shopbasket.azurewebsites.net/api/storeLocations";       //Used when API is deployed.
            var Url = "http://10.0.2.2:5000/api/storeLocations";                        //Used for development testing with hosing locally

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(Url);                              //Gets response from API

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content == "")
                {
                    Preferences.Set("Store_IDs", "1,"); //for mean time********* 
                    //debug code MISSING
                }
                else
                {
                    var StoreInfo = JsonConvert.DeserializeObject<List<StoreLocations>>(content);       //Add content from API call to list of store information
                  
                    foreach (var Store in StoreInfo)                                                  //Loop through list of stores.
                    {
                        var storeLocation = new Location(double.Parse(Store.Latitude), double.Parse(Store.Longitude));
                        var testLocation = new Location(-33.96842050869081, 25.62738453084694);                                      // test**** Emulator does not give current location

                        double distance = Math.Round(testLocation.CalculateDistance(storeLocation, DistanceUnits.Kilometers), 2);      //Calculating distance as crow flies

                        if (distance <= double.Parse(Preferences.Get("Store_Range", "15")))                                           //Check if store is in selected range the user has set in settings
                        {
                            StoreIDs += Store.StoreID.ToString() + ",";                                                               //Add store ID to string
                        }
                    }

                    if (StoreIDs != "")
                    {
                        Preferences.Set("Store_IDs", StoreIDs);                    //check if sting is not empty and add the string to preferences.
                    }
                    else
                    {
                        Preferences.Set("Store_IDs", "");                          //insert empty string to prefrences
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
