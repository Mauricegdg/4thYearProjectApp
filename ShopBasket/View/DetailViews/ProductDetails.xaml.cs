using ShopBasket.Models;
using ShopBasket.ViewModels;
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
    public partial class ProductDetails : ContentPage
    {
        public ProductDetails(string Name, string Image, string Description, string Barcode)
        {
            InitializeComponent();
            ProdName.Text = Name;
            ProdImage.Source = new UriImageSource()
            {
                Uri = new Uri(Image)
            };
            ProdDescription.Text = Description;

            ProductListModel productListModel = new ProductListModel();
            productListModel.Barcode = Barcode;

            BindingContext = new StoreDetailViewModel(productListModel);
        }

        public ProductDetails()
        {

        }
    }
}