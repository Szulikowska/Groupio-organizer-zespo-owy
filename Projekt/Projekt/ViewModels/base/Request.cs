using Newtonsoft.Json;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public class Request : Api
    {
        #region ADRES REQUEST
        const string Request_User = "/user";
        const string Request_UserListGroup = "/user/listGroups";
        const string Request_UserListPost = "/user/listPosts";

        const string Request_Group = "/group";
        const string Request_GroupUser = "/group/user";
        const string Request_GroupList = "/group/list";
        const string Request_GroupListUser = "/group/listUsers";
        const string Request_GroupReqested = "/group/requested";
        const string Request_GroupBanned = "/group/banned";
        const string Request_GroupPost = "/group/post";
        const string Request_GroupEvent = "/group/event";
        #endregion

        #region METHOD
        const string GET = "GET";
        const string POST = "POST";
        const string PUT = "PUT";
        const string DELETE = "DELETE";
        #endregion

        #region CONST
        const string TRUE = "true";
        const string EMPTY = "{}";
        #endregion

        #region GET

        #region SYNCHRONICZNE

        /// <summary>
        /// POBIERANIE LISTY GROUP
        /// </summary>
        /// <param name="groups"></param>
        public void GetAllGroup(out List<Group> group)
        {
            group = JsonConvert.DeserializeObject<List<Group>>(Execute(GET, Request_GroupList));
        }
        /// <summary>
        /// POBIERANIE LISTY GROUP URZYTKOWNIKA
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        public void GetListGroup(out List<Group> group, Users user)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", user.Login);

            group = JsonConvert.DeserializeObject<List<Group>>(Execute(GET, Request_UserListGroup, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE URZYTKOWNIKÓW W GRUPIE
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groups"></param>
        public void GetListUser(out List<Users> user, Group groups)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", groups.Id);

            user = JsonConvert.DeserializeObject<List<Users>>(Execute(GET, Request_GroupListUser, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE DANYCH UŻYTKOWNIKA
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userLogin"></param>
        public void GetUser(out Users user, string userLogin)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", userLogin);

            user = JsonConvert.DeserializeObject<Users>(Execute(GET, Request_User, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE DANYCH GRUPY
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="group"></param>
        public void GetGroup(out Groups groups, Group group)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", group.Id);

            groups = JsonConvert.DeserializeObject<Groups>(Execute(GET, Request_Group, RequestHeaders: temp));
        }
        public void GetRequest(out List<Users> user, Group groups)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", groups.Id);

            user = JsonConvert.DeserializeObject<List<Users>>(Execute(GET, Request_GroupReqested, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE POSTÓW Z GRUPY
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="group"></param>
        /// <param name="users"></param>
        public void GetPost(out List<Post> posts, Group group, Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", group.Id);
            temp.Add("userlogin", users.Login);

            posts = JsonConvert.DeserializeObject<List<Post>>(Execute(GET, Request_GroupPost, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE EVENTÓW Z GRUPY
        /// </summary>
        /// <param name="events"></param>
        /// <param name="group"></param>
        /// <param name="users"></param>
        public void GetEvent(out List<Event> events, Group group, Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", group.Id);
            temp.Add("userlogin", users.Login);

            events = JsonConvert.DeserializeObject<List<Event>>(Execute(GET, Request_GroupEvent, RequestHeaders: temp));
        }
        /// <summary>
        /// Zapytanie zwracające wszystkie posty
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="users"></param>
        public void GetListPost(out List<Post2> posts, Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", users.Login);

            posts = JsonConvert.DeserializeObject<List<Post2>>(Execute(GET, Request_UserListPost, RequestHeaders: temp));
        }
        /// <summary>
        /// Zapytanie zwracające wszystkie posty
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="users"></param>
        public void GetListPost(out List<Post3> posts, Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", users.Login);

            var result = JsonConvert.DeserializeObject<List<Post2>>(Execute(GET, Request_UserListPost, RequestHeaders: temp));

            posts = new List<Post3>();
            foreach (var x in result)
            {
                if (result.Count != 0)
                    foreach (var y in x.Posts)
                    {
                        posts.Add(new Post3() { Posts = y, Name = x.Name });
                    }
            }
        }
        /// <summary>
        /// Zapytanie zwracające wszystkie posty
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="users"></param>
        public void GetListPost(out List<Groups> groups, Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", users.Login);

            groups = JsonConvert.DeserializeObject<List<Groups>>(Execute(GET, Request_UserListPost, RequestHeaders: temp));
        }

        #endregion

        #region ASYNCHRONICZNE
        /// <summary>
        /// POBIERANIE LISTY GROUP
        /// </summary>
        /// <param name="groups"></param>
        async static public Task<List<Group>> GetAllGroup()
        {
            return JsonConvert.DeserializeObject<List<Group>>(await ExecuteAsync(GET, Request_GroupList));
        }
        /// <summary>
        /// POBIERANIE LISTY GROUP URZYTKOWNIKA
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        async static public Task<List<Group>> GetListGroup(Users user)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", user.Login);

            return JsonConvert.DeserializeObject<List<Group>>(await ExecuteAsync(GET, Request_UserListGroup, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE URZYTKOWNIKÓW W GRUPIE
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groups"></param>
        async static public Task<List<Users>> GetListUser(Group groups)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", groups.Id);

            return JsonConvert.DeserializeObject<List<Users>>(await ExecuteAsync(GET, Request_GroupListUser, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE DANYCH UŻYTKOWNIKA
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userLogin"></param>
        async static public Task<Users> GetUser(string userLogin)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", userLogin);

            return JsonConvert.DeserializeObject<Users>(await ExecuteAsync(GET, Request_User, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE DANYCH GRUPY
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="group"></param>
        async public Task<Groups> GetGroupAsync(Group group)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", group.Id);

            return JsonConvert.DeserializeObject<Groups>(await ExecuteAsync(GET, Request_Group, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE GRUPY OCZEKUJĄCEJ
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        async static public Task<List<Users>> GetRequest(Group groups)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", groups.Id);

            return JsonConvert.DeserializeObject<List<Users>>(await ExecuteAsync(GET, Request_GroupReqested, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE POSTÓW Z GRUPY
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="group"></param>
        /// <param name="users"></param>
        async static public Task<List<Post>> GetPost(Group group, Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", group.Id);
            temp.Add("userlogin", users.Login);

            return JsonConvert.DeserializeObject<List<Post>>(await ExecuteAsync(GET, Request_GroupPost, RequestHeaders: temp));
        }
        /// <summary>
        /// POBIERANIE EVENTÓW Z GRUPY
        /// </summary>
        /// <param name="events"></param>
        /// <param name="group"></param>
        /// <param name="users"></param>
        async static public Task<List<Event>> GetEvent(Group group, Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", group.Id);
            temp.Add("userlogin", users.Login);

            return JsonConvert.DeserializeObject<List<Event>>(await ExecuteAsync(GET, Request_GroupEvent, RequestHeaders: temp));
        }

        /// <summary>
        /// Zapytanie zwracające wszystkie posty
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="users"></param>
        async static public Task<List<Post2>> GetListPost1(Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", users.Login);

            return JsonConvert.DeserializeObject<List<Post2>>(await ExecuteAsync(GET, Request_UserListPost, RequestHeaders: temp));
        }

        /// <summary>
        /// Zapytanie zwracające wszystkie posty
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="users"></param>
        async static public Task<List<Post3>> GetListPost2(Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", users.Login);

            var result = JsonConvert.DeserializeObject<List<Post2>>(await ExecuteAsync(GET, Request_UserListPost, RequestHeaders: temp));

            var posts = new List<Post3>();
            foreach (var x in result)
            {
                if (result.Count != 0)
                    foreach (var y in x.Posts)
                    {
                        posts.Add(new Post3() { Posts = y, Name = x.Name });
                    }
            }
            return posts;
        }

        /// <summary>
        /// Zapytanie zwracające wszystkie posty
        /// </summary>
        /// <param name="posts"></param>
        /// <param name="users"></param>
        async static public Task<List<Groups>> GetListPost3(Users users)
        {

            NameValueCollection temp = new NameValueCollection();
            temp.Add("userlogin", users.Login);

            return JsonConvert.DeserializeObject<List<Groups>>(await ExecuteAsync(GET, Request_UserListPost, RequestHeaders: temp));
        }

        #endregion

        #endregion

        #region POST
        /// <summary>
        /// TWORZENIE URZYTKOWNIKA
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public bool NewUsers(Users newUser)
        {
            if (newUser.Birth_date == null)
                newUser.Birth_date = "";
            if (newUser.City == null)
                newUser.City = "";
            if (newUser.Email == null)
                newUser.Email = "";
            if (newUser.Login == null)
                newUser.Login = "";
            if (newUser.Name == null)
                newUser.Name = "";
            if (newUser.Password == null)
                newUser.Password = "";
            if (newUser.Sex == null)
                newUser.Sex = "";
            if (newUser.Surname == null)
                newUser.Surname = "";

            var temp = new
            {
                birth_date = newUser.Birth_date,
                city = newUser.City,
                email = newUser.Email,
                login = newUser.Login,
                name = newUser.Name,
                password = newUser.Password,
                sex = newUser.Sex,
                surname = newUser.Surname,
                groups = newUser.Groups
            };

            string request = Execute(POST, Request_User, ObjJson: temp);

            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }
        /// <summary>
        /// TWORZENIE GRUPY
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool NewGroup(Groups groups, Users user)
        {
            if (groups.Name == null)
                groups.Name = "";
            if (groups.Year == null)
                groups.Year = "";
            if (groups.School == null)
                groups.School = "";
            if (groups.Description == null)
                groups.Description = "";
            if (user.Login == null)
                user.Login = "";
            if (user.Name == null)
                user.Name = "";
            if (user.Surname == null)
                user.Surname = "";

            var temp = new
            {
                group = groups,
                userLogin = user.Login,
                userName = user.Name,
                userSurname = user.Surname,
                descrition = groups.Description
            };

            var request = Execute(POST, Request_Group, ObjJson: temp);

            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }
        /// <summary>
        /// DODAWANIE UŻYTKOWNIKA DO GRUPY 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool AddUser(Group groups, Users users)
        {
            if (groups.Id == null)
                groups.Id = "";
            if (groups.Name == null)
                groups.Name = "";
            if (users.Name == null)
                users.Name = "";
            if (users.Surname == null)
                users.Surname = "";
            if (users.Login == null)
                users.Login = "";

            var temp = new
            {
                groupID = groups.Id,
                groupName = groups.Name,
                userName = users.Name,
                userSurname = users.Surname,
                userLogin = users.Login
            };

            var request = Execute(POST, Request_GroupUser, ObjJson: temp);

            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }
        /// <summary>
        /// DODADAWANIE DO LISTY OCZEKUJĄCEJ DO GRUPY
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool AddRequest(Group groups, Users users)
        {
            var temp = new
            {
                groupID = groups.Id,
                user = new
                {
                    name = users.Name,
                    surname = users.Surname,
                    login = users.Login
                }
            };

            var request = Execute(POST, Request_GroupReqested, ObjJson: temp);

            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Dodaj do zbanowaych
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool AddBanned(Group groups, Users users)
        {
            var temp = new
            {
                groupID = groups.Id,
                userLogin = users.Login
            };

            var request = Execute(POST, Request_GroupBanned, ObjJson: temp);

            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }

        /// <summary>
        /// dodaj post
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool AddPost(Group groups, Post post)
        {
            var temp = new
            {
                groupID = groups.Id,
                post = post
            };

            var request = Execute(POST, Request_GroupPost, ObjJson: temp);
            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }

        /// <summary>
        /// dodaj event
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="newevent"></param>
        /// <returns></returns>
        public bool AddEvent(Group groups, Event newevent)
        {
            var temp = new
            {
                groupID = groups.Id,
                Event = newevent
            };

            var request = Execute(POST, Request_GroupEvent, ObjJson: temp);
            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }
        #endregion

        #region PUT

        /// <summary>
        /// zamien dane user
        /// </summary>
        /// <param name="olduser"></param>
        /// <param name="newuser"></param>
        /// <returns></returns>
        public bool ChangeUser(Users olduser, Users newuser)
        {
            if (!newuser.Login.Equals(olduser.Login))
                newuser.Login = olduser.Login;

            var temp = new
            {
                userLogin = olduser.Login,
                user = newuser
            };


            var request = Execute(PUT, Request_User, ObjJson: temp);
            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }

        /// <summary>
        /// zmień dane grupy
        /// </summary>
        /// <param name="oldGroup"></param>
        /// <param name="newGroup"></param>
        /// <returns></returns>
        public bool ChangeGroup(Group oldGroup, Groups newGroup)
        {
            
            var temp = new
            {
                groupID = oldGroup.Id,
                group = new
                {
                    name = newGroup.Name,
                    school = newGroup.School
                }
            };

            var request = Execute(PUT, Request_Group, ObjJson: temp);
            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }

        #endregion

        #region DELETE 

        /// <summary>
        /// usuń grupe
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool DeleteUser(Group groups, Users users)
        {
            NameValueCollection temp = new NameValueCollection();
            temp.Add("groupid", groups.Id);
            temp.Add("userlogin", users.Login);

            var request = Execute(DELETE, Request_GroupUser, RequestHeaders: temp);
            if (request.Equals(TRUE))
                return true;
            else
                return false;
        }

        #endregion
    }
}
