using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class ProductDetails : ContentPage
    {
        string bcode = "";
        public ProductDetails(string Name, byte[] image, string Description, string Barcode)
        {
            InitializeComponent();
            ProdName.Text = Name;

            MemoryStream ms = new MemoryStream(image);
            ProdImage.Source = ImageSource.FromStream(() => ms);

            ProdDescription.Text = Description;

            ProductListModel productListModel = new ProductListModel();
            productListModel.Barcode = Barcode;
            bcode = Barcode;

            BindingContext = new StoreDetailViewModel(productListModel);
        }

        public ProductDetails()
        {

        }

        private async void AddToCart_Clicked(object sender, EventArgs e)
        {
            int qty = 0;
            qty = Convert.ToInt32(numericUpDown.Value);

            var shopinglistInfo = new ShoppingListInfo()
            {
                UserName = Preferences.Get("UserName", ""),
                Barcode = bcode,
                Qty = qty

            };

            var Url = "http://10.0.2.2:5000/api/ShopList";
            //var Url = "http://3d05b49d.ngrok.io/api/ShopList";
            //var Url = "http://shopbasket.azurewebsites.net/api/ShopList";

            HttpClient httpClient = new HttpClient();

            var jsonObject = JsonConvert.SerializeObject(shopinglistInfo);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(Url, content);

            if (response.IsSuccessStatusCode == true)
            {

                await DisplayAlert("Successfully", "This Item has been added to your Basket", "OK");

            }
            else
            {
                await DisplayAlert("UnSuccessfully", "There seems to be an error with adding this Item.", "OK");
            }


        }
    }
}