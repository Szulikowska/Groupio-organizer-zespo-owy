using Projekt.Models;
using Projekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace Projekt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        CalendarViewModel Kalendarz { get; set; } 
        Group UserGroup { get; set; }
        public CalendarPage(Group gr)
        {
            InitializeComponent();
            Kalendarz = new CalendarViewModel(gr);
            UserGroup = gr;
            Kalendarz.GetEvent(RefresshCalendar);
            MessagingCenter.Subscribe<AddEventViewModelcs>(this, "NewEvent", (NewPost) =>
            {
                Kalendarz.GetEvent(RefresshCalendar);
            });
        }

        async void CurrentDate(object sender, DateTimeEventArgs e)
        {
            var temp = Calendar.SelectedDate.Value;
            await Application.Current.MainPage.Navigation.PushAsync(new CalendarDayPage(Kalendarz , temp, UserGroup));
        }

        void RefresshCalendar(List<Event> events)
        {
            Calendar.SpecialDates.Clear();
            foreach (var x in events)
            {
                Calendar.SpecialDates.Add(new SpecialDate(x.Date_from.AddDays(0)) { TextColor = Color.LightPink, BorderColor = Color.Purple, BorderWidth = 5, Selectable = true });
            }
        }
    }
}