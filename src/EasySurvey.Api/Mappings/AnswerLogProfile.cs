using AutoMapper;
using EasySurvey.Api.Dtos;
using EasySurvey.Domain.WriteModels.Responses;
using EasySurvey.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Mappings
{
	public class AnswerLogProfile: Profile
	{
		public AnswerLogProfile()
		{
			CreateMap<AnswerChangeLogView, AnswerLogDto>();
		}
	}
}
