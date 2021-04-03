using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Aciderp.CQRS.Command;
using Aciderp.CQRS.Query;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Aciderp.Data;
using Aciderp.DTO.Command;
using Aciderp.BusinessLayer.Commands;
using Aciderp.BusinessLayer.Query;
using Aciderp.DTO.Query;

namespace Aciderp
{
	public class Startup
	{
		private IConfiguration _configuration { get; }
		const string API_TITLE = "Trip Management - API";
		const string API_VER = "v1";
		const string SWAGGER_ENDPOINT = "v1/swagger.json";
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApplicationInsightsTelemetry();
			// Settings
			services.Configure<KestrelServerOptions>(_configuration.GetSection("Kestrel"));
			services.AddControllers();

			services.AddDbContext<TripManagementContext>(options =>
            	options.UseSqlServer(_configuration.GetConnectionString("TripManagementContext")));

			// Repositories
			services.AddScoped<Data.TripRepository>();
			services.AddScoped<Data.CustomerRepository>();

			// Commands
			services.AddScoped<ICommandDispatcher, CommandDispatcher>();

			services.AddTransient<ICommandHandler<AssignCustomerToTrip>,
				AssignCustomerToTripHandler>();
			services.AddTransient<ICommandHandler<CustomerCreate>,
				CustomerCreateHandler>();
			services.AddTransient<ICommandHandler<CustomerUpdate>,
				CustomerUpdateHandler>();
			services.AddTransient<ICommandHandler<TripCancel>,
				TripCancelHandler>();
			services.AddTransient<ICommandHandler<TripCreate>,
				TripCreateHandler>();
			services.AddTransient<ICommandHandler<TripUpdate>,
				TripUpdateHandler>();

			services.AddScoped<IQueryDispatcher, QueryDispatcher>();
			services.AddTransient<IQueryHandler<TripGet,
				TripGetResult>, TripGetHandler>();
			services.AddTransient<IQueryHandler<CustomerGet,
				CustomerGetResult>, CustomerGetHandler>();

			services.AddMvc();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
			ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
				app.UseHttpsRedirection();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(SWAGGER_ENDPOINT, API_VER);
			});
			app.UseRouting();
			app.UseCors();
			// app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
