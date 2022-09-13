using AutoMapper;
using EasySurvey.Api.Dtos;
using EasySurvey.Domain.WriteModels.Responses;
using EasySurvey.Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasySurvey.Domain.Events.Internal;

namespace EasySurvey.Api.Mappings
{
	public class AnswerProfile: Profile
	{
		public AnswerProfile()
		{
			CreateMap<LatestAnswer, AnswerDto>();

			CreateMap<Answer, AnswerSavedEvent>()
				.ForMember(dest => dest.SetDtm, opt => opt.MapFrom(src => src.UpdatedDtm))
				.ForMember(dest => dest.TextValue, opt => opt.MapFrom(src => src.Text));

			CreateMap<AnswerSavedEvent, LatestAnswer>()
				.ForMember(dest => dest.ChangedDtm, opt => opt.MapFrom(src => src.SetDtm));
		}
	}
}
