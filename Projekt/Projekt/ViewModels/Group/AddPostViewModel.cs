using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    class AddPostViewModel : BaseViewModel
    {
        #region Properties
        private Group userGroup;
        public Group UserGroup
        {
            get => userGroup;
            set { this.SetProperty(ref this.userGroup, value); }
        }

        private string postTitle;
        public string PostTitle
        {
            get => postTitle;
            set { this.SetProperty(ref this.postTitle, value); }
        }

        private string content;
        public string Content
        {
            get => content;
            set { this.SetProperty(ref this.content, value); }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { this.SetProperty(ref this.isLoading, value); }
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
        #endregion
        #region Constructors
        public AddPostViewModel()
        {

        }

        public AddPostViewModel(Group gr)
        {
            UserGroup = gr;
        }


        #endregion

        private async Task AddPostMethod()
        {
            if (isLoading)
                return;
            isLoading = true;
            PostAdded = false;
            if (!String.IsNullOrWhiteSpace(PostTitle))
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        Users usr = await App.UserDatabase.GetItemAsync(0);
                        uint k = 111;
                        Post tmp = new Post 
                        {
                            Title = PostTitle,
                            Content = Content,
                            User = usr.Login,
                            Date = k
                        };
                        Request r = new Request();
                        if (r.AddPost(UserGroup, tmp))
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
                    await Application.Current.MainPage.DisplayAlert("", "Dodano post", "OK");
                    MessagingCenter.Send(this, "NewPost");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("", "Błąd", "OK");
            }
            else
                await Application.Current.MainPage.DisplayAlert("","Tytuł nie może być pusty", "OK");
            isLoading = false;
        }

    }
}
