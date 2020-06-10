using BusinessApp.CarpetWash.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.ViewComponents
{
    public class RegisterModelViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var model = new RegisterViewModel();
            return View(model);
        }
    }
}
