﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.Security;
namespace MyCms.Controllers
{
    public class AccountController : Controller
    {
        private ILoginRepository loginRepository;
        MyCmsContext db = new MyCmsContext();

        public AccountController()
        {
            loginRepository = new LoginRepository(db);
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                if (loginRepository.IsExistLogin(login.UserName,login.PassWord))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "کاربری یافت نشد");
                }
            }
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}