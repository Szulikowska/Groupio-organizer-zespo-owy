using Projekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    class RequestMemberViewModel : BaseViewModel
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
                return reloadMembers ?? (reloadMembers = new Command(() => GetRequest()));
            }
        }
        public RequestMemberViewModel(Group grp)
        {
            Title = "Requested";
            UserGroup = grp;
            GetRequest();
        }

        void GetRequest()
        {
            IsBusy = true;
            Task T = Task.Run(async () =>
            {
                try
                {
                    this.Members = await Request.GetRequest(UserGroup);
                    IsBusy = false;
                }
                finally { }
            });
        }
    }
}
