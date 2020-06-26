using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessApp.CarpetWash.MvcWebUI.Data;

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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

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
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ITagDal, EfTagDal>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<CarpetWashContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                   ServiceLifetime.Scoped);

            //Adding Identity 
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<CarpetWashContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.AddAuthorization();

            services.AddRazorPages();

            services.AddDbContext<BusinessAppCarpetWashMvcWebUIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BusinessAppCarpetWashMvcWebUIContext")));

            ////Adding Session
            //services.AddSession();
            //// Session ı serviste tutma
            //services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            //app.UseSession();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern:"{controller=Contents}/{action=Details}/{id?}");
            });
            SeedData.CreateRolesAndAdminUser(serviceProvider);
        }
    }
}
