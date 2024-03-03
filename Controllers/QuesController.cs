using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Models;


namespace WebUI.Controllers
{
    public class QuesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _dbContext; // Burada ApplicationDbContext, projenizdeki DbContext sınıfınızın ismi olarak varsayıldı

        public QuesController(UserManager<User> userManager, IHttpContextAccessor httpContext, AppDbContext dbContext)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _dbContext = dbContext;
        }

        public IActionResult choose()
        {
            var activSual = _dbContext.Actqueses.FirstOrDefault();
            var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

             ViewBag.AktivSual = activSual.ActQuesName;

    

            return View();
        }

        public IActionResult flat()
        {
              var activSual = _dbContext.Actqueses.FirstOrDefault();
            var numbers = _dbContext.AdresParams.FirstOrDefault();
           
            var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.AktivSual = activSual.ActQuesName;

            ViewBag.numberB = numbers.BuildNumber;
            ViewBag.numberMn = numbers.FlatNumber;
            ViewBag.numberMr = numbers.FloorNumber;
            ViewBag.numberG = numbers.EntryNumber;


            

            return View();
        }

        [HttpPost]
public async Task<IActionResult> flat(int bla)
{
    var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

    var model = new LClass();
    model.Vaxt = DateTime.Now;
  

    _dbContext.Add(model); // Modeli veritabanına ekleyin
    await _dbContext.SaveChangesAsync();
          
             LClass lData = _dbContext.LClasses.OrderByDescending(x => x.Vaxt).FirstOrDefault();
              var a = lData.Vaxt.Minute;
              var b = lData.Vaxt.Second;
              var c = a*60 + b;
            FClass fData = _dbContext.FClasses.OrderByDescending(x => x.Vaxt).FirstOrDefault();
              var d = fData.Vaxt.Minute;
              var e = fData.Vaxt.Second;
              var f = d*60 + e;
              int result = c - f;
             string userId = user.Value;

Score newScore = new Score { Value = result };
    // BestScore modelini al, eğer yoksa yeni bir tane oluştur
     BestScore bestScore = _dbContext.BestScores.FirstOrDefault(bs => bs.UserId == userId);
if (bestScore == null)
{
    bestScore = new BestScore();
}

    // BestScore modeline skoru ve kullanıcı ID'sini ekle
    bestScore.Scores.Add(newScore);
    bestScore.UserId = userId;

    // BestScore modelini veritabanına kaydet
    _dbContext.BestScores.Update(bestScore);
    await _dbContext.SaveChangesAsync();

    
     var userScores = _dbContext.Score.Where(s => s.BestScore.UserId == userId).ToList();
     User userBest = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

    // userin son gorulme tarixini al
    DateTime lastSeen = DateTime.Now;
 
   // Kullanıcının skorlarının toplamını ve sayısını hesaplayın
    int totalScore = userScores.Sum(s => s.Value);
    int scoreCount = userScores.Count;

    // Skor sayısı sıfırdan büyükse ortalama puanı hesaplayın, aksi takdirde varsayılan değeri kullanın
    double averageScore = scoreCount > 0 ? (double)totalScore / scoreCount : 0;

    userBest.UserBestScore = averageScore;
    userBest.ScoreCount = scoreCount;
    userBest.LastSeen = lastSeen;
    await _dbContext.SaveChangesAsync();
   
   

    return RedirectToAction("Erize");
}


        public async Task<IActionResult> erize()
        {
            var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
             LClass lData = _dbContext.LClasses.OrderByDescending(x => x.Vaxt).FirstOrDefault();
              var a = lData.Vaxt.Minute;
              var b = lData.Vaxt.Second;
              var c = a*60 + b;

            FClass fData = _dbContext.FClasses.OrderByDescending(x => x.Vaxt).FirstOrDefault();
              var d = fData.Vaxt.Minute;
              var e = fData.Vaxt.Second;
              var f = d*60 + e;

              var result = c - f;

            
              ViewBag.Eleps = result;
                

            return View();
        }


//         [HttpPost]
//          public async Task<IActionResult> erize(int bla)
//         {
     



//     // Ortalama puanı kullanmak için ViewBag'e ekleyin
//     // ViewBag.AverageScore = averageScore;

//     return View();
// }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }

    internal class YourViewModel
    {
        public string binaNomresi { get; set; }
    }
}
