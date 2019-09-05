using ShopBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopBasket.View.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        string finalRange = Preferences.Get("Store_Range", "15");


        public Settings()
        {
            InitializeComponent();
            StoreRangePicker.Title = "--Select Store Range--";

            var range = new List<string>();
            range.Add("15 Km");
            range.Add("30 Km");
            range.Add("45 Km");




            StoreRangePicker.ItemsSource = range;

            

            
            

        }

        private void StoreRangePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
           var SelectedRange = StoreRangePicker.SelectedItem;

            switch (SelectedRange)
            {
                case "15 Km":
                    finalRange = "15";         //Preffrence Code
                    break;
                case "30 Km":
                    finalRange = "30";         //Preffrence Code
                    break;
                case "45 Km":
                    finalRange = "45";         //Preffrence Code
                    break;
            }

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var action = await DisplayAlert("Settings","Do you want to save settings?","Yes","Cancel");
            if (action)
            {
                Preferences.Set("Store_Range", finalRange);
                StoresInRange SETstoreRanges = new StoresInRange();
                SETstoreRanges.SetStoresInRange();
            }
           
        }

       
    }
}