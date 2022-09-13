using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.Aggregates.Surveys
{
	public class Survey
	{
		public long SurveyId { get; private set; }
		public string SurveyName { get; private set; }
		public DateTime? DatePublished { get; private set; }

		public IReadOnlyCollection<Question> Questions { get; }

		public Survey()
		{
			Questions = new List<Question>();
		}
	}
}
