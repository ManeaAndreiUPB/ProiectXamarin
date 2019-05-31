using BlankApp1.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsAcademy.Utils;

namespace BlankApp1.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
    {
        public ICommand LoginClicked { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private bool _rememberMe;
        public bool RememberMe
        {
            get { return _rememberMe; }
            set { SetProperty(ref _rememberMe, value); }
        }

        public LoginPageViewModel(INavigationService navigationMethod, IPageDialogService dialogService) : base(navigationMethod, dialogService)
        {
            if (Settings.RememberMe)
            {
                RememberMe = true;
                Username = Settings.Username;
                Password = Settings.Password;
            }
            LoginClicked = new Command(Button_Clicked);
        }

        private async void Button_Clicked()
        {
            string username = Username;
            string password = Password;
            bool remember = RememberMe;
            bool loggedIn = false;

            Settings.RememberMe = remember;

            if (remember)
            {
                Settings.Username = username;

                if (Settings.Password == Settings.DefaultPassword)
                {
                    Settings.Password = password;
                }
            }
            else
            {
                Settings.Username = username;
                if (Settings.Password == password)
                {
                    Settings.Password = Settings.DefaultPassword;
                    loggedIn = true;
                }
                Settings.Username = string.Empty;
            }

            if (loggedIn || (Settings.Password == password))
            {
                await NavigationMethod.NavigateAsync(nameof(Home));
            }
            else
            {
                await DialogService.DisplayAlertAsync("Alert", "Invalid Password", "OK");
            }
        }
    }
}
