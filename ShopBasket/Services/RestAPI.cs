using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.Models.sp_Models;
using ShopBasket.View;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using ShopBasket.View.DetailViews;

namespace ShopBasket.Services
{
    public class RestAPI
    {
        //public List<ProductsOnSpecial> prodl = new List<ProductsOnSpecial>();
        // public List<ProductsOnSpecial> GetProductsOnSpecials()
        //{
        //GetProductsOnSpecial();

        //   return prodl;


        //  }
        public bool LoggedIN = false;
        public async void Register(RegisterUser user)
        {
           // var Url = "http://shopbasket.azurewebsites.net/api/register";
            var Url = "http://10.0.2.2:5000/api/register";
            //var Url = "http://3d05b49d.ngrok.io/api/register";
            HttpClient httpClient = new HttpClient();

            //bool IsLoading = true;

            

            var jsonObject = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(Url, content);

            if (response.IsSuccessStatusCode)
            {
                
                var content2 = await response.Content.ReadAsStringAsync();
                
                
                if (content2 == "Register successfully")
                {


                    RegisterPage registerPage = new RegisterPage();

                    await registerPage.DisplayAlert("Register", "Register Successfull!!", "OK");
                }

                

            }
            else
            {
                Debug.WriteLine("An error occured while loading data");
                
            }

            //IsLoading = false;
        }

        //public async void Login(LoginUser logUser)
        //{
        //    //var Url = "http://shopbasket.azurewebsites.net/api/login";
        //    

        //    HttpClient httpClient = new HttpClient();

        //    //bool IsLoading = true;

        //    var user = new User();

        //    var jsonObject = JsonConvert.SerializeObject(logUser);
        //    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await httpClient.PostAsync(Url, content);

        //    if (response.IsSuccessStatusCode == true)
        //    {

        //        var content2 = await response.Content.ReadAsStringAsync();
        //        if (content2 == "\"User not existing in Database\"")
        //        {
        //            //Error Message
        //            LoggedIN = false;
        //        }

        //        else
        //        {
        //            user = JsonConvert.DeserializeObject<User>(content2);
        //            LoggedIN = true;

        //        }





        //        //var user = new User();
        //        //user = jsonObject.

        //    }
        //    else
        //    {
        //        Debug.WriteLine("An error occured while loading data");
        //        LoggedIN = false;
        //    }

        //    //IsLoading = false;
        //}

        public async void GetProductsOnSpecial()
        {
            var Url = "http://shopbasket.azurewebsites.net/api/prodOnSpl";
            HttpClient httpClient = new HttpClient();

            List<ProductsOnSpecial> prodOnSpecialList = new List<ProductsOnSpecial>();

            

            ProductsOnSpecial productsOnSpecial = new ProductsOnSpecial();

            //var jsonObject = JsonConvert.SerializeObject(productsOnSpecial);
            //var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {

                var content2 = await response.Content.ReadAsStringAsync();
                if (content2 == "")
                {

                }

                var prodInfo = JsonConvert.DeserializeObject<List<ProductsOnSpecial>>(content2);

                prodOnSpecialList = new List<ProductsOnSpecial>(prodInfo);

                //return prodOnSpecialList;
                
            }

            else
            {
                Debug.WriteLine("An error occured while loading data");
            }

            //return prodOnSpecialList;
        }
    }
}
