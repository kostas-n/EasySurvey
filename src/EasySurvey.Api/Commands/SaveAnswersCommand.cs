using EasySurvey.Api.Dtos;
using EasySurvey.Api.Notifications;
using EasySurvey.Common;
using EasySurvey.Domain.WriteModels.Responses;
using EasySurvey.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasySurvey.Api.Commands
{
	public class SaveAnswersCommand : IRequest
	{
		public string ResponseId { get; }
		public string UserId { get; private set; }
		public IEnumerable<NewAnswerDto> Answers { get; }

		public SaveAnswersCommand(NewResponseAnswersDto newResponseAnswersDto)
		{
			ResponseId = newResponseAnswersDto.ResponseId;
			Answers = newResponseAnswersDto.Answers;
		}
	}

	public class SaveResponseAnswersCommandHandler : IRequestHandler<SaveAnswersCommand>
	{
		private IResponsesRepository _responsesRepo;
		private INotificationsService _notificationsService;

		public SaveResponseAnswersCommandHandler(IResponsesRepository responsesRepo, INotificationsService notificationsService)
		{
			_responsesRepo = responsesRepo;
			_notificationsService = notificationsService;
		}

		public async Task<Unit> Handle(SaveAnswersCommand request, CancellationToken cancellationToken)
		{
			var response = await _responsesRepo.GetResponseById(Guid.Parse(request.ResponseId));

			foreach (var answer in request.Answers)
			{
				var answerToAdd = Answer.BuildNewAnswer(
						 questionId: answer.QuestionId,
						 textValue: answer.TextValue,
						 numericValue: answer.NumericValue,
						 comment: answer.Comment
				);
				response.AddAnswer(answerToAdd);
			}

			await _responsesRepo.Save();

			await _notificationsService.Execute(response.Answers.ToList());

			return Unit.Value;
		}
	}
}
