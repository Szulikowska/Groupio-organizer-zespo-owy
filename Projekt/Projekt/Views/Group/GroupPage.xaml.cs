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
    public partial class GroupPage : ContentPage
    {
        GroupViewModel groupViewModel { get; set; }
        Group UserGroup { get; set; }
        public GroupPage(Group gr)
        {
            InitializeComponent();
            UserGroup = gr;
            BindingContext = new GroupViewModel(gr);
        }

        async void Board_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new BoardGroupPage(UserGroup));
        }

        async void Calendar_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CalendarPage(UserGroup));
        }
             
        async void Members_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MembersGroupPage(UserGroup));
        }

        async void Date_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditGroupPage(UserGroup));
        }
    }
}