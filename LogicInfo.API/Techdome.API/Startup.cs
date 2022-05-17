using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techdome.API.Model;
using Techdome.API.Repository;
using static Techdome.API.Model.Members;

namespace Techdome.API
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
            /*services.AddDbContext<InlineDatabaseContext>(opt => opt.UseInMemoryDatabase("MyDatabase"));*/
            services.AddDbContext<InlineDatabaseContext>();
            /*services.AddIdentity<Member, IdentityRole>(config =>
            { */
                // User defined password policy settings.  
               /* config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;*/
            //}).AddEntityFrameworkStores<InlineDatabaseContext>().AddDefaultTokenProviders();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                /*option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;*/
            }).AddJwtBearer(option =>
            {
                var key = Encoding.UTF8.GetBytes(Configuration["Auth:JWT:Key"]);
                option.SaveToken = true;
                /*option.RequireHttpsMetadata = false;*/
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Auth:JWT:Issuer"],
                    ValidAudience = Configuration["Auth:JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>(); // NOt working with AddSingeltone - please check 

            services.AddSwaggerGen();
            services.AddControllers();
            /*AddTestData();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*var context = app.ApplicationServices.GetService<InlineDatabaseContext>();
            AddTestData(context);*/
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Analysis Portal APIs v1.0");
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void AddTestData()
        {
            InlineDatabaseContext context = new InlineDatabaseContext();
            Model.Members.Member member1 = new Model.Members.Member()
            {
                Id = 1,
                FirstName = "Sahil",
                LastName = "Raj",
                EmailId = "rajfasfaskf",
                Password = "rajsahil",
                Role = Roles.Admin,
            };
            Model.Members.Member member2 = new Model.Members.Member()
            {
                Id = 2,
                FirstName = "abc",
                LastName = "Raj",
                EmailId = "sdfsdaf",
                Password = "rajabc",
                Role = Roles.User,
            };
            context.Add(member1);
            context.Add(member2);
            context.SaveChanges();
        }
    }
}
