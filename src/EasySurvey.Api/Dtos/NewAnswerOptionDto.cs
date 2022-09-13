using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Dtos
{
	public class NewAnswerOptionDto
	{
		public long QuestionId { get; set; }
		public long QuestionOptionId { get; set; }
	}
}
