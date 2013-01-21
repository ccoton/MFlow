using System.Web.Mvc;
using MFlow.Samples.Mvc.Models;

namespace MFlow.Samples.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            return View(model);
        }

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
                return RedirectToLocal(returnUrl);
            }

            return View(model);
        }

        #region Helpers
        ActionResult RedirectToLocal(string returnUrl)
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

}