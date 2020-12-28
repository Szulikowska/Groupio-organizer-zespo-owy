using Projekt.Models;
using Projekt.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projekt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestedMembersPage : ContentPage
    {
        public Group UserGroup { get; set; }
        public RequestedMembersPage(Group grp)
        {
            var vm = new RequestMemberViewModel(grp);
            InitializeComponent();
            UserGroup = grp;
            this.BindingContext = vm;
        }

        private bool isLoading;
        public bool IsLoading
        {
            get; set;
        }

        async void MembersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var users = e.SelectedItem as Users;
            if (users == null)
                return;
            if (IsLoading)
                return;
            IsLoading = true;
            bool result = await Shell.Current.DisplayAlert("", "Chcesz dodać użytkownika?", "Tak", "Nie");
            if (result)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        Request r = new Request();
                        var k = users;
                        if (r.AddUser(UserGroup, users))
                        {
                            MessagingCenter.Send(this, "NewUser");
                        }
                    }
                    finally { }
                });
            }
            await Application.Current.MainPage.Navigation.PopAsync();
            MembersListView.SelectedItem = null;
            IsLoading = false;
        }
    }
}
