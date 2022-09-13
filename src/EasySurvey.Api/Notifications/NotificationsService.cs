using AutoMapper;
using EasySurvey.Domain.Events.Internal;
using EasySurvey.Domain.WriteModels.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasySurvey.Api.Notifications
{
	public interface INotificationsService
	{
		Task Execute(List<Answer> answers);
	}

	public class NotificationsService : INotificationsService
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public NotificationsService(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		public async Task Execute(List<Answer> answers)
		{
			foreach (var answer in answers)
			{
				var answerSavedEvent = _mapper.Map<Answer, AnswerSavedEvent>(answer);
				await _mediator.Publish(answerSavedEvent);
			};
		}
	}
}
