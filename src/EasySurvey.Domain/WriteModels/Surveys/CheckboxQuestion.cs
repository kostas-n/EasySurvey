using EasySurvey.Domain.Aggregates.Surveys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.WriteModels.Surveys
{
	public class CheckboxQuestion : Question
	{
		public override string QuestionType => "checkbox";

		public List<QuestionOption> QuestionOptions { get; set; }
	}
}
