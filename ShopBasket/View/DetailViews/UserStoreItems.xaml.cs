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
    public partial class UserStoreItems : ContentPage
    {
        string username = "";
        UserItems userItems = new UserItems();

        public UserStoreItems(string UserName,int storeID,string storeName)
        {
            InitializeComponent();
            username = UserName;
            BindingContext = userItems;
            userItems.GetUserStoreItems(username,storeID);
            Title = storeName ;
        }


    }
}