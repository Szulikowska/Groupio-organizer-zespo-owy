using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Projekt.Models;
using Projekt.Views;
using System.Collections.Generic;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private List<Post3> posts3;
        public List<Post3> Posts3
        {
            get => posts3;
            set { this.SetProperty(ref this.posts3, value); }
        }

        private ICommand loadPosts;
        public ICommand LoadPosts
        {
            get
            {
                return loadPosts ?? (loadPosts = new Command(() => GetPost()));
            }
        }
        public ItemsViewModel()
        {
            Title = "Groupio";

            GetPost();
            MessagingCenter.Subscribe<AddPostViewModel>(this, "NewPost", (NewPost) =>
            {
                GetPost();
            });

        }

        void GetPost()
        {
            Task T = Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;
                    this.Posts3 = await Request.GetListPost2(await App.UserDatabase.GetItemAsync(0));
                    IsBusy = false;
                }
                finally { }
            });
            IsBusy = false;
        }
    }
}