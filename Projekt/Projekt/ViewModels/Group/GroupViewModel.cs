using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        public Group Group { get; set; }
        public GroupViewModel(Group obj)
        {
            Group = obj;
            GetInfo();
        }

        private Groups gr;
        public Groups Gr
        {
            get => gr;
            set
            {
                gr = value;
                this.SetProperty(ref this.gr, value);
            }
        }

        public GroupViewModel() 
        {
        }


        public void GetInfo()
        {
            Task t = Task.Run(async () =>
            {
                Groups temp;
                Request r = new Request();
                temp = await r.GetGroupAsync(Group);
                Gr = temp;
            });
        }
    }
}
