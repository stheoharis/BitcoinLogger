using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BitcoinLoggerServer.API
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS
            services.AddCors();

            services.AddControllers();

            InjectionContainer.InjectEntities(services, this.Configuration);

            InjectionContainer.AddAuthentication(services, this.Configuration);
                        
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

            //JWT REQUIRED
            app.UseAuthentication();

            app.UseAuthorization();

            //CUSTOM EXCEPTION HANDLING
            app.UseExceptionHandler(
                options =>
                {
                    options.Run(async context =>
                    {

                        context.Response.ContentType = "text/html";
                        var ex = context.Features.Get<IExceptionHandlerFeature>();

                        Trace.WriteLine(ex.Error.GetType());

                        if (ex.Error.GetType() == typeof(BusinessException))
                        {
                            //422
                            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        }
                        else
                        {
                            //500
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        }

                        if (ex != null)
                        {
                            var err = ex.Error.Message; 

                            await context.Response.WriteAsync(ex.Error.Message).ConfigureAwait(true);
                        }
                    });
                }
            );

            //CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
