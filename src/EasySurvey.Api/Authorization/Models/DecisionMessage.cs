using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Authorization.Models
{
	public class DecisionMessage
	{
		public Result Result { get; set; }
	}

	public class Result
	{
		public bool Allow { get; set; }
	}
}
