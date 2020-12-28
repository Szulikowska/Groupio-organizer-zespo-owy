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
    public partial class EditGroupPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public EditGroupPage(Group gr)
        {
            InitializeComponent();
            BindingContext = new EditGroupModel(gr);
            Title = "Edytuj Dane";
        }
    }
}

