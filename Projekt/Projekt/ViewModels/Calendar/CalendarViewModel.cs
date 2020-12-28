using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        #region Properties
        private List<Event> events;
        public List<Event> Events
        {
            get => events;
            set { this.SetProperty(ref this.events, value); }
        }

        private List<Event> eventsDay;
        public List<Event> EventsDay
        {
            get => eventsDay;
            set { this.SetProperty(ref this.eventsDay, value); }
        }

        /*private ICommand reloadEvents;
        public ICommand ReloadEvents
        {
            get
            {
                return reloadEvents ?? (reloadEvents = new Command(() => GetEvent(refresh)));
            }
        }*/

        public Group UserGroup { get; set; }
        #endregion
        #region Constructors
        public CalendarViewModel(Group gr)
        {
            UserGroup = gr;
        }
        #endregion

        #region Methods
        public void GetEvent(Action<List<Event>> refresh)
        {
            IsBusy = true;
            Task T = Task.Run(async () =>
            {
                try
                {
                    var k = await Request.GetEvent(UserGroup, await App.UserDatabase.GetItemAsync(0));
                    refresh(k);
                    this.Events = k;
                    IsBusy = false;
                    //MessagingCenter.Send(this, "RefreshCalendar");
                    
                }
                finally { }
            });
        }

        public void SetDay(DateTime d)
        {
            if(Events != null)
                EventsDay = Events.Where(e => e.Date_from.Date.Equals(d.Date)).ToList<Event>();
        }
        #endregion
    }
}