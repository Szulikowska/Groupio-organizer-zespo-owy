using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Projekt.Services;
using Projekt.Views;
using Projekt.ViewModels;
using Projekt.Models;
using Projekt.Dictionary;
using Projekt.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projekt
{
    public partial class App : Application
    {

        public App()
        {

            InitializeComponent();
            AppResources.Culture = new System.Globalization.CultureInfo("pl");
            DependencyService.Register<MockDataStore>();
            RegisteringRoutes();
            var isLogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            if (isLogged == "1")
            {

                MainPage = new AppShell();
            }
            else
            {
                Task t = Task.Run(async () =>
                {
                    await App.UserDatabase.DeleteItemAsync(await App.UserDatabase.GetItemAsync(0)); ;
                });
                MainPage = new NavigationPage(new LogInPage()) { BarBackgroundColor = Xamarin.Forms.Color.FromHex("#724F7B") };
            }
        }

        protected void RegisteringRoutes()
        {
            Routing.RegisterRoute("addGroup", typeof(AddGroupPage));
            Routing.RegisterRoute("allGroup", typeof(AllGroupPage));
            Routing.RegisterRoute("menuGroup", typeof(GroupPage));
            Routing.RegisterRoute("boardGroup", typeof(BoardGroupPage));
        }

        public static List<Group> allGroups;
        public static List<Group> AllGroups
        {
            get
            {
                if (allGroups == null)
                    allGroups = new List<Group>();
                return allGroups;
            }
            set
            {
                allGroups = value;
            }
        }

        static UserDatabase userDatabase;
        public static UserDatabase UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabase();
                }
                return userDatabase;
            }
        }

        static UserGroupDatabase userGroupDatabase;
        public static UserGroupDatabase UserGroupDatabase
        {
            get
            {
                if (userGroupDatabase == null)
                {
                    userGroupDatabase = new UserGroupDatabase();
                }
                return userGroupDatabase;
            }
        }

        static GroupDatabase groupDatabase;
        public static GroupDatabase GroupDatabase
        {
            get
            {
                if (groupDatabase == null)
                {
                    groupDatabase = new GroupDatabase();
                }
                return groupDatabase;
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
