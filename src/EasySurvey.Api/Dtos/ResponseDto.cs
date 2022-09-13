using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Dtos
{
	public class ResponseDto
	{
		public string ResponseId { get; set; }
		public string SurveyId { get; set; }
		public string UserId { get; set; }
		public DateTime? SubmittedDate { get; set; }
		public DateTime? CreatedDtm { get; set; }
		public DateTime? LastUpdatedDtm { get; set; }
		public List<AnswerDto> Answers { get; set; }
	}

}
