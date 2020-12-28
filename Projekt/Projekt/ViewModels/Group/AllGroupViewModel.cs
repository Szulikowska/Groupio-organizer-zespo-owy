using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    class AllGroupViewModel : BaseViewModel
    {
        private List<Group> userGroups;
        public List<Group> UserGroups
        { 
            get => userGroups;
            set { this.SetProperty(ref this.userGroups, value); }
        }
        private ICommand reloadGroups;
        public ICommand ReloadGroups
        {
            get
            {
                return reloadGroups ?? (reloadGroups = new Command(() => GetGroups()));
            }
        }

        public AllGroupViewModel()
        {
            GetGroups();

            MessagingCenter.Subscribe<AddGroupViewModel>(this, "NewGroup", (NewGroup) =>
            {
                GetGroups();
            });
            MessagingCenter.Subscribe<EditGroupModel>(this, "EditGroup", (NewGroup) =>
            {
                GetGroups();
            });
        }

        public void GetGroups()
        {
            IsBusy = true;
            Task T = Task.Run(async () =>
            {
                UserGroups = await Request.GetListGroup(await App.UserDatabase.GetItemAsync(0));
                IsBusy = false;
                //UserGroups = await App.UserGroupDatabase.GetItemsAsync();
            });
        }

        public void RemoveThisGroup()
        {

        }
    }
}
