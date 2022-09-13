using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasySurvey.Api.Messaging
{
	public static class RabbitMqExtensions
	{
		public static IServiceCollection ConfigureBus(this IServiceCollection serviceCollection, BusSettings settings)
		{
			serviceCollection.AddSingleton(provider =>
				Bus.Factory.CreateUsingRabbitMq(cfg =>
				{
					cfg.Host(
						host: settings.Host,
						port: settings.Port,
						virtualHost: settings.VirtualHost, h =>
						{
							h.Username(settings.Username);
							h.Password(settings.Password);
						}
					);

					cfg.Durable = true;
				})
			);
			return serviceCollection;
		}
	}
}
