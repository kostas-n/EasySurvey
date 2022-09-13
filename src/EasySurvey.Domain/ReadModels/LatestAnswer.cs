using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.ReadModels
{
	public class LatestAnswer
	{
		public long AnswerId { get; set; }
		public long QuestionId { get; set; }
		public DateTime? ChangedDtm { get; set; }
		public string TextValue { get; set; }
		public double? NumericValue { get; set; }
		public DateTime? DateValue { get; set; }
		public string Comment { get; set; }
		public Guid ResponseId { get; set; }
		public List<long> SelectedQuestionOptionIds { get; set; }
	}
}
