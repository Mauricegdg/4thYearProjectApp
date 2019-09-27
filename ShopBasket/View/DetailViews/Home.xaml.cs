﻿using Newtonsoft.Json;
using ShopBasket.Models.sp_Models;
using ShopBasket.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ShopBasket.ViewModels;
using ShopBasket.Models;
using System.Collections.ObjectModel;

namespace ShopBasket.View.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
       
        private ProducListViewModel plvm = new ProducListViewModel();
        

        public Home()
        {
            InitializeComponent();
            StoresInRange SETstoreRanges = new StoresInRange();
            SETstoreRanges.SetStoresInRange();
            //OnSpecialList();
            //GetProductsOnSpecial();
            BindingContext = new ProducListViewModel();


            
            

           // RestAPI restAPI = new RestAPI();
            
           // BindingContext = restAPI.GetProductsOnSpecials();
        }

        

        void OnSpecialList()
        {
            //var restAPI = new RestAPI();
           // restAPI.GetProductsOnSpecial();

            //ProdList.ItemsSource = ProductList;


            
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as ProductListModel;
            await Navigation.PushAsync(new ProductDetails(details.ProdName, details.ProdImg, details.ProdDescription, details.Barcode));
        }

        private void MainSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var KeyWord = MainSearchBar.Text;

           prodList.ItemsSource =  plvm.GetSearchedProducts(KeyWord);
            if (prodList.ItemsSource == null)
            {
                DisplayAlert("No Products found", "No products found.", "OK");
            }
            
        }

        //public async void GetProductsOnSpecial()
        //{
        //    var Url = "http://shopbasket.azurewebsites.net/api/prodOnSpl";
        //    HttpClient httpClient = new HttpClient();

        //    List<ProductsOnSpecial> prodOnSpecialList = new List<ProductsOnSpecial>();



        //    ProductsOnSpecial productsOnSpecial = new ProductsOnSpecial();


        //    var response = await httpClient.GetAsync(Url);

        //    if (response.IsSuccessStatusCode)
        //    {

        //        var content2 = await response.Content.ReadAsStringAsync();
        //        if (content2 == "")
        //        {

        //        }

        //        var prodInfo = JsonConvert.DeserializeObject<List<ProductsOnSpecial>>(content2);

        //        ProdList.ItemsSource = new List<ProductsOnSpecial>(prodInfo);

        //        //return prodOnSpecialList;

        //    }

        //    else
        //    {
        //        Debug.WriteLine("An error occured while loading data");
        //    }

        //    //  return prodOnSpecialList;
        //}
    }
}
