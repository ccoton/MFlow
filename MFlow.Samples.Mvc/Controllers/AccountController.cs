using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MFlow.Samples.Mvc.Models;
using MFlow.Core.Validation;

namespace MFlow.Samples.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var validation = new FluentValidation<LoginModel>(model);

                validation
                    .If(UsernameService.CheckUsernameExists(model.UserName))
                    .Then(() => { ModelState.AddModelError("", "The user name is already in use"); })
                    .If(UsernameService.SuggestUsernames())
                    .Then(() => { ModelState.AddModelError("", string.Format("Try {0}", UsernameService.SuggestUsername(model.UserName))); });

                if (ModelState.IsValid)
                    return RedirectToLocal(returnUrl);
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }

    class UsernameService
    {

        public static bool CheckUsernameExists(string username)
        {
            return true;
        }

        public static bool SuggestUsernames()
        {
            return false;
        }

        public static string SuggestUsername(string username)
        {
            return string.Format("{0}.blah", username);
        }

    }

}