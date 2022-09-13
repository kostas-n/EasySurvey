using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Dtos
{
	public class AnswerDto
	{
		public long AnswerId { get; set; }
		public long QuestionId { get; set; }
		public DateTime? LastUpdatedDtm { get; set; }
		public string TextValue { get; set; }
		public double? NumericValue { get; set; }
		public string Comment { get; set; }
		public List<AnswerOptionDto> AnswerOptions { get; set; }
	}
}
