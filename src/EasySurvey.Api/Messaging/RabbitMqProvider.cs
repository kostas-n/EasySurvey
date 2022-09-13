using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasySurvey.Api.Messaging
{
	public class RabbitMQProvider : IMessageBusProvider
	{
		private ILoggerFactory _loggerFactory;
		private BusSettings _settings;

		public RabbitMQProvider(IOptions<BusSettings> options, ILoggerFactory loggerFactory)
		{
			_loggerFactory = loggerFactory;
			_settings = options.Value;
		}

		public Task<IBusControl> CreateBus(CancellationToken cancellationToken)
		{
			var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
			{
				// defaults to port 5672
				cfg.Host(
					host: _settings.Host,
					virtualHost: _settings.VirtualHost,
					configure: h =>
					{
						h.Username(_settings.Username);
						h.Password(_settings.Password);
					}
				);
			});

			busControl.Start(TimeSpan.FromSeconds(10));

			return Task.FromResult(busControl);
		}
	}
}
