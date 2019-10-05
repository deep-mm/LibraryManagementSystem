
namespace LMS.APILayer
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.AzureAD.UI;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using LMS.DataAccessLayer.Repositories;
    using LMS.DataAccessLayer.DatabaseContext;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using LMS.DataAccessLayer.Profiles;
    using Swashbuckle.AspNetCore.Swagger;
    using System.Collections;
    using LMS.SharedFiles;
    using LMS.BusinessLogic.Services;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = Configuration["AzureAd:ResourceId"];
                    options.Authority = $"{Configuration["AzureAd:Instance"]}{Configuration["APITenantId"]}";
                });


            //Serialization into JSON configuration files
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(o =>
                {
                    if (o.SerializerSettings.ContractResolver != null)
                    {
                        var castedResolver = o.SerializerSettings.ContractResolver
                            as DefaultContractResolver;
                        castedResolver.NamingStrategy = null;
                    }
                });

            services.AddDbContextPool<ReadDBContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"], b => b.MigrationsAssembly("LMS.APILayer"));
            });

            services.AddMvc();

            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v2", new Info { Title = "LMSApiServices", Version = "v2" });
                   c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                   {
                       Description = "Jwt Authorisation Header {token}",
                       Name = "Authorization",
                       In = "header",
                       Type = "apiKey"
                   });
                   c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                   {
                        {"Bearer", new string[] { } }
                   });
               });

            //AutoMapper Configurations
            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AutoMapperProfiles())
            );

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration["RedisConnection"];
                option.InstanceName = "master";
            });

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBlobRepository, BlobRepository>();
            services.AddScoped<IBooksBusinessLogic, BooksBusinessLogic>();
            services.AddScoped<ILibraryBusinessLogic, LibraryBusinessLogic>();
            services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();
            services.AddScoped<IAzureSearchService, AzureSearchService>();
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

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "LMSApiServices");

            });
        }
    }
}
