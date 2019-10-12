using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace ShopBasket.View.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainSearch : ContentPage
    {
        public string barcodeScan = "";
        public MainSearch()
        {
            InitializeComponent();
        }

        private async void MainSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string seachedText = "";
            seachedText += MainSearchBar.Text;
            if (seachedText == "")
            {
                await DisplayAlert("Notification","No text entered","OK");
                BindingContext = null;
            }
            else
            {

                BindingContext = new SeachedProducts(seachedText);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _ = await Navigation.PopAsync();
                    barcodeScan = result.Text;

                });
            };

            if (barcodeScan == "")
            {
                await DisplayAlert("Notification", "No Barcode Scanned", "Ok");
            }
            else
            {
                //var Url = "ttp://shopbasket.azurewebsites.net/api/prodOnSpl";                 //Used When Deploying API
                var Url = "http://10.0.2.2:5000/api/search";                                  //used while local hosting API
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync(Url + "/" + barcodeScan);                //Gets response from API

                if (response.IsSuccessStatusCode)
                {
                    
                    

                    var content = await response.Content.ReadAsStringAsync();                     //Extact data from response 
                    if (content == "")
                    {
                        await DisplayAlert("Notification","No Product Found","Ok");
                    }
                    else
                    {
                        var prodInfo = JsonConvert.DeserializeObject<ProductListModelDisplay>(content);
                        
                        await Navigation.PushAsync(new ProductDetails(prodInfo.ProdName, prodInfo.ProdImg, prodInfo.ProdDescription, prodInfo.Barcode));
                    }
                }
            }
        }

       

        private async void ProdList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as StoreDetailModel;
            await Navigation.PushAsync(new ProductDetails(details.ProdName, details.ProdImg, details.ProdDescription, details.Barcode));
        }

        
    }
}