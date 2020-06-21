using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.DataAccess.Concrete.EntityFramework;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public static class SeedData
    {
        public static void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CarpetWashContext>();
            const string adminRoleName = "Administrator";
            string[] roleNames = { adminRoleName, "Manager", "Subscriber" };

            foreach (string roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }

            // Get these value from "appsettings.json" file.
            string adminUserEmail = "m.ismailardic@gmail.com";
            string adminPwd = "Admin?123";
            string userName = "Admin";
            string userId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf";

            AddUser(serviceProvider, adminUserEmail, userName, userId, adminPwd, adminRoleName);
            AddCategory(serviceProvider, context);
            AddBanner(serviceProvider, context);
            AddContent(serviceProvider, context);

        }

        /// <summary>
        /// Create a role if not exists.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="roleName">Role Name</param>
        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Role role = new Role
                {
                    Name = roleName
                };

                Task<IdentityResult> roleResult = roleManager.CreateAsync(role);
                roleResult.Wait();
            }
        }

        /// <summary>
        /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="userEmail">User Email</param>
        /// <param name="userPwd">User Password. Used to create the user if not exists.</param>
        /// <param name="roleName">Role Name</param>
        private static void AddUser(IServiceProvider serviceProvider, string userEmail, string userName, string userId, string userPwd, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            Task<User> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            User appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                User newAppUser = new User
                {
                    Id = userId,
                    Email = userEmail,
                    UserName = userName,
                    Biography = "Gürpak Halı Yıkama Hizmetleri reklam sorumlusu ",
                    Image = "admin-image.jpg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;
                }
            }

            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
            newUserRole.Wait();
        }
        private static void AddCategory(IServiceProvider serviceProvider, CarpetWashContext context)
        {
            context.Database.Migrate();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category
                    {
                        UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                        Name = "Hizmetlerimiz",
                        Image = "hizmetlerimiz.png",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    },
                    new Category
                    {
                        UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                        Name = "Posts",
                        Image = "hizmetlerimiz.png",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Category
                    {
                        UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                        Name = "Hizmet Bölgeleri",
                        Image = "hizmetlerimiz.png",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    });
                context.SaveChanges();
            }
        }
        private static void AddContent(IServiceProvider serviceProvider, CarpetWashContext context)
        {
            var contentId = context.Categories.First(x => x.Name == "Hizmetlerimiz").Id;

            context.Database.Migrate();
            if (!context.Contents.Any())
            {
                context.Contents.AddRange(
                    new Content
                    {
                        UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                        CategoryId = contentId,
                        Title = "Halı Yıkama",
                        Slug = "hali-yikama",
                        Type = ContentType.Service,
                        Excerpt =
                        " Süper 3 aşamalı halı yıkama" +
                        " hizmeti metodu ile halılarınızı" +
                        " istenilen temizliğe ulaştırır.",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Image = "hali-yikama.png",
                    },
                   new Content
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       CategoryId = contentId,
                       Title = "Battaniye Yıkama",
                       Slug = "battaniye-yikama",
                       Type = ContentType.Service,
                       Excerpt = "Battaniye Yıkama Hizmeti" +
                       "sunarak müşteri memnuniyeti odaklı hizmetler vermektedir.",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "battaniye-yikama.png",
                   },
                   new Content
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       CategoryId = contentId,
                       Title = " Yorgan Yıkama",
                       Slug = "yorgan-yikama",
                       Type = ContentType.Service,
                       Excerpt = "Yorgan Yıkama hizmeti sunarak müşteri memnuniyeti odaklı hizmetler vermektedir.",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "yorgan-yikama.png",
                   },
                   new Content
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       CategoryId = contentId,
                       Title = "Koltuk Yıkama",
                       Slug = "koltuk-yikama",
                       Type = ContentType.Service,
                       Excerpt =
                       " Özel koltuk yıkama için tasarlanmış koltuk yıkama" +
                       " makinalarımız sayesinde koltukarınızı hijyen" +
                       " ve temizlik ile buluşturuyoruz.",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "koltuk-yikama.png",
                   },
                   new Content
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       CategoryId = contentId,
                       Title = "Store Perde Yıkama",
                       Slug = "store-perde-yikama",
                       Type = ContentType.Service,
                       Excerpt = "GürPak Halı Yıkama olarak sizlere Stor" +
                       " Perde Perde yıkama hizmetleride vermekteyiz.",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "stor-perde-yikama.png",
                   },
                    new Content
                    {
                        UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                        CategoryId = contentId,
                        Title = "Halı Tamiri",
                        Slug = "hali-yikama",
                        Type = ContentType.Service,
                        Excerpt =
                        " El dokuması, yün, veya makina halılarınızın bakım" +
                        " ve onarımını konusunda siz değerli müşterilerimize hizmet" +
                        " verilmektedir.",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Image = "hali-tamiri.png",
                    });
                context.SaveChanges();
            }
        }
        private static void AddBanner(IServiceProvider serviceProvider, CarpetWashContext context)
        {
            context.Database.Migrate();
            if (!context.Banners.Any())
            {
                context.Banners.AddRange(
                    new Banner
                    {
                        UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                        Title = "Halı Yıkama",
                        SubTitle = "Telefon Numaramız: 0212 441 8916",
                        BtnTitle = "Detay",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Image = "hali-yikama.jpg"
                    },
                   new Banner
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       Title = " Halı tamiri",
                       SubTitle = "Telefon Numaramız: 0212 441 8916",
                       BtnTitle = "Detay",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "hali-tamiri.jpg"
                   },
                   new Banner
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       Title = "Stor Perde & Perde Yıkama",
                       SubTitle = "Telefon Numaramız: 0212 503 83 32",
                       BtnTitle = "Detay",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "store-perde-yikama.jpg"
                   },
                   new Banner
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       Title = "El Yapımı & Yün Halı Yıkama",
                       SubTitle = "WhatsApp İletişim +90 530 223 5241 Merkez No: 0212 442 4545",
                       BtnTitle = "Detay",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "yün-hali-yikama.jpg"
                   },
                    new Banner
                    {
                        UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                        Title = "Battaniye & Yorgan Yıkama",
                        SubTitle = "Telefon Numaramız: 0212 442 4545",
                        BtnTitle = "Detay",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Image = "yorgan-yikama.jpg"
                    },
                   new Banner
                   {
                       UserId = "bc68af64-5675-4a5b-b6b2-92b2fd282cbf",
                       Title = "Koltuk Yıkama",
                       SubTitle = "Telefon Numaramız: +90 530 223 5241",
                       BtnTitle = "Detay",
                       IsActive = true,
                       CreatedAt = DateTime.Now,
                       UpdatedAt = DateTime.Now,
                       Image = "koltuk-yikama.jpg"
                   });
                context.SaveChanges();
            }
        }
    }
}

