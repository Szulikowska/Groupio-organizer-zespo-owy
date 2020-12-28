using Projekt.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        
        async void LogOut(object sender, EventArgs e)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");
            await Xamarin.Essentials.SecureStorage.SetAsync("userLogin", "");
            await App.UserDatabase.DeleteItemAsync(await App.UserDatabase.GetItemAsync(0));
            NavigationPage tmp = new NavigationPage(new LogInPage()) { BarBackgroundColor = Xamarin.Forms.Color.FromHex("#724F7B") };
            NavigationPage.SetHasBackButton(tmp, false);
            await App.Current.MainPage.Navigation.PushModalAsync(tmp);
        }

    }
}
