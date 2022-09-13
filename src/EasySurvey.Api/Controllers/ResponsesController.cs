using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EasySurvey.Api.Authorization;
using EasySurvey.Api.Commands;
using EasySurvey.Api.Dtos;
using EasySurvey.Api.Queries;
using EasySurvey.Domain.WriteModels.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasySurvey.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResponsesController : ControllerBase
	{
		private IMapper _mapper;
		private IMediator _mediator;

		public ResponsesController(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.Created)]
		public async Task<ActionResult<long>> CreateResponse(NewResponseDto newResponse)
		{
			var response = await _mediator.Send(new CreateResponseCommand(newResponse));

			return CreatedAtAction(nameof(GetFullResponseById), new { responseId = response.ResponseId }, string.Empty);
		}

		[HttpGet]
		[ProducesResponseType(typeof(List<ResponseHeaderDto>), StatusCodes.Status200OK)]
		public async Task<ActionResult> GetResponses()
		{
			var responses = await _mediator.Send(new GetResponsesQuery());
			return Ok(responses);
		}

		[HttpGet("{responseId}")]
		[ProducesResponseType(typeof(ResponseHeaderDto), (int)HttpStatusCode.OK)]
		//[ResponseAuthorize(Policy = "ReadResponsePolicy")]
		public async Task<ActionResult<ResponseDto>> GetFullResponseById(string responseId)
		{
			var response = await _mediator.Send(new GetResponseByIdQuery(responseId));
			if (response == null) { return NotFound(); }

			return Ok(_mapper.Map<ResponseDto>(response));
		}


		[HttpPost("{responseId}/saveAnswers")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
		public async Task<ActionResult> SaveAnswers(NewResponseAnswersDto newResponseAnswers)
		{
			await _mediator.Send(new SaveAnswersCommand(newResponseAnswers));
			return Ok();
		}


		[HttpPost("{responseId}/submit/")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult> SubmitResponse(string responseId)
		{
			await _mediator.Send(new SubmitResponseCommand(responseId));
			
			return Ok();
		}
	}
}