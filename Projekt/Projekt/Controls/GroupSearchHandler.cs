using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Projekt.Models;
using Projekt;
using Projekt.ViewModels;

namespace Projekt.Controls
{
    public class GroupSearchHandler : SearchHandler
    {
        private IEnumerable<Group> gr;

        public IEnumerable<Group> Gr { get => gr; set => gr = value; }

        private bool isPossibleToAdd;
        public bool IsPossibleToAdd
        {
            get;set;
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Gr
                    .Where(group => group.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<Group>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            bool result = await Shell.Current.DisplayAlert("", "Chcesz dołączyć do grupy?", "Tak", "Nie");
            if (result)
            {
                try
                {
                    isPossibleToAdd = true;
                    Group grp = (Group)item;
                    List<Group> temp = await App.UserGroupDatabase.GetItemsAsync();
                    foreach(Group grupa in temp)
                    {
                        if (grupa.Name == grp.Name)
                        {
                            await Shell.Current.DisplayAlert("", "Obecnie w tej grupie", "OK");
                            isPossibleToAdd = false;
                        }
                    }
                    if (isPossibleToAdd)
                    {
                        Request r = new Request();
                        if (r.AddRequest(grp, await App.UserDatabase.GetItemAsync(0)))
                            await Shell.Current.DisplayAlert("", "Dodano zapytanie o dołączenie", "OK");
                        else
                            await Shell.Current.DisplayAlert("", "Niepowodzenie", "OK");
                    }
                }
                finally
                { }
            }
        }
    }
}
