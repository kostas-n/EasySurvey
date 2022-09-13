using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasySurvey.Api.Messaging
{
	public class BusHostedService : IHostedService
	{
		private IMessageBusProvider _messageBusProvider;
		private IBusControl _bus;

		public BusHostedService(IMessageBusProvider messageBusProvider)
		{
			_messageBusProvider = messageBusProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			_bus = await _messageBusProvider.CreateBus(cancellationToken);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await _bus.StopAsync(cancellationToken);
		}
	}
}
