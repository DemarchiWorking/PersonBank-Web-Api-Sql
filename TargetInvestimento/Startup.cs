using Application.Service;
using Application.Service.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Oracle.ManagedDataAccess.Client;
using Serilog;
using System.Data;
using System.IO;

namespace TargetInvestimento
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
            //services.AddSingleton<IPersonRepository, PersonRepository>();

            services.AddMvcCore();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TargetInvestimento", Version = "v1" });
            });

            services.AddSingleton((ILogger)new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.File(Path.Combine("/var/log/ms_target", "ms_target_investimento.log"), rollingInterval: RollingInterval.Day)
              .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
              .CreateLogger());

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<IKpiService, KpiService>();


            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPlanRepository,  PlanRepository>();
            services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            services.AddScoped<IKpiRepository, KpiRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TargetInvestimento v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
