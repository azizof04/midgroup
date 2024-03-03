using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
   
       private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _dbContext; // Burada ApplicationDbContext, projenizdeki DbContext sınıfınızın ismi olarak varsayıldı

        public HomeController(IHttpContextAccessor httpContext, AppDbContext dbContext)
        {
            
            _httpContext = httpContext;
            _dbContext = dbContext;
        }

    public async Task <IActionResult> Index()
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
public async Task<IActionResult> Index(int a)
{
    
   
       
    
    var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

    if (user == null)
    {
      return RedirectToAction("Auth" , "Login");
    }

      var rowCount = _dbContext.FClasses.Count();
      var rowCountL = _dbContext.LClasses.Count();
      
        if (rowCount > 200)
        {
            var entities = _dbContext.FClasses.ToList();
            _dbContext.RemoveRange(entities);
            _dbContext.SaveChanges();
        }
        if ( rowCountL > 200)
        {
          var entities = _dbContext.LClasses.ToList();
            _dbContext.RemoveRange(entities);
            _dbContext.SaveChanges();   
        }

    var model = new FClass();
    model.Vaxt = DateTime.Now;
   

    _dbContext.Add(model); // Modeli veritabanına ekleyin
    await _dbContext.SaveChangesAsync(); // Değişiklikleri asenkron olarak kaydedin

    return RedirectToAction("Choose" , "Ques");
}



    public IActionResult Privacy()
    {
        return View();
    }

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
