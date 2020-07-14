using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.Business.Concrete;
using BusinessApp.CarpetWash.DataAccess.Abstract;
using BusinessApp.CarpetWash.DataAccess.Concrete.EntityFramework;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessApp.MvcWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //// Session ı serviste tutma
            services.AddDistributedMemoryCache();

            ////Adding Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddDirectoryBrowser();

            //Adding Services..
            services.AddScoped<IBannerService, BannerManager>();
            services.AddScoped<IBannerDal, EfBannerDal>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EfCommentDal>();
            services.AddScoped<IContentService, ContentManager>();
            services.AddScoped<IContentDal, EfContentDal>();
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ITagDal, EfTagDal>();
            services.AddScoped<IFeatureService, FeatureManager>();
            services.AddScoped<IFeatureDal, EfFeatureDal>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<CarpetWashContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                    ServiceLifetime.Scoped);

            //Adding Identity 
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<CarpetWashContext>()
                .AddDefaultTokenProviders();

            //User session time out
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.LoginPath = "/Identity/Account/Login";
                options.SlidingExpiration = true;
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddAuthorization();

            //services.AddRazorPages();

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default2Days",
                    new CacheProfile()
                    {
                        Duration = 172800
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Use static files
            const string cacheMaxAge = "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Cache static files for 30 days
                    //ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=2592000");
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cacheMaxAge}");
                    ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(30).ToString("R", CultureInfo.InvariantCulture));
                }
            });

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,
            //    OnPrepareResponse = (context) =>
            //    {
            //        var headers = context.Context.Response.GetTypedHeaders();
            //        headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            //        {
            //            Public = true,
            //            MaxAge = TimeSpan.FromDays(30)
            //        };

            //    }
            //});

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Contents}/{action=Details}/{id?}");

                endpoints.MapRazorPages();
            });

            SeedData.CreateRolesAndAdminUser(serviceProvider);
        }
    }
}
