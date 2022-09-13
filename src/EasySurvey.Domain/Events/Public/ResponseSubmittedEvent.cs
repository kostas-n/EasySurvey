using EasySurvey.Common;
using EasySurvey.Domain.WriteModels.Responses;
using System;

namespace EasySurvey.Domain.Events.Public
{
	public class ResponseSubmittedEvent
	{
		public Guid ResponseId { get; set; }
		public Guid SurveyId { get; set; }
		public DateTime SubmittedDate { get; set; }
	}
}
