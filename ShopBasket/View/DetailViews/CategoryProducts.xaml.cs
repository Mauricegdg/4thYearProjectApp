using ShopBasket.Models;
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
    public partial class CategoryProducts : ContentPage
    {
        public CategoryProducts(int catID)
        {
            InitializeComponent();
            BindingContext = new GetProductsByCategory(catID);

            switch (catID)
            {
                case 1:
                    Title = "Pantry";
                    break;
                case 2:
                    Title = "Frozen Foods";
                    break;
                case 3:
                    Title = "Beverages";
                    break;
                case 4:
                    Title = "Electronics Office";
                    break;
                case 5:
                    Title = "Health & Beauty";
                    break;
                case 6:
                    Title = "Baby";
                    break;
                case 7:
                    Title = "Household & Cleaning";
                    break;
                case 8:
                    Title = "Home & Outdoor";
                    break;
            }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as ProductByCat;
            await Navigation.PushAsync(new ProductDetails(details.ProdName, details.ProdImageUrl, details.ProdDescription, details.Barcode));
        }
    }
}