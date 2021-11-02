using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using CoreApplication;
using CoreBaseClass;
using CoreEntityFramework;
using CoreIocManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb.Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContextPool<App_DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("default")))
                .AddTransient(typeof(App_DbContext));
            //services.AddRazorPages();
            services.AddMvc();
            
            services.AddAutoMapper(typeof(MapperConfigProfile));

            //https://github.com/cnblogs/EnyimMemcachedCore
            //memcache
            services.AddEnyimMemcached();

            #region redis
            var redis_section = Configuration.GetSection("Redis:Default");
            string redis_connection_string = redis_section.GetSection("Connection").Value;
            string redis_instance_name = redis_section.GetSection("InstanceName").Value;
            int redis_default_db =int.Parse(redis_section.GetSection("DefaultDB").Value ?? "0");
            services.AddSingleton(new RedisHelper(redis_connection_string, redis_instance_name, redis_default_db));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEnyimMemcached();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
