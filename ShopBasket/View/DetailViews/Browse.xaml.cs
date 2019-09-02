using ShopBasket.ViewModels.sp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopBasket.View.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Browse : ContentPage
    {
        public Browse()
        {
            InitializeComponent();
            
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(1));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(2));
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(3));
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(4));
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(5));
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(6));
        }

        private void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(7));
        }

        private void TapGestureRecognizer_Tapped_8(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryProducts(8));
        }
    }
}