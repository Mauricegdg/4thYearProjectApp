using ShopBasket.Services;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                DisplayAlert("No Internet","","OK");
                return;
            }

            var registerUser = new RegisterUser()
            {
                Name = EntryName.Text,
                Surename = EntrySurname.Text,
                UserName = EntryUsername.Text,
                Password = EntryPassword.Text

            };

            RestAPI restAPI = new RestAPI();
            restAPI.Register(registerUser);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Congratulations", "User Registeration Successfull", "Yes", "Cancel");
                if (result)
                {
                    await Navigation.PushAsync( new LoginPage());
                }
            });
        }
    }
}