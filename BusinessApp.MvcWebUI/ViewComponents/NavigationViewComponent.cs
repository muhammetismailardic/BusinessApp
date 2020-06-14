using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke(ContentType type)
        {
            return View(type);
        }
    }
}
