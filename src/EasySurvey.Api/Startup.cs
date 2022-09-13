using System.Reflection;
using AutoMapper;
using EasySurvey.Api.Mappings;
//using EasySurvey.ReadModel.ReadModelGenerators;
using EasySurvey.Common;
using EasySurvey.Domain.Events;
using EasySurvey.Infrastructure;
using EasySurvey.Infrastructure.Database;
//using EasySurvey.Infrastructure.EventsDispatching;
using EasySurvey.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EasySurvey.Api.Commands;
using EasySurvey.Domain.WriteModels.Responses;
using Microsoft.AspNetCore.Authorization;
using EasySurvey.Api.Authentication;
using EasySurvey.Api.Authorization;
using Microsoft.AspNetCore.Authentication;
using MassTransit;
using EasySurvey.Api.Messaging;
using EasySurvey.ReadModel.ReadModelGenerators;
using EasySurvey.Api.Notifications;

namespace EasySurvey.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			var busSettings = new BusSettings();
			var busSettingsSection = Configuration.GetSection("RabbitMQ");
			busSettingsSection.Bind(busSettings);
			services.Configure<BusSettings>(busSettingsSection);

			services.AddAutoMapper(typeof(ResponseProfile), typeof(AnswerProfile), typeof(AnswerLogProfile));

			services.AddDbContext<SurveysDbContext>();

			services.AddScoped<IResponsesRepository, ResponsesRepository>();

			services.AddScoped<IAnswersViewGenerator, AnswersViewGenerator>();

			services.AddScoped<INotificationsService, NotificationsService>();

			services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);


			//services.AddScoped<IUnitOfWork, UnitOfWork>();

			//services.AddScoped<IEventsAccessor, EventsAccessor>();
			//services.AddScoped<IEventsDispatcher, EventsDispatcher>();

			//services.AddTransient(typeof(IPipelineBehavior<SaveAnswersCommand, Unit>),
			//	typeof(CommandHandlerWithUnitOfWork<SaveAnswersCommand, Unit>));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = TestAuthHandler.DefaultScheme;
				options.DefaultScheme = TestAuthHandler.DefaultScheme;
			})
			.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.DefaultScheme, options => { });

			services.AddAuthorization(options =>
			{
				var opaUrl = Configuration.GetValue<string>("Authorization:DecisionPoint:Url");
				options.AddPolicy("ReadResponsePolicy", policy => policy.Requirements.Add(new OpaPolicyRequirement(opaUrl, "opaResponsePolicy", "Read")));
			});

			services.AddHttpClient();
			services.AddHttpContextAccessor();
			services.AddSingleton<IAuthorizationHandler, OpaAuthHandler>();

			services.AddMassTransit();
			services.ConfigureBus(busSettings);
			services.AddSingleton<IMessageBusProvider, RabbitMQProvider>();
			services.AddHostedService<BusHostedService>();

			// services.AddScoped<IRequestHandler<SaveResponseAnswersCommand>, SaveResponseAnswersCommandHandler>();
			// services.Decorate<IRequestHandler<SaveResponseAnswersCommand>, CommandHandlerWithUnitOfWork<SaveResponseAnswersCommand>>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
