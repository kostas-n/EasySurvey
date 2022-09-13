using EasySurvey.Common;
using EasySurvey.Domain.WriteModels.Responses;
using System;

namespace EasySurvey.Domain.Events.Public
{
	public class ResponseCreatedEvent
	{
		public Guid ResponseId { get; set; }
		public Guid SurveyId { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
