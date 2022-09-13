using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Authorization
{
	public class OpaPolicyRequirement : IAuthorizationRequirement
	{
		public string PolicyUrl { get; }
		public string PolicyName { get; }

		public string Permission { get; }

		public OpaPolicyRequirement(string policyUrl, string policyName, string permission)
		{
			PolicyUrl = policyUrl;
			PolicyName = policyName;
			Permission = permission;
		}
	}
}
