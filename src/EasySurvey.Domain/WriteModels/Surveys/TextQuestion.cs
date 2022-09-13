using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.Aggregates.Surveys
{
	public class TextQuestion : Question
	{
		public override string QuestionType => "text";
	}
}
