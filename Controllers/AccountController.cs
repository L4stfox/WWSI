using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zaginieni.App_Start;
using Zaginieni.Models;

namespace Zaginieni.Controllers
{
    public class AccountController : Controller
    {

		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

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

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, shouldLockout: false);
			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToLocal(returnUrl);
				case SignInStatus.Failure:
				default:
					ModelState.AddModelError("", "Nieprawidłowa próba logowania.");

					return View(model);
			}
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
				var result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

					return RedirectToAction("Index", "Home");
				}
				//AddErrors(result);
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home");
		}

	}
}
