using Newtonsoft.Json;
using ShopBasket.Services;
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

namespace ShopBasket.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            //{
             //   DisplayAlert("No Internet","","OK");
              //  return;
           // }

            var registerUser = new RegisterUser()
            {
                Name = EntryName.Text,
                Surename = EntrySurname.Text,
                UserName = EntryUsername.Text,
                Password = EntryPassword.Text

            };


            
                // var Url = "http://shopbasket.azurewebsites.net/api/register";
                var Url = "http://10.0.2.2:5000/api/register";
                //var Url = "http://3d05b49d.ngrok.io/api/register";
                HttpClient httpClient = new HttpClient();

                //bool IsLoading = true;



                var jsonObject = JsonConvert.SerializeObject(registerUser);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(Url, content);

            if (response.IsSuccessStatusCode)
            {

                var content2 = await response.Content.ReadAsStringAsync();


                if (content2 == "\"Register successfully\"")
                {

                    await DisplayAlert("Register", "Register Successfull!!", "OK");
                   
                    await  Navigation.PushAsync(new LoginPage());
                }
                else if (content2 == "\"User is existing in Database\"")
                {
                    await DisplayAlert("Register", "User is existing in Database", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Unknown Error occured Please try again.", "OK");
                }



            }
            else
            {
                await DisplayAlert("Error", "There was an error with registering user please try again", "OK");
                // Debug.WriteLine("An error occured while loading data");
            }
                
               // RestAPI restAPI = new RestAPI();
           // restAPI.Register(registerUser);
           // Device.BeginInvokeOnMainThread(async () =>
            //{
               // var result = await this.DisplayAlert("Congratulations", "User Registeration Successfull", "Yes", "Cancel");
               // if (result)
               // {
               //     await Navigation.PushAsync( new LoginPage());
               // }
            //});
        }
    }
}