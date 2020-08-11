using DejtingSidan.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Reflection;

namespace DejtingSidan.Controllers
{
    public class HomeController : Controller
    {


        private ApplicationUserManager _userManager;

        public

        OwnContext DbManager
        { get; set; } = new OwnContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /*public ActionResult Index(string searchText)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<bool> isFriends = new List<bool>();
            List<string> filePaths = new List<string>();
            if (Request.IsAuthenticated)
            {
                var currUser = UserManager.FindById(User.Identity.GetUserId());
                //List<string> extensions = new List<string> { ".png", ".jpg", ".jpeg", ".gif" };
                var searchedUsers = UserManager.Users.Where(u => u.Id != currUser.Id && (u.FirstName.Contains(searchText) || u.LastName.Contains(searchText)));
                foreach (var user in searchedUsers)
                {
                    bool existAsFriend = DbManager.Friends.Any(f => (f.User1Id == user.Id && f.User2Id == currUser.Id)
                        || (f.User2Id == user.Id && f.User1Id == currUser.Id));
                    bool existAsRequest = DbManager.FriendRequests.Any(f => (f.UserSentId == user.Id && f.UserReceivedId == currUser.Id)
                        || (f.UserReceivedId == user.Id && f.UserSentId == currUser.Id));
                    isFriends.Add(existAsFriend || existAsRequest);
                    users.Add(user);
                    var file = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles")).Where(f => f.Contains(user.Id));
                    if (file.Count() != 0)
                    {
                        var filePath = file.First().Split('\\');
                        var fileName = filePath[filePath.Length - 1];
                        filePaths.Add(fileName);
                    }
                    else
                    {
                        filePaths.Add("default_profile.jpg");
                    }
                }
            }
            HomeViewModel model = new HomeViewModel();
            model.users = users;
            model.filePath = filePaths;
            model.isFriends = isFriends;
            return View(model);
        }*/
        public ActionResult Index(string searchText)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<bool> isFriends = new List<bool>();
            List<string> filePaths = new List<string>();
            int usersAdded = 0;
            if (Request.IsAuthenticated)
            {
                var currUser = UserManager.FindById(User.Identity.GetUserId());
                IQueryable<ApplicationUser> userList;
                if (!String.IsNullOrEmpty(searchText))
                    userList = UserManager.Users.Where(u => u.Id != currUser.Id && (u.FirstName.Contains(searchText) || u.LastName.Contains(searchText)));
                else
                    userList = UserManager.Users.Where(u => u.Id != currUser.Id);
                foreach (var user in userList)
                {
                    bool existAsFriend = DbManager.Friends.Any(f => (f.User1Id == user.Id && f.User2Id == currUser.Id)
                        || (f.User2Id == user.Id && f.User1Id == currUser.Id));
                    bool existAsRequest = DbManager.FriendRequests.Any(f => (f.UserSentId == user.Id && f.UserReceivedId == currUser.Id)
                        || (f.UserReceivedId == user.Id && f.UserSentId == currUser.Id));
                    isFriends.Add(existAsFriend || existAsRequest);
                    users.Add(user);
                    usersAdded++;
                    var file = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles")).Where(f => f.Contains(user.Id));
                    if (file.Count() != 0)
                    {
                        var filePath = file.First().Split('\\');
                        var fileName = filePath[filePath.Length - 1];
                        filePaths.Add(fileName);
                    }
                    else
                    {
                        filePaths.Add("default_profile.jpg");
                    }
                }
            }
            HomeViewModel model = new HomeViewModel();
            model.users = users;
            model.filePath = filePaths;
            model.isFriends = isFriends;
            return View(model);
        }

        public ActionResult AddFriend(string id)
        {
            var friendRequest = new FriendRequests();
            friendRequest.UserReceivedId = id;
            friendRequest.UserSentId = User.Identity.GetUserId();

            DbManager.FriendRequests.Add(friendRequest);
            DbManager.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AcceptFriend(string id)
        {
            var friends = new Friends();
            friends.User1Id = id;
            friends.User2Id = User.Identity.GetUserId();

            DbManager.Friends.Add(friends);
            var friendRequest = DbManager.FriendRequests.First(r => r.UserReceivedId == friends.User2Id && r.UserSentId == friends.User1Id);
            DbManager.FriendRequests.Remove(friendRequest);
            DbManager.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeclineFriend(string id)
        {
            var friends = new Friends();
            friends.User1Id = id;
            friends.User2Id = User.Identity.GetUserId();
            var friendRequest = DbManager.FriendRequests.First(r => r.UserReceivedId == friends.User2Id && r.UserSentId == friends.User1Id);
            DbManager.FriendRequests.Remove(friendRequest);
            DbManager.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}