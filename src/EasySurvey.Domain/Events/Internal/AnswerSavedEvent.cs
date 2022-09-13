using EasySurvey.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.Events.Internal
{
	public class AnswerSavedEvent: IInternalEvent
	{
		public DateTime OccurredOn => DateTime.UtcNow;
		public long AnswerId { get; set; }
		public long QuestionId { get; set; }
		public Guid ResponseId { get; set; }
		public string TextValue { get; set; }
		public string Comment { get; set; }
		public DateTime? SetDtm { get; set; }
	}
}
