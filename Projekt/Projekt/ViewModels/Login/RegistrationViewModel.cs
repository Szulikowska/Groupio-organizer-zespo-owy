using Projekt.Commands;
using Projekt.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Projekt.Models;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Projekt.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        #region Properties
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set { this.SetProperty(ref this.firstName, value);}
        }

        private string surname;
        public string Surname
        {
            get => surname;
            set { this.SetProperty(ref this.surname, value);}
        }

        private DatePicker birthDate;
        public DatePicker BirthDate
        {
            get => birthDate;
            set { this.SetProperty(ref this.birthDate, value); }
        }

        private bool genderFemale;
        public bool GenderFemale
        {
            get => genderFemale;
            set { this.SetProperty(ref this.genderFemale, value);
                if (GenderFemale) GenderMale = false; }
        }

        private bool genderMale;
        public bool GenderMale
        {
            get => genderMale;
            set { this.SetProperty(ref this.genderMale, value);
                if (GenderMale) GenderFemale = false;
            }
        }

        private string city;
        public string City
        {
            get => city;
            set { this.SetProperty(ref this.city, value); }
        }

        private string studies;
        public string Studies
        {
            get => studies;
            set { this.SetProperty(ref this.studies, value); }
        }

        private string login;
        public string Login
        {
            get => login;
            set { this.SetProperty(ref this.login, value); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { this.SetProperty(ref this.password, value);
                if (Password != RepeatPassword) RepeatPasswordError = true;
                else RepeatPasswordError = false;
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword;
            set { this.SetProperty(ref this.repeatPassword, value);
                if (Password != RepeatPassword) RepeatPasswordError = true;
                else RepeatPasswordError = false;
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set { this.SetProperty(ref this.email, value);
                if (Email != RepeatEmail) RepeatEmailError = true;
                else RepeatEmailError = false;
            }
        }

        private string repeatEmail;
        public string RepeatEmail
        {
            get => repeatEmail;
            set { this.SetProperty(ref this.repeatEmail, value);
                if (Email != RepeatEmail) RepeatEmailError = true;
                else RepeatEmailError = false;
            }
        }

        private bool acceptRules;
        public bool AcceptRules
        {
            get => acceptRules;
            set { this.SetProperty(ref this.acceptRules, value); }
        }

        private ICommand registrationCommand;
        public ICommand RegistrationCommand
        {
            get
            {
                return registrationCommand ?? (registrationCommand = new Command(async () => await RegistrationMethod()));
            }
        }

        private bool nameError;
        public bool NameError
        {
            get => nameError;
            set { this.SetProperty(ref this.nameError, value); }
        }

        private bool surnameError;
        public bool SurnameError
        {
            get => surnameError;
            set { this.SetProperty(ref this.surnameError, value); }
        }

        private bool dateOfBirthError;
        public bool DateOfBirthError
        {
            get => dateOfBirthError;
            set { this.SetProperty(ref this.dateOfBirthError, value); }
        }

        private bool cityError;
        public bool CityError
        {
            get => cityError;
            set { this.SetProperty(ref this.cityError, value); }
        }

        private bool nameOfSchoolError;
        public bool NameOfSchoolError
        {
            get => nameOfSchoolError;
            set { this.SetProperty(ref this.nameOfSchoolError, value); }
        }

        private bool nameOfUserError;
        public bool NameOfUserError
        {
            get => nameOfUserError;
            set { this.SetProperty(ref this.nameOfUserError, value); }
        }

        private bool passwordError;
        public bool PasswordError
        {
            get => passwordError;
            set { this.SetProperty(ref this.passwordError, value); }
        }

        private bool repeatPasswordError;
        public bool RepeatPasswordError
        {
            get => repeatPasswordError;
            set { this.SetProperty(ref this.repeatPasswordError, value); }
        }

        private bool emailError;
        public bool EmailError
        {
            get => emailError;
            set { this.SetProperty(ref this.emailError, value); }
        }

        private bool repeatEmailError;
        public bool RepeatEmailError
        {
            get => repeatEmailError;
            set { this.SetProperty(ref this.repeatEmailError, value); }
        }

        private bool registrationDone;
        public bool RegistrationDone 
        { 
            get => registrationDone; 
            set { this.SetProperty(ref this.registrationDone, value); } 
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set { this.SetProperty(ref this.isLoading, value); }
        }

        private bool userExist;
        public bool UserExist
        {
            get => userExist;
            set { userExist = value; }
        }

        private bool registationFailed;
        public bool RegistationFailed
        {
            get => registationFailed;
            set { this.SetProperty(ref this.registationFailed, value); }
        }
        #endregion

        #region Constructors
        public RegistrationViewModel()
        {
            GenderFemale = true;
        }
        #endregion

        #region Methods
        private async Task RegistrationMethod()
        {
            IsLoading = true;
            UserExist = false;
            if (CheckingData())
            {
                Users newUser = new Users
                {
                    Name = FirstName,
                    Surname = Surname,
                    //Birth_date = BirthDate.Format,
                    City = City,
                    Login = Login,
                    Password = Password,
                    Email = Email
                };
                if (GenderFemale)
                    newUser.Sex = "K";
                if (GenderMale)
                    newUser.Sex = "M";
                await Task.Run(() =>
                {
                    Request r = new Request();
                    try
                    {
                        if (r.NewUsers(newUser))
                            RegistrationDone = true;
                        else
                            UserExist = true;
                    }
                    finally
                    {
                    }
                });
            }
            else
                await App.Current.MainPage.DisplayAlert("", "Błąd rejestracji", "OK");
            if (RegistrationDone)
            {
                await App.Current.MainPage.DisplayAlert("", "Konto zarejestrowane", "OK");
                NavigationPage tmp = new NavigationPage(new LogInPage(null));
                NavigationPage.SetHasBackButton(tmp, false);
                Application.Current.MainPage = tmp;
            }
            else
            {
                if (UserExist)
                    await App.Current.MainPage.DisplayAlert("", "Użytkownik już istnieje", "OK");
            }
            RegistrationDone = false;
            IsLoading = false;
        }

        bool CheckingData()
        {
            if (String.IsNullOrWhiteSpace(FirstName))
                NameError = true;
            else
                NameError = false;

            if (String.IsNullOrWhiteSpace(Surname))
                SurnameError = true;
            else
                SurnameError = false;

            if (String.IsNullOrWhiteSpace(Login))
                NameOfUserError = true;
            else
                NameOfUserError = false;

            if (String.IsNullOrWhiteSpace(Password))
                PasswordError = true;
            else
            {
                if (Password.Length < 8)
                    PasswordError = true;
                else
                    PasswordError = false;
            }

            if (String.IsNullOrWhiteSpace(Email))
            {
                EmailError = true;
            }
            else
            { 
                try
                {
                    MailAddress m = new MailAddress(Email);

                    EmailError = false;
                }
                catch (FormatException)
                {
                    EmailError = true;
                }
            }

            if (NameError || SurnameError || DateOfBirthError || CityError || NameOfSchoolError
                || NameOfUserError || PasswordError || RepeatPasswordError || EmailError || RepeatEmailError || !AcceptRules)
            {
                RegistationFailed = true;
                return false;
            }
            RegistationFailed = false;
            return true;
        }
        #endregion
    }
}