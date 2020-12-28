using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projekt.ViewModels
{
    class EditGroupModel : BaseViewModel
    {
        #region
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

        public Group Groups { get; set; }

        #endregion

        public EditGroupModel(Group gr)
        {
            Groups = gr;
        }

        private async Task NewGroupMethod()
        {
            NameError = SchoolError = YearError = DescriptionError = false;
            if (String.IsNullOrWhiteSpace(School))
                SchoolError = true;
            if (String.IsNullOrWhiteSpace(Year))
                YearError = true;
            if (SchoolError || YearError)
                await App.Current.MainPage.DisplayAlert("", "Złe dane", "OK");
            else
            {
                try
                {
                    Groups newGroups = new Groups();
                    newGroups.Name = Groups.Name;
                    newGroups.School = School;
                    newGroups.Year = Year;
                    newGroups.Description = Groups.Description;
                    Request r = new Request();
                    if (r.ChangeGroup(Groups, newGroups))
                    {
                        await App.Current.MainPage.DisplayAlert("", "Zmieniono Dane W Grupie", "OK");
                        MessagingCenter.Send(this, "EditGroup");
                        Shell.Current.SendBackButtonPressed();
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("", "Błąd połączenia", "OK");
                }
                finally { }
            }
        }
    }
}
