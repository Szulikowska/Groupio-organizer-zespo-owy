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
    public partial class AddEventPage : ContentPage
    {
        
        public AddEventPage(Group g, DateTime d)
        {
            InitializeComponent();
            BindingContext = new AddEventViewModelcs(g, d);
        }

    }
}
