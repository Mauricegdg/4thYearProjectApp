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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShopBasket.ViewModels.sp_Models
{
    public class GetProductsByCategory : INotifyPropertyChanged
    {
        ObservableCollection<ProductByCat> _prodList;
        ObservableCollection<ProductByCat> pre_prodList = new ObservableCollection<ProductByCat>();
        public ObservableCollection<ProductByCat> ProdList
        {
            get
            {
                return _prodList;
            }

            set
            {
                _prodList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public GetProductsByCategory(int CatID)
        {

            GetProductsByCatID(CatID);

        }

        public async void GetProductsByCatID(int catID)
        {
            string StoreID = Preferences.Get("Store_IDs", "");
            string[] StoreIDs = StoreID.Split(',');

            //ProdList = new List<ProductListModel>();


            //var Url = "http://shopbasket.azurewebsites.net/api/ProductsByCategory";
            var Url = "http://10.0.2.2:5000/api/ProductsByCategory";
            //var Url = "http://3d05b49d.ngrok.io/api/ProductsByCategory";
            HttpClient httpClient = new HttpClient();



            var response = await httpClient.GetAsync(Url + "/" + catID);

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                if (content == "")
                {

                }
                else
                {
                    var prodInfo = JsonConvert.DeserializeObject<List<ProductByCat>>(content);  //Extact data to list of products

                    foreach (var prod in prodInfo)
                    {


                        for (int i = 0; i < StoreIDs.Length - 1; i++)
                        {
                            if (prod.StoreID == i + 1)
                            {
                                if (pre_prodList.Count == 0)
                                {
                                    
                                    pre_prodList.Add(prod);
                                }
                                else
                                {
                                    bool duplicate = false;
                                    for (int j = 0; j < pre_prodList.Count; j++)
                                    {
                                        if (prod.Barcode == pre_prodList[j].Barcode)      // Loops through temp list and elinates duplicate products
                                        {
                                            duplicate = true;
                                            break;
                                        }

                                    }


                                    if (duplicate == false)
                                    {
                                       
                                        pre_prodList.Add(prod);
                                    }
                                }

                            }
                        }

                       



                    }
                    ProdList = pre_prodList;
                    

                    for (int k = 0; k < pre_prodList.Count; k++)
                    {
                        MemoryStream ms = new MemoryStream(pre_prodList[k].ProdImg);
                        ProdList[k].ProdImage = ImageSource.FromStream(() => ms);
                       
                    }
                    

                }
            }
            else
            {
                Debug.WriteLine("An error occured while loading data");
            }
        }
    }
}
