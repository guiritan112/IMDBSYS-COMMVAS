using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CommVas.Controllers
{
    [Authorize(Roles = "User,Manager")]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            //var list = new List<user>();
            //using (var db = new dbsys32Entities())
            //{
            //    list = db.user.ToList();
            //}
            //    return View(list);
            return View(userRepo.GetAll());
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserInfo u)
        {
            var _user = userRepo.Table.Where(m => m.username == u.username).FirstOrDefault();
            if (_user != null)
            {
                FormsAuthentication.SetAuthCookie(u.username, false);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "User not Exist or Incorrect Password");
            return View(u);
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo u)
        {
            using (var db = new CommVasEntities())
            {
                var Newuser = new UserInfo();
                Newuser.username = u.username;
                Newuser.password = u.password;

                db.UserInfo.Add(Newuser);
                db.SaveChanges();

                TempData["msg"] = $"Added {Newuser.username} Successfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var u = new UserInfo();
            using (var db = new CommVasEntities())
            {
                u = db.UserInfo.Find(id);
            }
            return View(u);
        }
        [HttpPost]
        public ActionResult Update(UserInfo u)
        {
            using (var db = new CommVasEntities())
            {
                var Newuser = db.UserInfo.Find(u.id);
                Newuser.username = u.username;
                Newuser.password = u.password;

                db.UserInfo.Add(Newuser);
                db.SaveChanges();

                TempData["Msg"] = $"Updated {Newuser.username} Successfully";
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            var u = new UserInfo();
            using (var db = new CommVasEntities())
            {
                u = db.UserInfo.Find(id);
                db.UserInfo.Remove(u);
                db.SaveChanges();

                TempData["Msg"] = $"Deleted {u.username} Successfully";
                return RedirectToAction("Index");
            }


        }
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            return View(userRepo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(UserInfo u)
        {
            userRepo.Update(u.id, u);
            TempData["Msg"] = $"User {u.username} updated!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(userRepo.Get(id));
        }
    }
}