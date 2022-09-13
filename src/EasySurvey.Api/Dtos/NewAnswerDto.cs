using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Dtos
{
	public class NewAnswerDto
	{
		public long QuestionId { get; set; }
		public DateTime? LastUpdatedDtm { get; set; }
		public string TextValue { get; set; }
		public double? NumericValue { get; set; }
		public string Comment { get; set; }
		public List<NewAnswerOptionDto> AnswerOptions { get; set; }
	}
}
