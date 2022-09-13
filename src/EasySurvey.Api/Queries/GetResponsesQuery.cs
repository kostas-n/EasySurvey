using AutoMapper;
using EasySurvey.Api.Dtos;
using EasySurvey.Domain.ReadModels;
using EasySurvey.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasySurvey.Api.Queries
{
	internal class GetResponsesQuery : IRequest<List<ResponseHeaderDto>>
	{
		public GetResponsesQuery()
		{
		}
	}

	internal class GetResponsesQueryHandler : IRequestHandler<GetResponsesQuery, List<ResponseHeaderDto>>
	{
		private readonly IResponsesRepository _responsesRepo;
		private readonly IMapper _mapper;

		public GetResponsesQueryHandler(IResponsesRepository responsesRepo, IMapper mapper)
		{
			_responsesRepo = responsesRepo;
			_mapper = mapper;
		}

		public async Task<List<ResponseHeaderDto>> Handle(GetResponsesQuery request, CancellationToken cancellationToken)
		{
			var responses = await _responsesRepo.GetResponsesView();

			return _mapper.Map<List<ResponseBasicView>, List<ResponseHeaderDto>>(responses);
		}
	}
}
