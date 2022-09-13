using EasySurvey.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Domain.WriteModels.Responses
{
	public class Response : IAggregateRoot
	{
		public Guid ResponseId { get; private set; }
		public Guid SurveyId { get; set; }
		public string UserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? SubmittedDate { get; set; }

		private readonly List<Answer> _answers;
		public IReadOnlyCollection<Answer> Answers => _answers;

		public Response()
		{
			_answers = new List<Answer>();
		}
		public static Response Create(Guid surveyId, string userId)
		{
			var Response = new Response()
			{
				ResponseId = Guid.NewGuid(),
				SurveyId = surveyId,
				UserId = userId,
				CreatedDate = DateTime.UtcNow,
			};

			return Response;
		}

		public void AddAnswer(Answer answer)
		{
			answer.ResponseId = ResponseId;

			_answers.Add(answer);
		}

		public void Submit()
		{
			SubmittedDate = DateTime.UtcNow;
		}
	}
}
