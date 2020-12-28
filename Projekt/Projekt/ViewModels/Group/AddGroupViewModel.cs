using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    class AddGroupViewModel : BaseViewModel
    {
        #region Properties
        private string name;
        public string Name
        {
            get => name;
            set { this.SetProperty(ref this.name, value); }
        }

        private string school;
        public string School
        {
            get => school;
            set { this.SetProperty(ref this.school, value); }
        }

        private string year;
        public string Year
        {
            get => year;
            set { this.SetProperty(ref this.year, value); }
        }

        private string description;
        public string Description
        {
            get => description;
            set { this.SetProperty(ref this.description, value); }
        }

        private bool nameError;
        public bool NameError
        {
            get => nameError;
            set { this.SetProperty(ref this.nameError, value); }
        }

        private bool schoolError;
        public bool SchoolError
        {
            get => schoolError;
            set { this.SetProperty(ref this.schoolError, value); }
        }

        private bool yearError;
        public bool YearError
        {
            get => yearError;
            set { this.SetProperty(ref this.yearError, value); }
        }

        private bool descriptionError;
        public bool DescriptionError
        {
            get => descriptionError;
            set { this.SetProperty(ref this.descriptionError, value); }
        }

        private ICommand newGroupClicked;
        public ICommand NewGroupClicked
        {
            get
            {
                return newGroupClicked ?? (newGroupClicked = new Command(async () => await NewGroupMethod()));
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { this.SetProperty(ref this.isLoading, value); }
        }
        #endregion

        #region Constructors
        public AddGroupViewModel()
        {
        }
        #endregion

        #region Methods
        private async Task NewGroupMethod()
        {
            if (IsLoading)
                return;
            IsLoading = true;
            NameError = SchoolError = YearError = DescriptionError = false;
            if (String.IsNullOrWhiteSpace(Name))
                NameError = true;
            if (String.IsNullOrWhiteSpace(School))
                SchoolError = true;
            if (String.IsNullOrWhiteSpace(Year))
                YearError = true;
            if (String.IsNullOrWhiteSpace(Description))
                DescriptionError = true;
            if (NameError || SchoolError || YearError|| DescriptionError)
                await App.Current.MainPage.DisplayAlert("", "Złe dane", "OK");
            else 
            {
                try
                {
                    Users user = await App.UserDatabase.GetItemAsync(0);
                    Groups newGroups = new Groups();
                    newGroups.Name = Name;
                    newGroups.School = School;
                    newGroups.Year = Year;
                    newGroups.Description = Description;
                    Request r = new Request();
                    if (r.NewGroup(newGroups, user))
                    {
                        //Group group = new Group();
                        //group.Name = newGroups.Name;
                        //await App.UserGroupDatabase.SaveItemAsync(group);
                        await App.Current.MainPage.DisplayAlert("", "Grupa założona", "OK");
                        MessagingCenter.Send(this, "NewGroup");
                        Shell.Current.SendBackButtonPressed();
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("", "Błąd połączenia", "OK");
                }
                finally { }
            }
            IsLoading = false;
        }
        #endregion
    }
}
