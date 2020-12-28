using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Projekt.Models;
using Projekt.Views;
using Projekt.ViewModels;
using System.Windows.Input;

namespace Projekt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllGroupPage : ContentPage
    {
        AllGroupViewModel vm;
        public AllGroupPage()
        {
            vm = new AllGroupViewModel();
            InitializeComponent();
            this.BindingContext = vm;
        }

        async void AddGroup_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"addGroup");
            Shell.Current.FlyoutIsPresented = false;
        }

        async void GroupsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var group = e.SelectedItem as Group;
            if (group == null)
                return;

            await Application.Current.MainPage.Navigation.PushAsync(new GroupPage(group));
            // Manually deselect item.
            GroupsListView.SelectedItem = null;
        }

        private void DeleteFromGroup(object sender, EventArgs e)
        {

        }
    }
}