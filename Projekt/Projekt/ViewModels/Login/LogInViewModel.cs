using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Projekt.Views;
using System.Windows.Input;
using Projekt.Commands;
using Projekt.Dictionary;
using System.ComponentModel;
using System.Threading.Tasks;
//using Android.Content.Res;
using Projekt.Models;

namespace Projekt.ViewModels
{
    public class LogInViewModel : BaseViewModel
    {
        #region Properties
        private string login;
        public string Login
        {
            get => login;
            set { this.SetProperty(ref this.login, value); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { this.SetProperty(ref this.password, value); }
        }

        private bool save;
        public bool Save
        {
            get => save;
            set { this.SetProperty(ref this.save, value); }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { this.SetProperty(ref this.isLoading, value); }
        }

        private bool areCredentialsInvalid;
        public bool AreCredentialsInvalid
        {
            get => areCredentialsInvalid;
            set { this.SetProperty(ref this.areCredentialsInvalid, value); }
        }

        private ICommand logInClicked;
        public ICommand LogInClicked
        {
            get
            {
                return logInClicked ?? (logInClicked = new Command(async () => await LogInMethod()));
            }
        }

        #endregion

        #region Constructors
        public LogInViewModel()
        {
        }

        #endregion

        #region Methods
        private async Task LogInMethod()
        {
            if (IsLoading)
                return;
            IsLoading = true;
            Users user = new Users();
            try
            {
                AreCredentialsInvalid = true;
                if (!String.IsNullOrWhiteSpace(Login))
                {
                    if (!String.IsNullOrWhiteSpace(Password))
                    {

                        Request r = new Request();
                        r.GetUser(out user, Login);
                        if (user != null)
                        {
                            if (user.Password == Password)
                                AreCredentialsInvalid = false;
                        }
                        r.GetAllGroup(out App.allGroups);
                    }
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("", "Błąd logowania. Spróbuj ponownie", "OK");
            }
            finally
            {
                if (!AreCredentialsInvalid)
                {
                    if (Save)
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("userName", Login);
                        await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                    }
                    await App.UserDatabase.SaveItemAsync(user);
                    List<Group> groups = await App.UserGroupDatabase.GetItemsAsync();
                    foreach (Group group in groups)
                        await App.UserGroupDatabase.DeleteItemAsync(group);
                    foreach (Group group in user.Groups)
                        await App.UserGroupDatabase.SaveItemAsync(group);
                    IsLoading = false;
                    Application.Current.MainPage = new AppShell();
                }
            }
            IsLoading = false;
        }
        #endregion
    }
}