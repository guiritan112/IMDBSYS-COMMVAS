using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommVas.Repository;

namespace CommVas.Controllers
{
    public class BaseController : Controller
    {
        public CommVasEntities db;
        public BaseRepository<UserInfo> userRepo;
        public BaseController()
        {
            db = new CommVasEntities();
            userRepo = new BaseRepository<UserInfo>();
        }
    }
}