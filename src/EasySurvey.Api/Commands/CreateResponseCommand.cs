using AutoMapper;
using EasySurvey.Api.Dtos;
using EasySurvey.Domain.Events.Public;
using EasySurvey.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Response = EasySurvey.Domain.WriteModels.Responses.Response; 

namespace EasySurvey.Api.Commands
{
	public class CreateResponseCommand : IRequest<ResponseDto>
	{
		public CreateResponseCommand(NewResponseDto newResponse)
		{
			SurveyId = newResponse.SurveyId;
			UserId = newResponse.UserId;
		}

		public Guid SurveyId { get; }
		public string UserId { get; }
	}

	public class CreateResponseCommandHandler : IRequestHandler<CreateResponseCommand, ResponseDto>
	{
		private IResponsesRepository _responsesRepo;
		private IBusControl _busControl;
		private IMapper _mapper;

		public CreateResponseCommandHandler(
			IResponsesRepository responsesRepo,
			IBusControl busControl,
			IMapper mapper
			)
		{
			_responsesRepo = responsesRepo;
			_busControl = busControl;
			_mapper = mapper;
		}

		public async Task<ResponseDto> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
		{
			var response = Response.Create(request.SurveyId, request.UserId);
			await _responsesRepo.CreateResponse(response);
			await _responsesRepo.Save();

			await _busControl.Publish(message: new ResponseCreatedEvent()
			{
				CreatedDate = response.CreatedDate,
				SurveyId = response.SurveyId,
			});

			return _mapper.Map<Response, ResponseDto>(response);
		}
	}
}
