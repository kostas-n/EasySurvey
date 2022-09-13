using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Dtos
{
	public class NewResponseDto
	{
		public Guid SurveyId { get; set; }
		public string UserId { get; set; }
	}
}
