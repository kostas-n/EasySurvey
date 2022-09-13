using AutoMapper;
using EasySurvey.Common;
using EasySurvey.Domain.Events;
using EasySurvey.Domain.Events.Internal;
using EasySurvey.Domain.ReadModels;
using EasySurvey.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.ReadModel.ReadModelGenerators
{
	public interface IAnswersViewGenerator : IReadModelGenerator<AnswerSavedEvent>
	{

	}

	public class AnswersViewGenerator : IAnswersViewGenerator
	{
		private IMapper _mapper;
		private IResponsesRepository _responsesRepo;

		public AnswersViewGenerator(IMapper mapper, IResponsesRepository responsesRepo)
		{
			_mapper = mapper;
			_responsesRepo = responsesRepo;
		}

		public async Task HandleEvent(AnswerSavedEvent e)
		{
			var latestAnswer = _mapper.Map<AnswerSavedEvent, LatestAnswer>(e);
			_responsesRepo.SetLatestAnswer(latestAnswer);
			await _responsesRepo.Save();
		}
	}
}
