using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    class AddEventViewModelcs : BaseViewModel
    {
        private Group userGroup;
        public Group UserGroup
        {
            get => userGroup;
            set { this.SetProperty(ref this.userGroup, value); }
        }

        private string eventTitle;
        public string EventTitle
        {
            get => eventTitle;
            set { this.SetProperty(ref this.eventTitle, value); }
        }

        private string content;
        public string Content
        {
            get => content;
            set { this.SetProperty(ref this.content, value); }
        }

        private ICommand addPostClicked;
        public ICommand AddPostClicked
        {
            get
            {
                return addPostClicked ?? (addPostClicked = new Command(async () => await AddPostMethod()));
            }
        }

        private bool postAdded;
        public bool PostAdded
        {
            get => postAdded;
            set { this.SetProperty(ref this.postAdded, value); }
        }

        DateTime Date { get; set; }

        public AddEventViewModelcs(Group g, DateTime d)
        {
            UserGroup = g;
            Date = d;
        }

        private async Task AddPostMethod()
        {
            PostAdded = false;
            if (!String.IsNullOrWhiteSpace(EventTitle))
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        Users usr = await App.UserDatabase.GetItemAsync(0);
                        Event tmp = new Event
                        {
                            Title = EventTitle,
                            Content = Content,
                            User = usr.Login,
                            Create_date = Date,
                            Date_from = Date,
                            Date_to = Date
                        };
                        Request r = new Request();
                        if (r.AddEvent(UserGroup, tmp))
                        {
                            PostAdded = true;
                        }
                        else
                            PostAdded = false;
                    }
                    finally { }
                });
                if (PostAdded)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Dodano event", "OK");
                    MessagingCenter.Send(this, "NewEvent");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("", "Błąd", "OK");
            }
            else
                await Application.Current.MainPage.DisplayAlert("", "Tytuł nie może być pusty", "OK");
        }
    }
}
