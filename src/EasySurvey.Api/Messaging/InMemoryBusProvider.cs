using MassTransit;
using MassTransit.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasySurvey.Api.Messaging
{
	public class InMemoryBusProvider : IMessageBusProvider
	{
		private bool _fakeConnectionFailure;

		public InMemoryBusProvider(bool fakeConnectionFailure = false)
		{
			_fakeConnectionFailure = fakeConnectionFailure;
		}

		public InMemoryTestHarness Harness { get; private set; }

		public async Task<IBusControl> CreateBus(CancellationToken cancellationToken)
		{
			Harness = new InMemoryTestHarness();

			if (_fakeConnectionFailure)
			{
				throw new ConnectionException("Simulated connection failure exception thrown");
			}

			await Harness.Start();

			return Harness.BusControl;
		}
	}
}
