using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.Aggregates.Surveys
{
	public abstract class Question
	{
		public long QuestionId { get; private set; }
		public string QuestionLabel { get; private set; }
		public abstract string QuestionType { get; }
	}
}
