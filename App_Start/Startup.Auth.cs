using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zaginieni.App_Start;
using Zaginieni.DAL;
using Zaginieni.Models;

namespace Zaginieni
{
	public partial class Startup
	{
		public void ConfigureAuth(IAppBuilder app)
		{
			app.CreatePerOwinContext(Baza.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
				Provider = new CookieAuthenticationProvider
				{
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
						validateInterval: TimeSpan.FromMinutes(30),
						regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
				}
			});
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
			app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
		}
	}
}