using Newtonsoft.Json;
using ShopBasket.Models;
using ShopBasket.Models.sp_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopBasket.ViewModels
{
    
    public class ProducListViewModel : INotifyPropertyChanged
    {
        //public List<ProductListModel> ProdList { get; set; }
        //ProdList = new ObservableCollection<ProductListModel>();
        ObservableCollection<ProductListModel>_prodList;
        public ObservableCollection<ProductListModel> ProdList
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

         
        public ProducListViewModel()
        {

           GetProductsOnSpecial();

        }

        public async void GetProductsOnSpecial()
        {
            

            //ProdList = new List<ProductListModel>();


            //var Url = "ttp://shopbasket.azurewebsites.net/api/prodOnSpl";
            var Url = "http://10.0.2.2:5000/api/ProdOnSpl";
            HttpClient httpClient = new HttpClient();

            

            var response = await httpClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {

                var content2 = await response.Content.ReadAsStringAsync();
                if (content2 == "")
                {

                }


               // ProdList = new ObservableCollection<ProductListModel>();

                var prodInfo = JsonConvert.DeserializeObject<List<ProductListModel>>(content2);

                ProdList = new ObservableCollection<ProductListModel>(prodInfo);
                //ProdList.Add(new ProductListModel(prodInfo));

                Debug.WriteLine("{0}",ProdList[0].ProdName);

            }

            else
            {
                Debug.WriteLine("An error occured while loading data");
            }

            
        }

    }
}
