using AutoMapper;
using JobListing.Core.Services.RoleService.Implementation;
using JobListing.Core.Services.RoleService.Interfaces;
using JobListing.DataAccess.EFCore;
using JobListing.DataAccess.EFCore.EFRepository.Implementation;
using JobListing.DataAccess.EFCore.EFRepository.Interface;
using JobListing.DataAccess.Repository.Implementation;
using JobListing.DataAccess.Repository.Interface;
using JobListing.Models.EFModels;
using JobListingAppUI.DataAccess.Repository.Implementation;
using JobListingAppUI.DataAccess.Repository.Interface;
using JobListingAppUI.DbContexts;
using JobListingAppUI.Services;
using JobListingAppUI.Services.JobServices.Implementation;
using JobListingAppUI.Services.JobServices.Interfaces;
using JobListingAppUI.Services.UserServices.Implementation;
using JobListingAppUI.Services.UserServices.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

namespace JobListingAppUI
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
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Job Listing App Demo",
                    Version = "v1",
                    Description = "Job Listing Project"
                });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT authorization using bearer scheme",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                } });

            });
            services.AddDbContextPool<JobListingDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("EFCoreConnection")));
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {

                    option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireDigit = true;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<JobListingDbContext>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddScoped<IApplicantService, UserService>();
            services.AddScoped<IJobServices, JobServices>();
            services.AddScoped<IPriviledgeJobServices, JobServices>();
            services.AddScoped<IAdmin, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobEFRepo, JobEFRepo>();
            services.AddScoped<ICategoryEFRepo, CategoryEFRepo>();
            services.AddScoped<IADOOperations, ADOOperations>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleServices, RoleService>();
            services.AddTransient<SeederClass>();
            services.AddAutoMapper();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {

                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"])),
                    ValidateIssuerSigningKey = true
                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeederClass seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context => 
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var contentFeatures = context.Features.Get<IExceptionHandlerFeature>();
                        context.Response.Headers.Add("Application-Error", contentFeatures.Error.Message);
                        context.Response.Headers.Add("Application-Control-Expose-Header", "Application-Error");
                        context.Response.Headers.Add("Application-Control-Allow-Origin", "*");

                    });
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            seed.SeedMe().Wait();
            //SetupSeed.SeedMe(aDOOperations).Wait();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "JobListing App.v1"));
        }
    }
}
