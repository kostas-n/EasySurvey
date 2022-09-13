using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EasySurvey.Api.Authentication
{
	public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public const string DefaultScheme = "Easy.Authorization";

		public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var claims = new[] {
				new Claim(JwtRegisteredClaimNames.GivenName, "Test user"),
				new Claim(JwtRegisteredClaimNames.Sub , "999999"),
			};
			var identity = new ClaimsIdentity(claims, DefaultScheme);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, DefaultScheme);

			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}
