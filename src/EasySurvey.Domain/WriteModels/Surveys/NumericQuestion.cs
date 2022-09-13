using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.Aggregates.Surveys
{
	public class NumericQuestion : Question
	{
		public override string QuestionType => "numeric";
	}
}
