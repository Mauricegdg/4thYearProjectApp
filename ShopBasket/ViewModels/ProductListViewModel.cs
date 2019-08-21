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
                onPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void onPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

         
        public ProducListViewModel()
        {
           //ProdList = new ObservableCollection<ProductListModel>();
           //ProdList.Add(new ProductListModel { ProdName = "AWE", CatName = "CatAwe", ProdImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAPEAAADRCAMAAAAquaQNAAAAw1BMVEX///88UNIWLsn+/v78/P75+f339/3z8/zs7frq6/r09Pzw8Pvm5/kTLMne3/fj5Pjn5+fMzvPy8vLIyvLR0/Syte3ExvHV1/WusezNz/O2ue7Z2vafo+g3TNGqreuanuczSdHt7e2bn+e9wO+UmOYAIcd/hOGlqOqLj+QAG8fY2NiEieJ3fN8uRdAAIseOkuTT09Nxdt5IW9Vla9tPVtYAE8Z6f+CNl+JgZ9omP89hcNlodtsAAMVOVdZbYdlTZdccOc7aEzdFAAAWCUlEQVR4nO2cC3eiSBOGMSCCIsQ7IiJRI8brqImJZsbk//+qr6pvNOhkds/50M0u756dRIPKQ1XXpaVbUXLlypUrV65cuXLlypUrV65cuXLlypUrV65cuXLlypUrV65cuXLlypUrV65cuXLlypXrW0gluvVZXElqWrc+oYx1xvsvh5YpNe0/wCxYY/2rmWXeIlMC+9Yn+P+W4KWsOiimTjL/O9xd4kXYMpHOuLXkoP5XBDYGTHjL5VKpQlUqlZmtv4a+9en/fQlg4EVag4tBizGdjGvfOLAJYOQ1TNOyLMeBfyzTROiyDB0Htm8c12RgA3AdmwmxTbC0MDRX/Egw3xri7ygGrlRM03HsXq9D1esBtWVRZhrIRFijjyXmW2P8DcXAYGDg7TS73Vqj0ah1u80OYabOzVWJ41pZZr41x18Uy8MxcKdZa7TbI1S73ag1EZo6tyHHNBrXKtTdvweylFz4GDYtMHC30R55XgvleUDdIIYmgcy0MKbFMkkwL1Mz3wj5L+bJ5FEJ4BrwtoIo8qMoCgIXoBkzCWQipsVxjZr5JsR/sTpI/ZlmG12HKG05FNgNarWaNxyPxwM/clvIjM4NcQyFEQ0fdcgQhzFeqdwE+Te4X0hOrgzY7nTbo1bglfAdNbM3CobjIWPGOIaq1RoY1Rq1WpcNcYLM/PoGvImS6LeFYaKQ0HWahzmwb8TvW7ZGQz8K6IimGnHBVQB3J8g4lq9p5IscqZLoIixPryUK3Gs2Rl4wdMG+XqeksTdvrMG3AxfjGKrFRcMamJkgc7++InCaI4GdkDgIqwiaX6GwROAajGF/3FOU3md/+/7ativw9sZgOh6AoSGOgaJYcBU8iGpoZaNyRSMLXplDbvU4tlwiSg0S5lWTFB7NWttzo8FYV5TXsNDvh8vCx7upFNf7+XQNUYxqjFqDxuDrrjdqoGOjka81kjkwH49yRVQShaDMKmiR1GRdg02AR61oMAWnLm77BaJwV1SK881iP5nPp6j5fDKZrH131DF0uIr2CJF7spGvAsz72lJJ2MzkJZFUDgs3JgchquOwDAs5p4vAEJsntqLUlhS4sAR8c7c7bqgm46jVcEp6jGW7XrsWGzl7YgFM+1qDN3oOKQ5Mk1EnCmLmxQSVJdhmEytLDNPj+Roi1oSZuBBaihKEv36dFuNo1DN4NGMyiorqtYiRTaN0HRvHwCT8UP+MSyLSAiRkYqFIaBlpExNsAwtpD4Gne09RKhy4P4HPGPptp6InPrbs9Eav++MWrocLRobgxdw6c2IBTPtaAkI5aIlEqYm1aSHM3Bho0aw1JKUJFrKOGwHwZGMqiiecegSfIX9g0bRr0fz9o39Yhv1+AY4NAuLWMXGmyAlgi+CyPo/URISaGVuYnnoxGJag8gTrukHkDwB4MYX3PQobl8VHGU6ntV7sCstliKxUQBxF6NY4kK8Rughxoq8VfR5r9aRyuNdh1kc3xoM8AkoTrO8PB+P1fLLYtRXF4iYOX+FDypY9ep0ct0tqVlnPhqL6ELfJQL4GMTNxoq8lfZ4LYjURUkNfTwtiPmLRtmjVCDhpfoUMC4lnv9i8QUkdCGI/EB6cZKUesAXi4RWJObAu97UtAkJEaqK4HOa2J5cEDxoQzKjl+mtMshPg3b2hU28FXRimzZokrijalYnJIKbATdbXondOp2MfaH25HObjlfgx8eFp0LO7Tcg3WsluR1PAfXv7VYMUe/gtY5q4pBQHVyamgxj6WlIwQXbB5q7dMyuYOCse9re0HKbjFSw/HKIfr+dz305F4cb49PwMrxuEYhiHXxIXPsqKPvDlyJVprOaDuCTavCiqmYacOHUofqEeHoJgvA58f7yGihhKxcl+YPODHIcHZK1RQKcWgfo4Wn6BC3X3FoiHEKtr18nHImyhT3cbYOEo7muVYsUCEG+P5TCW/fOpZ+h6x6fjddwhxVMngh/v4cfcbZrkiX0XKkzu1EvPu0jcxx7j0D8dJ4Gm6H7AKxA9Y2KemUrEp7GvHTTwea1k9LzX+bHwCcWTs1lAEwCQE5dejfJ+s9nNa+S0nNfC0lGsEAHC5/dXoPWKijLlrrw03GWaFVCX2+Pi1es4ZJ5EsX2suViVmTkxmpiErQ62ef4Amtn29J0kzrBf6O/hqD02AcfNwMKXFMHjW6dTq4gPrNcwLCwDnor6/QPUV9AZlzlw/x0qapkVescjeINtSlBl179aAZIysRuNscs7LqVsApje89vpNCZjVuscgcl63lDeQkiGapnXV5hqVLgkI6nC9AXxceI3elYxcQZ6xWoMBlcrMqlTk0BNTBwMpzh1IQfXZQu6m/DXokle0F0cwiP83IGzl/3nkIEGokuCoKUD0l5csZLCY3V/m+iZyoY9GkwnxzfoHsGpGwmnzpKYOjU1cSsaj8uKkkgnfQTcLMjh9hwRD9jrBGD5z/OyIoQLY8oV5lxR1pz4g1oXwmEbUTfDwF08nzb76QCr6s41nDph4i4xMVi0/JEg6YM3exjOnPmSnHsItJYPZM9nxP0PXdEMqcI8dOMg1t8VlVI0ne9Oc69mU/qyByZeD4NUr5i1iSE1OTYdxRMHPDcZW0OAqwD161LYCk5oDmc8Pyst8FjIUMWdGNZafFj/qCnldqeSOAVzupiPecF1HafmcYuYeD3WzkD6W1XRTPnpZQfcGqzePi+m4MrANesk2iY+pvvv8jjWdKPrwuH+4lZOXcP5uDkEJGObclYcm2DRriiUkcOEEFX5SB3ZX6hQZ0qB4IDx/Z0T02CgFsu250+P4eGwhHf299d3akjGNDX544kpJRYBuAY4aWYSrA75Z2KkQhweCVEAGMv8SBL1xMxAf1K02sHre+FAUz08A87i45xmqvy4glPTuDWYDsHvFul41IfCVy3JfEtwaQ/ouv3UgaaiQiBvy3OYkMnEUbSqiV/Uh1Q4nIhhXMl6Wk+OWx2SmiZtGIaFM4VtYuRe7Nbo0nuIQ7sEMjqDDqafCKcmUSrt+5LAIQZJ4kxLTO7UBjo1FpjrCVgyXQUX6FxkBUboR+zWGMkgRPkJt+43SNwyxWF78jnpuJAgVsfXI07FLQ/iVgQh9Hh+fgTQlPlwdrINR/cSh0EyLlryHGabzGF+Qezchpgm4xbELQidzUunF0IIBxRbmB+trm+KSvFdOhyTMVSYqrhmS40E+a+J1yRyYY1pZT2OhVPzZDyd6ufhlwLuabUsjUjw3mlTkfoiGofgujgiGeOwhrP/LW//2QLi/Rr7iG5Pao6zQY5nMFkyHmMyLsUG2W6lk7OIW4viEfp8qDMGiTiHqagIcSsScQuuCAT0y8SkcexbijbFstqTJkAyMzJPTVIyhsg5EjYLo2Fsv9AlMcmJO12snzZl6bslchEgNmtx2oZP2Srx/A9/GlCXy+d9MGrC4eocqkx5ej67gSwlY+bUqQrTqUmJ8x1OoiR9zVBYwuUZ1hKX6JXEqQ7PYTistZ8ycR9IodkceDVLnEbttJgnSpDMbJxqIlgytiRGTe6hQocYMJ6yWkKktqGJcmKeLZnbE4EAr4knE/ePrbad/FpRab+9YbeYmBDICDnp1F7gr+emHIjQR2W3BkAYo5XYrbHZhdN342OWYHJFL4i2CR49v8An9aX3iKU3W8PNsro8bSbT2K0zNHJ68mMwRSd8T9QYHcmtPxRi5DgbHXrkfaT0Tb40bYg5TAhalZ8riGZpYtuLFnc/f/78hKHskCeCIXVrK8OMLCdjVmFCVW/HX5zM4aCiVEQuAVCXSmbaCCq2nJ22ck8JcVjxX+7gVYIYBsHoAKg/T2O3K8+IV+LuKUtiUWHSzniO0z3LPtOBzOFG9DEhHkJPBK+LCZ9xFiNRZkKhYsSDFv4arg4Q8LBNwrfBvqLtj6zUqRThbUc+maB3susXL3TGLp5blSssYeC1n6qxFII8eeSPX6DSLIbSAdXVhxLwVzxBHLBfqiHk8RV/KkidhdNurbernyMkdjOevZUiNUvGexhRjac7pscpTlIo2seKP3P3VCORqiuOWb1DbhGPUNW73m7Ff4VBP63ePQPXqsreweUfb9Q8f/f5BILrt4Ic4eFAzvSLtnQyHkzxTpWj4HuCQe1hERkTISCOvc8q51uZyj6+IuSp3Sc/Gguww+fdCSz9yN90pOidUbAPV4+ACpanr6nCpWzRQjO7YC1/2cQqTHAtM4bBCbkTlMUGtw8896iTufdXDnD3GJQP4s/sIP4YPaK2+vzcQX/FX1AN75B1Va0mXwJX1mXEmQXrRDJuE6eGZNsSBn2CTGUdgFh5l9zaI2VXL3bro/d4d1nVJVyb/efhcwM5ThxUrVYvHFoFfwquRRxP90BToGxjf3X4kaOX+NS2hFgtxKfdv0BALwZOkmBNCSm6+7vLwokh7wXDbIlT0z04TQ3JyBYWWO3EodoydusVbaD8mOB3wHePQOEdwvAwTUe3C8RQjQY+j1zZjGN5DrNLpnv2kHf82Kk9Ams3vPVWOreniBDbq9+CCgqsMDfhr1/hWM4Al4+9I8TZfreYrDBbdLpHES5a/Ry1h8fws4rJQz63Arg1zgus0medvgaPEbYYz8/Pfahb2l8RV6uPjzCEgmzn6NPfvbDpHum0VyR5nJnykU5yBOmBuXoPU+EXXMH9dTqdfgH66CIx5OHHp5ef1dN8WMYv7jK9SVHErbjChGQ0/TrAEOJXOq2TGr+QX+YJs2PqVnant7e3k3tOXF2tnl5eHgv7V7dZpvffVoIWu9s4u8CVrDDxC8V0ar1kl76qaCW5UqFPb9VR4plHvE/gtAO9tSRiataXw3HtN0xdbpTb2d5sfMGpcbrnzyampRjeYpo8FurliuzW1U9IxtEb3km9w5r5iaHefcxfPaecvP9WM5we+HSmN5SfOXWqwvxCWG6bqmIm/IEE26n08hV0mupms9jv9xsgbv1cFRbroFkpJ+Y/1IrTGUXkdilcNNBNmDgDYl3ncwHg1Djd8/vcKsNhKWUmKjFaQitNyeyPXXi8wRUCc8zzplVK3vlRduxGgHe/4bqJIa4JYsDZmVhNTnDhdE902amJP0p/eqmRmZC2fDR2jUqRF2yQbqAnVqLJnNwANpY/umjazdZwPZ1M5nSRCF0DxRf/GBmaOG4UsfzA6Z7tWUtAIurnx+R19BpbFB22oim65NbVA7nHa/gEr4DRWn1+h2hVnADRYDAcjLtoX63i9EYR3odMLgQuiCHrgFy+ti/TBV7p+wIGc0jGHalHghN/wROfBh16Y6Yj/fFOJ0aeJy8CHvS0PM6HDYve31ibkns5QVAwe7iMgJISu5LFm3gnM13cVeOL+ErlbL4+Pusi1mU58FSXx4k4cSapyEIX1rFcEY7wEy6YVVRUoxS/oDzEtZkuu6ue3LlK71wm61QJq8duVudrdA0GnB0xvb0HhzEmY9EvVEMzcXTZAYu6yXkBDc4ofARHuHt+nwdNeLRuxq/QKmbNJ2tRPXofNrs3nd+f7tEb89t8jSZdfJ4h8Fn9scbpnrjLn/ITN5xmazzZ9XGiWiqvn8rk+0J3MWw4JnMEczdpaopaNu1GKxqOWfxFEzb4LegUlN6XTlkZLV13XiErFjNawCcTY6iOktM9YC2jN/LXkw2pmd5OGzWRrJ9ahFiWMYDcC8FoTdMNW2vcplxdusiALzPpdsma4x6FpcupjHhRvZo1MX5P3k1O9wDN+vntSOqH/QKqplPvbF6gyAe5VrEgBtMAzLINWWQwogO0Iy0kQZs2OaqdWDvG98zIaq+I1JcREfZqL7xPwukeA8olur4QKojF5g3cWpf6qBe8C8C1ABXXNS3Y8svhkEXgloi/vXiRGFlEY8eoVmJ9YNabY0glF8lOeJPlcVklE25kmmO0mRAKurJlv9ngerzYrR9xDiwMn09veGHgOEw3GIFZWAJ3ZpthOJa0HixGjVlLdOlr1hugyGU1NhJdfNJoepPt59MTFozjyRQxcD3EEIqG/Qbah5rk1vi18Py0o7xsFXWLZRvizc0eyTdi5Z/JxHfCoAs/U8t81cyA5XlMG5cKt3g60p3aGipsEzh8UiG4buRDOFr4OPUsResOfgm6AXcGXrE0iIUlsb+LyeASKpVi1sRa7gx5k3dDkIURrZohN6sjklxwOHq4DAiQJ+XEfMEj3sIH43ctdoAghpUisMUDUloJ1PP1+tkA805Cp4tfyPJoP2q17QrNOZbvs2RKFiS6gIwLPZqSWx90cm/hwKddAK8jRFgS8TetFGpqc4KMeBOrFNmKLrqgaxCNOrbdjiKXb2JBVvPhWmK8a6kQT2aTKfXpgBxYY8s4abZhe9iIjXrO9LvdJzKj5ch0hVOFbnPAFu3hqi0cv2BfmkwhkdL14utpRXz5Un2s7vF+WncckT6+yYNyIgLryX24/rDRRra4SnIpqiNtwEPrXvBn4CVe2hN7AtTYdylg3zlZH+IwYNr1xGvQ472nir/juyaqTEyXZlLkJl1Wy7YjqbFkiouN6b4P/hjvXixAp1Q9klsh1J4vZmqkKJXaVeKfwHoRmW6kxddXYzal2YVtdUE2MxlD0z98We3prR92QC1M+3ijktpL4kvjXhlVIlalRfR8eXwzbt7IqCSrkcnCvgG4dW/XYbw0aDWaPZu1tYn9Qi7Q3gZTFjMy7gxR4RsHILYtZVOyHYRF8pfXilo8YVvBFOoTsWMJ22Pq8lY4N2VMSULmmx46Yi8MPizhL2xFMiDTNahmQCcfcS6uR4HZ1mlnhr0xYVoqR443thSlb4WPS7JBIE/ZrYZhWN6Y5S9IwwngfzYtEUM+27xU3r5U5/mrSbcXo5NUtMzqXAa+NdZXUhPM8b5FcfFQjFM220vB8+TJRwb8fbZ8VGVmvjcRC7p8N6pkyqYSk63fDVi5vNG0lE1lZLonSpe1gxDPMbxlNveYncTwu1z5xsgsZfPNTdOTrd+H+E/7CMp7G/H9bejcRrZzjxnrz8hYmRmmvJsnawa/JzDRpXQqkNHMJbFta6US835f4MuKQ7nO92UTe7F9082X/yRVNjPL2Gcbyt/6JP/PSmRskrWvMNd6W8nJ68Ls461PLwvxoK1eyNe3PreMlMhVZwH936lLifrW55S5/mO4uXLlypUrV65cuXLlypUrV65cuXLlypUrV65cuXLlypUrV65cuXLlyvUfkqrV6w/1el3T4Bf8WY9/sN8fHupJ4a2S8QPyxDe6t0ytP9yDkOrhou7pn2XRy5B44hsRaw/3dfX+K9XVs7+z6yA9Ub81x19X/b6uPMzu1RmctqrOHnAh6MODNtPqyv1Mu5/NAGY2m9XhV3Dl2QN4xQwvg6LN7uFQuB6Kqnw74joSz2bagwJk4KMP98oPZQaWrT8gLfwPxzzMyKW5V3/M8DL8+AHX44cKR6lwwP23IoazR3MBy70yQ1xGjEbUwNDICMT3szqa/V4D/vt7RYXHilb/gbzfjPjhYYZuOvsBHnz/g/13P2M/6vgP/p0/SR79IMfTf8Dk97Pv5NUaWPThLBL9TT3UtT9/0j9Fqlb/Sme5+LI09X9ChgJhkb6MLQAAAABJRU5ErkJggg==" });

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
