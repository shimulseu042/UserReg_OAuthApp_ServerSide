using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using Angular5UserReg.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Angular5UserReg.API.Controllers
{
    public class AccountController : ApiController
    {
		[Route("api/User/Register")]
		[HttpPost]
		[AllowAnonymous]
	    public IdentityResult Register(AccountModel model)
	    {
		    var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
			var manager = new UserManager<ApplicationUser>(userStore);

		    var user = new ApplicationUser() {Email = model.Email, UserName = model.UserName};
		    user.FirstName = model.FirstName;
		    user.LastName = model.LastName;
		    manager.PasswordValidator = new PasswordValidator() {RequiredLength = 3};
		    IdentityResult result = manager.Create(user, model.Password);
		    return result;
	    }

		[HttpGet]
		[Route("api/GetUserClaims")]
		[Authorize]
	    public AccountModel GetUserClaims()
	    {
		    var identityClaims = (ClaimsIdentity)User.Identity;
		    IEnumerable<Claim> claims = identityClaims.Claims;
		    AccountModel model = new AccountModel()
		    {
			    UserName = identityClaims.FindFirst("UserName").Value,
			    Email = identityClaims.FindFirst("Email").Value,
			    FirstName = identityClaims.FindFirst("FirstName").Value,
			    LastName = identityClaims.FindFirst("LastName").Value,
				LoggedOn = identityClaims.FindFirst("LoggedOn").Value
		    };
		    return model;
	    }
    }
}
