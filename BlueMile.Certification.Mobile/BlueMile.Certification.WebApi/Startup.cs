using BlueMile.Certification.Data;
using BlueMile.Certification.Data.Models;
using BlueMile.Certification.WebApi.Areas.Identity;
using BlueMile.Certification.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace BlueMile.Certification.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds).EnableRetryOnFailure(3)),
                ServiceLifetime.Transient);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds).EnableRetryOnFailure(3)),
                ServiceLifetime.Transient);

            //services.AddDefaultIdentity<ApplicationUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
            //        .AddRoles<ApplicationRole>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services
              .AddDataProtection()
              .PersistKeysToDbContext<ApplicationDbContext>()
              .SetApplicationName("BlueMile.Certification");

            services.AddAuthentication();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            //{
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["JWT:Issuer"],
            //        ValidAudience = Configuration["JWT:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
            //    };
            //});
            services.AddServerSideBlazor();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Certification API",
                    Version = "v1",
                    Description = "This is a certification API for Blue Mile."
                });

                var xFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xPath = Path.Combine(AppContext.BaseDirectory, xFile);
                c.IncludeXmlComments(xPath);
            });

            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<ICertificationRepository, CertificationRepository>();

            services.AddScoped<IAuditUserProvider, AuditUserProvider>();

            services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
        {
            using (var servicescope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = servicescope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Certification API");
                x.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
