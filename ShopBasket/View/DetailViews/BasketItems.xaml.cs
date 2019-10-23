using ShopBasket.Models;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopBasket.View.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketItems : ContentPage
    {
        string Username;
        public ObservableCollection<UserListItems> listItems = new ObservableCollection<UserListItems>();
        UserItems userItems = new UserItems();
        bool changesAreMade = false;
        public BasketItems(string username)
        {
            InitializeComponent();
            
            Username = username;
            
            BindingContext = userItems;
            userItems.GetUserItems(username);
            // listItems = prodList.ItemsSource;
        }

       

        public async void ProdList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string[] buttons = { "Decrease Qty", "Increase Qty", "Delete Item" };

            var item = e.Item as UserListItems;

           string reaction =  await DisplayActionSheet("Edit Item","Cancel","",buttons);

            switch (reaction)
            {
                case "Decrease Qty":
                    {
                        if (item.Qty > 1)
                        {
                              changesAreMade = true;
                              
                              //userItems.DecreaseQty(item);
                              //BindingContext = userItems;
                        }
                        
                    }
                    break;
                case "Increase Qty":
                    {
                        changesAreMade = true;
                        //prodList.BeginRefresh();
                        //userItems.IncreaseQty(item);
                        
                    }
                    break;
                case "Delete Item":
                    {
                        changesAreMade = true;
                        //prodList.BeginRefresh();
                        //userItems.DeleteItem(item);
                    }
                    break;
            }
        }

        private void ProdList_Refreshing(object sender, EventArgs e)
        {
            userItems.refresh();
        }


    }
}