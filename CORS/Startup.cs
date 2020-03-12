using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CORS
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
            ///不推薦
            services.AddCors(cors =>
            {
                cors.AddPolicy("AllRequests", policy =>
                {
                    policy.AllowAnyOrigin()//任何來源
                    .AllowAnyMethod()//任何方式
                    .AllowAnyHeader()//任何表投
                    .AllowCredentials();//任何COOKIE
                });
            });

            services.AddCors(c =>
            {
                //一般采用這種方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    // 支持多個域名端口，註意端口號後不要帶/斜桿：比如localhost:8000/，是錯的
                    // 註意，http: 和 http://localhost:540 是不一樣的，盡量寫兩個
                    policy
                    .WithOrigins("http://127.0.0.1:5001", "http://localhost:5001", "http://127.0.0.1:5002", "http://localhost:5002")
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });
            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}