using Projekt.Models;
using Projekt.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    public class BoardGroupViewModel : BaseViewModel
    {
        #region Properties
        private List<Post> posts;
        public List<Post> Posts 
        {
            get => posts;
            set { this.SetProperty(ref this.posts, value); }
        }

        private ICommand addPostClicked;
        public ICommand AddPostClicked
        {
            get
            {
                return addPostClicked ?? (addPostClicked = new Command(async () => await AddPostMethod()));
            }
        }

        private ICommand refreshPosts;
        public ICommand RefreshPosts
        {
            get
            {
                return refreshPosts ?? (refreshPosts = new Command(() => GetPost()));
            }
        }
        public Group UserGroup { get; set; }
        #endregion
        #region Constructors
        public BoardGroupViewModel(Group gr)
        {
            UserGroup = gr;
            GetPost();
            Title = "Tablica " + UserGroup.Name;
            MessagingCenter.Subscribe<AddPostViewModel>(this, "NewPost", (NewPost) =>
            {
                GetPost();
            });
        }

        public BoardGroupViewModel()
        {

        }
        #endregion
        #region Methods
        public void GetPost()
        {
            IsBusy = true;
            Task T = Task.Run(async () =>
            {
                try
                {          
                    this.Posts = await Request.GetPost(UserGroup, await App.UserDatabase.GetItemAsync(0));
                    IsBusy = false;
                }
                finally { }
            });
            
        }

        private async Task AddPostMethod()
        { 
            await Application.Current.MainPage.Navigation.PushAsync(new AddPostPage(UserGroup));
        }
        #endregion
    }
}
