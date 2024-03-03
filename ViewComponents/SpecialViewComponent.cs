using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;

namespace WebUI.ViewComponents
{


    public class SpecialViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public SpecialViewComponent(IHttpContextAccessor httpContext, AppDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var specialType = _dbContext.Actqueses.FirstOrDefault();
            var specialContent = _dbContext.Specials.OrderByDescending(x => x.Id).ToList();

            if (specialType != null && specialType.ActQuesName == "Doğum Günü")
            {
                var index = specialContent.FindIndex(x => x.Id == 1);
                if (index != -1)
                {
                    var title = specialContent[index].Title;
                    var code = specialContent[index].Answer;
                    ViewBag.sualBasliqi = title;
                    ViewBag.sualCavabi = code;
                }
            }
            if (specialType != null && specialType.ActQuesName == "SeriaNo")
            {
                var index = specialContent.FindIndex(x => x.Id == 2); // Id'si 1 olan öğenin indeksini bul
                if (index != -1) // Id'si 1 olan öğe bulunduysa devam et
                {
                    var title = specialContent[index].Title;
                    var code = specialContent[index].Answer;
                    ViewBag.sualBasliqi = title;
                    ViewBag.sualCavabi = code;
                }
            }
            if (specialType != null && specialType.ActQuesName == "Fin Code")
            {
                var index = specialContent.FindIndex(x => x.Id == 3);
                if (index != -1)
                {
                    var title = specialContent[index].Title;
                    var code = specialContent[index].Answer;
                    ViewBag.sualBasliqi = title;
                    ViewBag.sualCavabi = code;
                }
            }


            return View("Special");
        }

    }
}