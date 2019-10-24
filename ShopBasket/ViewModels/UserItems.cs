using Newtonsoft.Json;
using ShopBasket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShopBasket.ViewModels
{
    public class UserItems : INotifyPropertyChanged
    {
        
        string StoreID = Preferences.Get("Store_IDs", "");

        ObservableCollection<UserListItems> _itemList;

        public ObservableCollection<UserListItems> ItemList
        {
            get
            {
                return _itemList;
            }

            set
            {
                _itemList = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<UserListItems> _storeitemList;

        public ObservableCollection<UserListItems> StoreItemList
        {
            get
            {
                return _storeitemList;
            }

            set
            {
                _storeitemList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserItems()
        {
            
        }

        public async void GetUserStoreItems(string Username,int storeID)
        {
            //var Url = "http://shopbasket.azurewebsites.net/api/ShopList";                 //Used When Deploying API
            //var Url = "http://3d05b49d.ngrok.io/api/ShopList";
            var Url = "http://10.0.2.2:5000/api/BasketStoreItems";                                  //used while local hosting API
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(Url + "/" + Username + "/" + storeID);                //Gets response from API

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();                     //Extact data from response 
                if (content == "")
                {
                    // DO NOTHING!!!!!!!!!!!
                }
                else
                {
                    var itemInfo = JsonConvert.DeserializeObject<ObservableCollection<UserListItems>>(content);  //Extact data to list of products

                    for (int k = 0; k < itemInfo.Count; k++)                                    // Loop through List of products given from API         
                    {
                        if (StoreItemList == null)
                        {
                            StoreItemList = itemInfo;

                            for (int p = 0; p < itemInfo.Count; p++)
                            {
                                if (StoreItemList[p].OnSpecial == true)
                                {
                                    StoreItemList[p].OnSale = "On Sale";
                                }

                                StoreItemList[p].CurrentPrice = "R" + StoreItemList[p].Current_Price.ToString("n2");

                                MemoryStream ms = new MemoryStream(itemInfo[p].ProdImg);

                                StoreItemList[p].ProdImage = ImageSource.FromStream(() => ms);

                            }

                        }

                        else
                        {
                            bool duplicate = false;
                            for (int j = 0; j < StoreItemList.Count; j++)
                            {
                                if (itemInfo[k].Barcode == StoreItemList[j].Barcode)                  // Loops through temp list and elinates duplicate products
                                {
                                    duplicate = true;
                                    break;
                                }

                            }
                            if (duplicate == false)
                            {
                                if (itemInfo[k].OnSpecial == true)
                                {
                                    itemInfo[k].OnSale = "On Sale";
                                }

                                itemInfo[k].CurrentPrice = "R" + itemInfo[k].Current_Price.ToString("n2");

                                MemoryStream ms = new MemoryStream(itemInfo[k].ProdImg);

                                itemInfo[k].ProdImage = ImageSource.FromStream(() => ms);

                                StoreItemList.Add(itemInfo[k]);
                            }
                        }
                    }

                }

            }

            else
            {
                Debug.WriteLine("An error occured while loading data"); // Sever ERROR 

            }

        }

        public async void GetUserItems(string Username)
        {
            //var Url = "http://shopbasket.azurewebsites.net/api/ShopList";                 //Used When Deploying API
            //var Url = "http://3d05b49d.ngrok.io/api/ShopList";
            var Url = "http://10.0.2.2:5000/api/ShopList";                                  //used while local hosting API
            HttpClient httpClient = new HttpClient();
            
                    var response = await httpClient.GetAsync(Url + "/" + Username);                //Gets response from API

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();                     //Extact data from response 
                        if (content == "")
                        {
                            // DO NOTHING!!!!!!!!!!!
                        }
                        else
                        {
                            var itemInfo = JsonConvert.DeserializeObject<ObservableCollection<UserListItems>>(content);  //Extact data to list of products

                            for (int k = 0; k < itemInfo.Count; k++)                                    // Loop through List of products given from API         
                            {
                                if (ItemList == null)
                                {
                                    ItemList = itemInfo;

                                    for (int p = 0; p < itemInfo.Count; p++)
                                    {
                                        

                                        MemoryStream ms = new MemoryStream(itemInfo[p].ProdImg);

                                        ItemList[p].ProdImage = ImageSource.FromStream(() => ms);

                                    }

                                }

                                else
                                {
                                    bool duplicate = false;
                                         for (int j = 0; j < ItemList.Count; j++)
                                        {
                                                if (itemInfo[k].Barcode == ItemList[j].Barcode)                  // Loops through temp list and elinates duplicate products
                                                {
                                                   duplicate = true;
                                                   break;
                                                }

                                         }
                                          if (duplicate == false)
                                          {
                                                 MemoryStream ms = new MemoryStream(itemInfo[k].ProdImg);

                                                 itemInfo[k].ProdImage = ImageSource.FromStream(() => ms);

                                                 ItemList.Add(itemInfo[k]);
                                          }
                                 }
                            }
                            
                        }

                    }

                    else
                    {
                        Debug.WriteLine("An error occured while loading data"); // Sever ERROR 

                    }

        }

        public List<UserListItems> DecreaseQty(UserListItems Item)
        {
            List<UserListItems> displayList = new List<UserListItems>();

            if (_itemList != null )
            {
               
                foreach (var items in _itemList)
                {
                    if (items.Barcode == Item.Barcode)
                    {
                        items.Qty -= 1;

                        MemoryStream ms = new MemoryStream(items.ProdImg);

                        items.ProdImage = ImageSource.FromStream(() => ms);

                        displayList.Add(items);


                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(items.ProdImg);

                        items.ProdImage = ImageSource.FromStream(() => ms);

                        displayList.Add(items);
                    }
                }

                
            }
             
            return displayList;
            
        }
        

        public List<UserListItems> IncreaseQty(UserListItems Item)
        {
            List<UserListItems> displayList = new List<UserListItems>();

            if (_itemList != null)
            {

                foreach (var items in _itemList)
                {
                    if (items.Barcode == Item.Barcode)
                    {
                        items.Qty += 1;
                        MemoryStream ms = new MemoryStream(items.ProdImg);

                        items.ProdImage = ImageSource.FromStream(() => ms);
                        displayList.Add(items);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(items.ProdImg);

                        items.ProdImage = ImageSource.FromStream(() => ms);
                        displayList.Add(items);
                    }
                }


            }

            return displayList;
        }

        public List<UserListItems> DeleteItem(UserListItems Item)
        {
            List<UserListItems> displayList = new List<UserListItems>();

            if (_itemList != null)
            {

                foreach (var items in _itemList)
                {
                    if (items.Barcode == Item.Barcode)
                    {
                        //_itemList.Remove(items);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(items.ProdImg);

                        items.ProdImage = ImageSource.FromStream(() => ms);
                        displayList.Add(items);
                    }
                }
            }

            return displayList;
        }

        public List<UserListItems> getEditedList()
        {
            return new List<UserListItems>(_itemList);
        }

    }

}

