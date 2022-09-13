using EasySurvey.Domain.WriteModels.Responses;
using EasySurvey.Domain.ReadModels;
using EasySurvey.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Infrastructure.Repositories
{
	public interface IResponsesRepository
	{
		Task<Response> GetResponseById(Guid responseId);
		Task CreateResponse(Response response);
		Task Save();

		Task<List<ResponseBasicView>> GetResponsesView();
		void SetLatestAnswer(LatestAnswer latestAnswer);
		Task<List<LatestAnswer>> GetLatestAnswers(Guid responseId);
	}
	public class ResponsesRepository : IResponsesRepository
	{
		private readonly SurveysDbContext _context;

		public ResponsesRepository(SurveysDbContext context)
		{
			_context = context;
		}

		public async Task<Response> GetResponseById(Guid responseId)
		{
			return await _context.Responses
				.SingleOrDefaultAsync(x => x.ResponseId == responseId);
		}

		public async Task<List<ResponseBasicView>> GetResponsesView()
		{
			return await _context.ResponseViews.ToListAsync();
		}
		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

		public async Task CreateResponse(Response response)
		{
			await _context.Responses.AddAsync(response);
		}

		public async Task<List<LatestAnswer>> GetLatestAnswers(Guid responseId)
		{
			return await _context.LatestAnswers.ToListAsync();
		}

		public void SetLatestAnswer(LatestAnswer latestAnswer)
		{
			var existingAnswer = _context.LatestAnswers.Where(
				x => x.ResponseId == latestAnswer.ResponseId &&
				x.QuestionId == latestAnswer.QuestionId
			).FirstOrDefault();

			if (existingAnswer != null)
			{
				_context.Attach(existingAnswer);
				_context.Remove(existingAnswer);
			}
			_context.Add(latestAnswer);
		}
	}
}
