using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents
{
    public class MisalLViewComponent : ViewComponent
    {
          public async Task<IViewComponentResult> InvokeAsync()
        {
            

            return View("MisalL");
        }
    }
}