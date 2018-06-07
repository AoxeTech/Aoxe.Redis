using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zaabee.Redis;
using Zaabee.Redis.Abstractions;
using Zaabee.Redis.ISerialize;
using Zaabee.Redis.Protobuf;

namespace Demo
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ISerializer, Serializer>();
            services.AddSingleton<IZaabeeRedisClient, ZaabeeRedisClient>(p =>
                new ZaabeeRedisClient(new RedisConfig("192.168.78.152:6379,abortConnect=false,syncTimeout=3000"),
                    services.BuildServiceProvider().GetService<ISerializer>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}