using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using BusinessApp.CarpetWash.MvcWebUI.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager,
                                 RoleManager<Role> roleManager,
                                 SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid && (registerViewModel.Password == registerViewModel.ConfirmPassword))
            {
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email
                };

                IdentityResult result =
                    _userManager.CreateAsync(user, registerViewModel.Password).Result;

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("Subscriber").Result)
                    {
                        Role role = new Role
                        {
                            Name = "Subscriber"
                        };

                        IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "We can't add the role!");
                            return View(registerViewModel);
                        }
                    }
                    _userManager.AddToRoleAsync(user, "Subscriber").Wait();
                    return RedirectToAction("Index", "Home");
                }

                TempData.Put("modal", new ModalData
                {
                    Type = "registration-modal",
                    Error = "Please Insert Correct values!",
                });

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            TempData.Put("modal", new ModalData
            {
                Type = "login-modal",
                Error = "Please Login First!"
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                //TODO: This will be deleted later.
                var user =  _userManager.FindByNameAsync(loginViewModel.UserName).Result;
                var role = _userManager.GetRolesAsync(user).Result.SingleOrDefault();

                if (ModelState.IsValid)
                {
                    //TODO: This condition will be deleted.
                    if(role == "Administrator") 
                    {
                        var result = _signInManager.PasswordSignInAsync(loginViewModel.UserName,
                        loginViewModel.Password, loginViewModel.RememberMe, loginViewModel.RememberMe).Result;

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        ModelState.AddModelError("", "Invalid login attempt!");
                    }
                    else { return RedirectToAction("Index", "Home"); }
                }

                TempData.Put("modal", new ModalData
                {
                    Type = "login-modal",
                    Error = "Username or Password is wrong!"
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index","Home");
        }
    }
}