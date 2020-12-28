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
    public partial class BoardGroupPage : ContentPage
    {
        public BoardGroupPage(Group gr)
        {
            var vm = new BoardGroupViewModel(gr);
            InitializeComponent();
            this.BindingContext = vm;
        }

        private void BoardListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}