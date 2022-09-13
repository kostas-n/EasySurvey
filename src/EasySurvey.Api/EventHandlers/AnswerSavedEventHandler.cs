using EasySurvey.ReadModel.ReadModelGenerators;
using EasySurvey.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasySurvey.Domain.Events.Internal;

namespace EasySurvey.Api.EventHandlers
{
	public class AnswerSavedEventHandler : INotificationHandler<AnswerSavedEvent>
	{
		private IAnswersViewGenerator _answersViewGenerator;

		public AnswerSavedEventHandler(IAnswersViewGenerator answersViewGenerator)
		{
			_answersViewGenerator = answersViewGenerator;
		}

		public async Task Handle(AnswerSavedEvent notification, CancellationToken cancellationToken)
		{
			await _answersViewGenerator.HandleEvent(notification);
			return;
		}
	}
}
