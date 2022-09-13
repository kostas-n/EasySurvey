using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Dtos
{
    public class AnswerLogDto
    {
        public string QuestionId { get; set; }
        public string ResponseId { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedDtm { get; set; }
        public string ChangedByUserId { get; set; }
    }
}
