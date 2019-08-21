using ShopBasket.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopBasket.Models
{
    public class RegisterUserModel
    {
        

        public event PropertyChangingEventHandler PropertyChanged = delegate { };

        public string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangingEventArgs("EntryUsername"));
            }
        }

        public string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangingEventArgs("EntryPassworrd"));
            }
        }

        public string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangingEventArgs("EntryName"));
            }
        }
        public string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                PropertyChanged(this, new PropertyChangingEventArgs("EntrySurname"));
            }
        }

        
        
        


        public ICommand SubmitCommand { get; set; }
        

        public RegisterUserModel()
        {
            SubmitCommand = new Command(OnSubmit);

        }



        public void OnSubmit()
        {
            if (string.IsNullOrEmpty(Username))
            {
                MessagingCenter.Send(this, "RegisterAlert", Username);
            }
            if (string.IsNullOrEmpty(Password))
            {
                MessagingCenter.Send(this, "RegisterAlert", Password);
            }
        }

    }
}
