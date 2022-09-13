using EasySurvey.Api.Dtos;
using EasySurvey.Common;
using EasySurvey.Domain.WriteModels.Responses;
using EasySurvey.Domain.Events;
using EasySurvey.Domain.Events.Public;
//using EasySurvey.Infrastructure.EventsDispatching;
using EasySurvey.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Response = EasySurvey.Domain.WriteModels.Responses.Response;

namespace EasySurvey.Api.Commands
{
	public class SubmitResponseCommand : IRequest<Response>
	{
		public string ResponseId { get; }

		public SubmitResponseCommand(string responseId)
		{
			ResponseId = responseId;
		}
	}

	public class SubmitResponseCommandHandler : IRequestHandler<SubmitResponseCommand, Response>
	{
		private IResponsesRepository _responsesRepo;
		private IBusControl _busControl;

		public SubmitResponseCommandHandler(IResponsesRepository responsesRepo, IBusControl busControl)
		{
			_responsesRepo = responsesRepo;
			_busControl = busControl;
		}

		public async Task<Response> Handle(SubmitResponseCommand request, CancellationToken cancellationToken)
		{
			var response = await _responsesRepo.GetResponseById(Guid.Parse(request.ResponseId));

			response.Submit();

			await _responsesRepo.Save();

			await _busControl.Publish(message: new ResponseSubmittedEvent()
			{
				ResponseId = response.ResponseId,
				SurveyId = response.SurveyId,
				SubmittedDate = response.SubmittedDate.Value
			});
			return response;
		}
	}
}
