using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Projekt.ViewModels;
using Projekt.Models;

namespace Projekt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarDayPage : ContentPage
    {
        DateTime Date { get; set; }
        CalendarViewModel Kalendarz { get; set; }
        Group UserGroup { get; set; }
        public CalendarDayPage(CalendarViewModel K, DateTime d, Group g)
        {
            InitializeComponent();
            this.BindingContext = Kalendarz = K;
            Date = d;
            UserGroup = g;
            Kalendarz.SetDay(d);
            MessagingCenter.Subscribe<CalendarViewModel>(this, "NewEvent", async (NewPost) =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddEventPage(UserGroup, Date));
        }
    }
}
