using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Angular5UserReg.API.Startup))]

namespace Angular5UserReg.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
			app.UseCors(CorsOptions.AllowAll);

			OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
			{
				TokenEndpointPath = new PathString("/token"),
				Provider = new ApplicationOAuthProvider(),
				AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
				AllowInsecureHttp = true
			};

			app.UseOAuthAuthorizationServer(options);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}
