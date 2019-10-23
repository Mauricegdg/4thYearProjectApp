using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopBasket.View.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basket : ContentPage
    {
        string username = Preferences.Get("UserName", "");
        public Basket()
        {
            InitializeComponent();
            BindingContext = new BasketDetails(username);
        }

        private async void TapGestureRecognizer_Tapped_Delete(object sender, EventArgs e)
        {
            bool  mayDelete = await DisplayAlert("Confirm", "Are you sure you want to delete the entire item list?", "Yes","No");

            if (mayDelete == true)
            {
                 

                 var Url = "http://10.0.2.2:5000/api/ShopList";
                //var Url = "http://3d05b49d.ngrok.io/api/ShopList";
                //var Url = "http://shopbasket.azurewebsites.net/api/ShopList";

                HttpClient httpClient = new HttpClient();

                 HttpResponseMessage response = await httpClient.DeleteAsync(Url +"/"+username);

                 if (response.IsSuccessStatusCode == true)
                 {
                       await DisplayAlert("Successfully", "All Items has been deleted from your basket", "OK");
                 }
                 else
                 {
                       await DisplayAlert("UnSuccessfully", "There seems to be an error with deleting your Basket List.", "OK");
                 }
            }
            

           
        }
        private async void TapGestureRecognizer_Tapped_Edit(object sender, EventArgs e)
        {
            bool mayEdit = await DisplayAlert("Confirm", "Are you sure you want to edit your basket?", "Yes", "No");

            if (mayEdit == true)
            {
                
                await Navigation.PushAsync(new BasketItems(username));
            }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as BasketStores;
            await Navigation.PushAsync(new UserStoreItems(username,details.StoreID,details.Store_Name));
        }
    }
}