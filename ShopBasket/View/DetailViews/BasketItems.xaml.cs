using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
        public List<UserListItems> deletedlistItems = new List<UserListItems>();
        UserItems userItems = new UserItems();
        bool changesAreMade = false;
        public BasketItems(string username)
        {
            InitializeComponent();
            
            Username = username;
            
            BindingContext = userItems;
            userItems.GetUserItems(username);
          
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
                              prodList.ItemsSource = userItems.DecreaseQty(item);
                            //userItems.DecreaseQty(item);
                            //BindingContext = userItems;
                        }
                        
                    }
                    break;
                case "Increase Qty":
                    {
                        changesAreMade = true;
                        //prodList.BeginRefresh();
                        prodList.ItemsSource = userItems.IncreaseQty(item);

                    }
                    break;
                case "Delete Item":
                    {
                        changesAreMade = true;
                        //prodList.BeginRefresh();
                        prodList.ItemsSource = userItems.DeleteItem(item);
                        deletedlistItems.Add(item);
                    }
                    break;
            }
        }

        private async void ContentPage_Disappearing(object sender, EventArgs e)
        {
            List<UserListItems> EditedList = userItems.getEditedList();

            var action = await DisplayAlert("Conformation", "Do you want to save changes?", "Yes", "Cancel");
            if (action)
            {
                if (changesAreMade == true)
                {
                    var Url = "http://10.0.2.2:5000/api/ShopList";
                    //var Url = "http://3d05b49d.ngrok.io/api/ShopList";
                    //var Url = "http://shopbasket.azurewebsites.net/api/ShopList";

                    HttpClient httpClient = new HttpClient();

                    HttpResponseMessage response = await httpClient.DeleteAsync(Url + "/" + Username);

                    if (response.IsSuccessStatusCode == true)
                    {
                        bool ItemIsDeleted = false;

                        foreach (var item in EditedList)
                        {
                            var shopinglistInfo = new ShoppingListInfo()
                            {
                                UserName = Username,
                                Barcode = item.Barcode,
                                Qty = item.Qty

                            };

                            foreach (var delItem in deletedlistItems)
                            {
                                if (item.Barcode == delItem.Barcode)
                                {
                                    ItemIsDeleted = true;
                                }
                                else
                                {
                                    ItemIsDeleted = false;
                                }
                            }

                            if (ItemIsDeleted == false)
                            {
                                Url = "http://10.0.2.2:5000/api/ShopList";
                                //var Url = "http://3d05b49d.ngrok.io/api/ShopList";
                                //var Url = "http://shopbasket.azurewebsites.net/api/ShopList";



                                var jsonObject = JsonConvert.SerializeObject(shopinglistInfo);
                                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                                HttpResponseMessage response2 = await httpClient.PostAsync(Url, content);
                                if (response2.IsSuccessStatusCode == false)
                                {
                                    await DisplayAlert("UnSuccessfully", "There seems to be an error with Editing your items.", "OK");
                                    break;

                                }
                            }

                            
                        }
                    }
                    else
                    {
                        await DisplayAlert("UnSuccessfully", "There seems to be an error with Editing your items.", "OK");
                    }
                    
                }
            }
        }
    }
}