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
    public partial class SearchOrScan : ContentPage
    {
        public SearchOrScan()
        {
           // InitializeComponent();
        }

        

        private void MainSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private void ProdList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}