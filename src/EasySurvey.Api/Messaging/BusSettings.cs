using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Messaging
{
	public class BusSettings
	{
		public string Host { get; set; }
		public ushort Port { get; set; }
		public string VirtualHost { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
