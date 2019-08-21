using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.Services;
using ShopBasket.View.Menu;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopBasket.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            var logUser = new LoginUser()
            {
                UserName = EntryUserName.Text,
                Password = EntryPassword.Text

            };

            if (logUser.UserName != null && logUser.Password != null)
            {



                var Url = "http://10.0.2.2:5000/api/login";

                HttpClient httpClient = new HttpClient();


                var user = new User();

                var jsonObject = JsonConvert.SerializeObject(logUser);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(Url, content);

                if (response.IsSuccessStatusCode == true)
                {

                    var content2 = await response.Content.ReadAsStringAsync();

                    if (content2 == "\"User not existing in Database\"")
                    {
                         await DisplayAlert("Unsuccessfull", "Wrong Details", "OK");
                    }
                    else if (content2 == "\"Wrong Password\"")
                    {
                        await DisplayAlert("Unsuccessfull", "Wrong Password", "OK");
                    }

                    else
                    {
                        user = JsonConvert.DeserializeObject<User>(content2);

                        await DisplayAlert("Welcome", user.Name.ToString() +" "+ user.Surename.ToString() , "OK");

                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            await Navigation.PushAsync(new MasterDetail());
                        }
                        else if (Device.RuntimePlatform == Device.Android)
                        {
                            Application.Current.MainPage = new MasterDetail();
                        }

                    }


                }
                else
                {
                    Debug.WriteLine("An error occured while loading data");
                    // LoggedIN = false;
                }
            }
            else
            {
                await DisplayAlert("Unsuccessfull", "Please enter details", "OK");
            }











            //if (loginUser.UserName != null && loginUser.Password != null)
            //{
            //    var restAPI = new RestAPI();
            //    restAPI.Login(loginUser);
            //    // bool loggedin = restAPI.LoggedIN;

            //    if (loggedin == true)
            //    {
            //        await DisplayAlert("Login", "Login Success", "OK");

            //        if (Device.RuntimePlatform == Device.iOS)
            //        {
            //            await Navigation.PushAsync(new NavigationPage(new MasterDetail()));
            //        }
            //        else if (Device.RuntimePlatform == Device.Android)
            //        {
            //            Application.Current.MainPage = new MasterDetail();
            //        }
            //    }

            //    else
            //    {
            //        await DisplayAlert("Unsuccessfull", "Wrong Details", "OK");
            //    }


            //}

            //else
            //{
            //    await DisplayAlert("Unsuccessfull", "Please enter details", "OK");
            //}
        }
    }
}