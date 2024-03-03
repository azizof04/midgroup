using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebUI.Areas.Dashboard.ViewModels;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
[Authorize(Roles = ("admin, moderator"))]



    public class WorksController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _dbContext; // Burada ApplicationDbContext, projenizdeki DbContext sınıfınızın ismi olarak varsayıldı

        public WorksController(UserManager<User> userManager, IHttpContextAccessor httpContext, AppDbContext dbContext)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alert()
        {
            return View();
        }

[HttpPost]
         public IActionResult Alert(string headText , string content)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Actques()
        {
            List<Ques> quesList = _dbContext.Queses.OrderBy(q => q.Id).ToList();

            var viewModel = new QuesListViewModel
            {
                QuesList = quesList.Select(q => new QuesViewModel
                {
                    Id = q.Id,
                    InputQuesName = q.QuesName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Actques(QuesViewModel viewModel,  int SelectedQuesId, int DeletedQuesId)

        
        {
            if (!string.IsNullOrEmpty(viewModel.InputQuesName))
            {
                Ques newQues = new Ques
                {
                    QuesName = viewModel.InputQuesName
                };

                _dbContext.Queses.Add(newQues);
                await _dbContext.SaveChangesAsync();
            }

              if (DeletedQuesId != 9 && DeletedQuesId != 10)
            {
                // Silinecek ques'in ID'sini alarak silme işlemi
                var quesToDelete = await _dbContext.Queses.FindAsync(DeletedQuesId);
                if (quesToDelete != null)
                {
                    _dbContext.Queses.Remove(quesToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }   

             if (SelectedQuesId != 9  )
            {
                Actques doActive = _dbContext.Actqueses.FirstOrDefault();
                var selectedQues = await _dbContext.Queses.FindAsync(SelectedQuesId);
                doActive.ActQuesName = selectedQues.QuesName;
                 if (selectedQues != null)
                {
                    await _dbContext.SaveChangesAsync();
                }
            }
           


            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        public IActionResult Parametr()
        {
            return View();
        }

       [HttpPost]
public async Task<IActionResult> Parametr(string binaNomresi, string girisNomresi, string mertNomresi, string menNomresi)
{
    AdresParam adresParam = _dbContext.AdresParams.FirstOrDefault();
    if (adresParam == null)
    {
        AdresParam newAdres = new AdresParam
        {
            BuildNumber = binaNomresi,
            FlatNumber = menNomresi,
            EntryNumber = girisNomresi,
            FloorNumber = mertNomresi
        };

        _dbContext.AdresParams.Add(newAdres);
    }
    else
    {
        adresParam.BuildNumber = binaNomresi;
        adresParam.FlatNumber = menNomresi;
        adresParam.EntryNumber = girisNomresi;
        adresParam.FloorNumber = mertNomresi;
    }

    await _dbContext.SaveChangesAsync();

    return RedirectToAction("Index");
}


       public async Task<IActionResult> Pasport()
        {

                List<Special> orderedSpecials = await _dbContext.Specials.OrderBy(s => s.Id).ToListAsync();

            foreach (var special in orderedSpecials)
            {

                if (special.Id == 1)
                {
                    
                    ViewBag.bd = special.Answer;
                }
                if (special.Id == 2)
                {
                  
                    ViewBag.sn = special.Answer;
                }
                if (special.Id == 3)
                {
                  
                    ViewBag.fc = special.Answer;
                }
       }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pasport(string dogumTarixi, string seriaNomresi, string finCode)
        {
            List<Special> orderedSpecials = await _dbContext.Specials.OrderBy(s => s.Id).ToListAsync();

            foreach (var special in orderedSpecials)
            {

                if (special.Id == 1)
                {
                    special.Answer = dogumTarixi;
                 
                }
                if (special.Id == 2)
                {
                    special.Answer = seriaNomresi;
               
                }
                if (special.Id == 3)
                {
                    special.Answer = finCode;
                  
                }
       }
            await _dbContext.SaveChangesAsync();
           return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}