

using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Identity;
using Serilog;
using SurveyBasket.Abstractions.Consts;

namespace MySurveyBasket

{
    public class Program
    {
        public static void Main(string[] args)
        {

            
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
           

            builder.Services.AddDependencies(builder.Configuration);
            builder.Services.AddDistributedMemoryCache();

            builder.Host.UseSerilog((Context, Configration) =>
            {
                Configration.ReadFrom.Configuration(Context.Configuration);
            });
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseHangfireDashboard("/jobs",new DashboardOptions
            {
                Authorization = 
                [
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = app.Configuration["HangfireSettings:DashboardUsername"],
                        Pass = app.Configuration["HangfireSettings:DashboardPassword"]
                    }
                ]
            });

            RecurringJob.AddOrUpdate<INotificationService>("SendNotifications", x => x.SendNewPollsNotification(null), Cron.Daily);

            app.UseExceptionHandler();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();
           


            app.MapControllers();
          

            app.Run();
        }
    }
}
