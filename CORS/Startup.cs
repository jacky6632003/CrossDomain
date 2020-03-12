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
            ///������
            services.AddCors(cors =>
            {
                cors.AddPolicy("AllRequests", policy =>
                {
                    policy.AllowAnyOrigin()//����ӷ�
                    .AllowAnyMethod()//����覡
                    .AllowAnyHeader()//������
                    .AllowCredentials();//����COOKIE
                });
            });

            services.AddCors(c =>
            {
                //�@����γo�ؤ�k
                c.AddPolicy("LimitRequests", policy =>
                {
                    // ����h�Ӱ�W�ݤf�A���N�ݤf���ᤣ�n�a/�ױ�G��plocalhost:8000/�A�O����
                    // ���N�Ahttp: �M http://localhost:540 �O���@�˪��A�ɶq�g���
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