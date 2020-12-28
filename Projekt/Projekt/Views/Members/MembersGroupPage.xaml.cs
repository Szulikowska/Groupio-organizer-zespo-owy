using Projekt.Models;
using Projekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projekt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MembersGroupPage : ContentPage
    {
        Group UserGroup { get; set; }
        public MembersGroupPage(Group grp)
        {
            var vm = new MembersViewModel(grp);
            InitializeComponent();
            this.BindingContext = vm;
            UserGroup = grp;
        }
        
        private void OnMembersGroupSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        async void AddMembers_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RequestedMembersPage(UserGroup));
        }
    }
}