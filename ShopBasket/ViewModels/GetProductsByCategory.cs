using Newtonsoft.Json;
using ShopBasket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopBasket.ViewModels.sp_Models
{
    public class GetProductsByCategory : INotifyPropertyChanged
    {
        ObservableCollection<ProductByCat> _prodList;
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


            //ProdList = new List<ProductListModel>();


            //var Url = "ttp://shopbasket.azurewebsites.net/api/prodOnSpl";
            var Url = "http://10.0.2.2:5000/api/ProductsByCategory";
            HttpClient httpClient = new HttpClient();



            var response = await httpClient.GetAsync(Url+"/"+catID);

            if (response.IsSuccessStatusCode)
            {

                var content2 = await response.Content.ReadAsStringAsync();
                if (content2 == "")
                {

                }


                // ProdList = new ObservableCollection<ProductListModel>();

                var prodInfo = JsonConvert.DeserializeObject<List<ProductByCat>>(content2);

                ProdList = new ObservableCollection<ProductByCat>(prodInfo);
                //ProdList.Add(new ProductListModel(prodInfo));

               

            }

            else
            {
                Debug.WriteLine("An error occured while loading data");
            }


        }
    }
}
