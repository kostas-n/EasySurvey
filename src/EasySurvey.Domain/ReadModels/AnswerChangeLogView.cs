using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.ReadModels
{
	public class AnswerChangeLogView
	{
		public long QuestionId { get; set; }
		public Guid ResponseId { get; set; }
		public string NewValue { get; set; }
		public DateTime? ChangedDtm { get; set; }
		public string ChangedByUserId { get; set; }
	}
}
