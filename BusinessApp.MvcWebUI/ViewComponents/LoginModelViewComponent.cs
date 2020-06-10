using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.ViewComponents
{
    public class LoginModelViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var model = new LoginViewModel();
            return View(model);
        }
    }
}
