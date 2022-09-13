using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Dtos
{
	public class AnswerOptionDto
	{
		public long AnswerId { get; set; }
		public long QuestionId { get; set; }
		public long QuestionOptionId { get; set; }
		public DateTime? LastUpdatedDtm { get; set; }
	}
}
