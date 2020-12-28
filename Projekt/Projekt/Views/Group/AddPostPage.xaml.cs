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
    public partial class AddPostPage : ContentPage
    {
        public AddPostPage()
        {
            InitializeComponent();
            BindingContext = new AddPostViewModel();
        }

        public AddPostPage(Group gr)
        {
            InitializeComponent();
            BindingContext = new AddPostViewModel(gr);
            Title = "Post w " + gr.Name;
        }
    }
}