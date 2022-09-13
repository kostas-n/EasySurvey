using EasySurvey.Common;
using EasySurvey.Domain.WriteModels.Surveys;
using System;
using System.Collections.Generic;

namespace EasySurvey.Domain.WriteModels.Responses
{
	public class Answer: IEntity
	{
		public long AnswerId { get; private set; }
		public long QuestionId { get; set; }
		public Guid ResponseId { get; set; }
		public List<long> SelectedQuestionOptionIds { get; set; }
		public string Text { get; set; }
		public double? Number { get; set; }
		public string Comment { get; set; }
		public DateTime UpdatedDtm { get; set; }

		public static Answer BuildNewAnswer(long questionId, string textValue, double? numericValue, string comment)
		{
			return new Answer()
			{
				QuestionId = questionId,
				Text = textValue,
				Number = numericValue,
				Comment = comment,
				UpdatedDtm = DateTime.UtcNow
			};
		}
	}
}