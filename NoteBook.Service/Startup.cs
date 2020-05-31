using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NoteBook.Business.ContactManager;
using NoteBook.Business.NoteManager;
using NoteBook.Data;
using NoteBook.Data.EntityModels;
using NoteBook.Data.Repository.Contracts;
using NoteBook.Data.Repository.Implementation;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NoteBook.Service
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
           
            services.AddAutoMapper(typeof(NoteBook.Business.BusinessMappingFactory));
            services.AddDbContext<NoteBookDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NoteDbConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<NoteBookDbContext>()
             .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(INoteRepository), typeof(NoteRepository));
            services.AddScoped(typeof(IContactRepository), typeof(ContactRepository));
            services.AddTransient<INoteManager, NoteManager>();
            services.AddTransient<IContactManager, ContactManager>();

            //  services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // ===== Use Authentication ======
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();

          
        }
    }
}
