using AutoMapper;
using EasySurvey.Api.Dtos;
using EasySurvey.Domain.ReadModels;
using EasySurvey.Domain.WriteModels.Responses;
using EasySurvey.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasySurvey.Api.Queries
{
	internal class GetResponseByIdQuery : IRequest<ResponseDto>
	{
		public string ResponseId { get; }
		public GetResponseByIdQuery(string responseId)
		{
			ResponseId = responseId;
		}
	}

	internal class GetResponseByIdQueryHandler : IRequestHandler<GetResponseByIdQuery, ResponseDto>
	{
		private readonly IResponsesRepository _responsesRepo;
		private readonly IMapper _mapper;

		public GetResponseByIdQueryHandler(IResponsesRepository responsesRepo, IMapper mapper)
		{
			_responsesRepo = responsesRepo;
			_mapper = mapper;
		}

		public async Task<ResponseDto> Handle(GetResponseByIdQuery request, CancellationToken cancellationToken)
		{
			var responseGuid = Guid.Parse(request.ResponseId);
			var response = await _responsesRepo.GetResponseById(responseGuid);
			var latestAnswers = await _responsesRepo.GetLatestAnswers(responseGuid);

			var responseDto = _mapper.Map<Response, ResponseDto>(response);
			responseDto.Answers = _mapper.Map<List<LatestAnswer>, List<AnswerDto>>(latestAnswers);

			return responseDto;
		}
	}
}
