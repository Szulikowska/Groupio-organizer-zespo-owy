using Projekt.Models;
using Projekt.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    public class MembersViewModel : BaseViewModel
    {
        private List<Users> members;
        public List<Users> Members 
        {
            get => members;
            set { this.SetProperty(ref members, value); }
        }
        public Group UserGroup { get; set; }
        private ICommand reloadMembers;
        public ICommand ReloadMembers
        {
            get
            {
                return reloadMembers ?? (reloadMembers = new Command(() => GetMembers()));
            }
        }
        public MembersViewModel(Group grp)
        {
            Title = "Members";
            UserGroup = grp;
            GetMembers();
            MessagingCenter.Subscribe<RequestedMembersPage>(this, "NewUser", (NewPost) =>
            {
                GetMembers();
            });
        }

        void GetMembers()
        {
            IsBusy = true;
            Task T = Task.Run(async () =>
            {
                try
                {
                    this.Members = await Request.GetListUser(UserGroup);
                    IsBusy = false;
                }
                finally { }
            });
        }

        public MembersViewModel()
        { }
    }
}
