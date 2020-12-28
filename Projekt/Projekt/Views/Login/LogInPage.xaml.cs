using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projekt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        LogInViewModel vm;
        public LogInPage(LogInViewModel viewModel)
        {
            InitializeComponent();
            if (viewModel == null)
                viewModel = new LogInViewModel();
            BindingContext = this.vm = viewModel;
        }

        public LogInPage()
        {
            InitializeComponent();
            BindingContext = new LogInViewModel();
        }

        async void Registration_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}