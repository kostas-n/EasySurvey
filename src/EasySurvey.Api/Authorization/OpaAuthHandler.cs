using EasySurvey.Api.Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EasySurvey.Api.Authorization
{
	//public class JWTFaker
	//{
	//	private static string _issuer { get; } = "Easy.Authorization";
	//	private JwtSecurityTokenHandler _tokenHandler;
	//	private SymmetricSecurityKey _securityKey;
	//	private SigningCredentials _signingCredentials;

	//	public JWTFaker()
	//	{
	//		var key = new byte[32];
	//		_tokenHandler = new JwtSecurityTokenHandler();
	//		_securityKey = new SymmetricSecurityKey(key) { KeyId = Guid.NewGuid().ToString() };
	//		_signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
	//	}
	//	public string Generate(IEnumerable<Claim> claims)
	//	{
	//		return _tokenHandler.WriteToken(new JwtSecurityToken(_issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), _signingCredentials));
	//	}
	//}

	//public class JWTAccessor
	//{
	//	public string GetRequestJWT()
	//	{
	//		//var rawJwt = httpContext.Request.Headers["Authorization"];
	//		var jwtFaker = new JWTFaker();
	//		return jwtFaker.Generate(new[] {
	//			new Claim(JwtRegisteredClaimNames.GivenName, "Test user"),
	//			new Claim(JwtRegisteredClaimNames.Sub , "999999"),
	//		});

	//	}
	//}

	public class OpaAuthHandler : AuthorizationHandler<OpaPolicyRequirement>
	{
		private IHttpClientFactory _clientFactory;
		private IHttpContextAccessor _accessor;

		public OpaAuthHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_clientFactory = httpClientFactory;
			_accessor = httpContextAccessor;
		}


		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OpaPolicyRequirement requirement)
		{
			// Don't bother trying to authorize if the user is not authenticated
			if (context.User.Identity.IsAuthenticated)
			{
				// Retrieve any parameters from routing configurations
				var httpContext = _accessor.HttpContext;

				// Instantiate the request to query OPA
				var authHeader = httpContext.Request.Headers["Authorization"].ToString();
				var inputRequest = new
				{
					Input = new InputRequest()
					{
						RawJwt = authHeader.Substring("bearer ".Length),
						Subject = context.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value
					}
				};

				var routes = httpContext.GetRouteData();

				routes.Values.TryGetValue("responseId", out object responseId);
				//inputRequest.Input.Attribute = new List<string>() { responseId.ToString() };
				inputRequest.Input.ResourceId = responseId.ToString();
				inputRequest.Input.Permission = requirement.Permission;
				inputRequest.Input.TenantId = Guid.NewGuid().ToString();

				// Package input request and send to Opa for decision
				var inputString = JsonSerializer.Serialize(inputRequest, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				var input = new StringContent(inputString, Encoding.UTF8, "application/json");

				var client = _clientFactory.CreateClient();
				var response = await client.PostAsync($"{requirement.PolicyUrl}/accessControl/surveyResponses", input);

				// Call succeed if decision is true
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					var decision = JsonSerializer.Deserialize<DecisionMessage>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
					if (decision.Result.Allow)
					{
						// Authorized to proceed.
						context.Succeed(requirement);
					}
				}
			}
		}
	}
}
