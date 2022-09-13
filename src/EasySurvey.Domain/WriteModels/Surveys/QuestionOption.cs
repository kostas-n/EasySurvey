using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.Aggregates.Surveys
{
	public class QuestionOption
	{
		public long QuestionOptionId { get; set; }
		public long ParentQuestionId { get; set; }
		public string QuestionOptionLabel { get; set; }
		public long Order { get; set; }
	}
}
