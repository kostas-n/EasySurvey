using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Authorization
{
	public class ResponseAuthorizeAttribute: AuthorizeAttribute
	{
		public string Permission { get; set; }
		public string PolicyPath { get; set; }

		public ResponseAuthorizeAttribute()
		{
			this.Permission = null;
			this.PolicyPath = null;
		}
	}
}
