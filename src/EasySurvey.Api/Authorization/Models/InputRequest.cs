using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Authorization.Models
{
    public class InputRequest
    {
        public string Permission { get; set; }
        public IEnumerable<string> Attribute { get; set; }
        public string Subject { get; set; }
        public string RawJwt { get; set; }
        public string TenantId { get; internal set; }
        public string ResourceId { get; internal set; }
    }
}
