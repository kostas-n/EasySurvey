using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.ReadModels
{
	public class ResponseBasicView
	{
		public string ResponseId { get; set; }
		public string SurveyId { get; set; }
		public DateTime? LastUpdatedDtm { get; set; }
		public int AnswersCount { get; set; }
		public DateTime CreatedDtm { get; set; }
		public DateTime? SubmittedDtm { get; set; }
		public string UserId { get; set; }
	}
}
