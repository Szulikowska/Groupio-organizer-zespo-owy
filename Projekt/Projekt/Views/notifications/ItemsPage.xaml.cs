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

namespace Projekt.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            viewModel = new ItemsViewModel();
            InitializeComponent();

            BindingContext = viewModel;
        }

       /* async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Post3;
            if (item == null)
                return;
            if (item.TextMode == 0)
                item.TextMode = 1;
            else
                item.TextMode = 0;
            //args.SelectedItemIndex

            // Manually deselect item.
            GroupsListView.BeginRefresh();
            GroupsListView.SelectedItem = null;
        }*/
    }
}