using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Dtos;
using WebUI.Models;

namespace WebUI.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IHttpContextAccessor _httpContext;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContext, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContext = httpContext;
        _roleManager = roleManager;
    }
    public IActionResult Login()
    {
        var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user != null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

   
    

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto1)
    {


        var findUser = await _userManager.FindByEmailAsync(loginDto1.Email);
        if (findUser == null)
            return View();


        var result = await _signInManager.PasswordSignInAsync(findUser, loginDto1.Password, true, true);
        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        return View();
    }
    //GET, POST
    //AOP
   [Authorize(Roles = "admin , moderator    ")]

    [HttpGet] // Attribute
    public IActionResult Register()
    {
    
            return View();

        
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var findUser = await _userManager.FindByEmailAsync(registerDto.Email);

        if (findUser != null)
            return View();

        User newUser = new()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.Email,    
            Type = "unvan",
            UserBestScore = 0,
            ScoreCount =  0,
            LastSeen =  new DateTime(0001, 1, 01, 00, 00, 00, 000)
        };

        IdentityResult result = await _userManager.CreateAsync(newUser, registerDto.Password);


        if (result.Succeeded)
        {
            if (registerDto.Email == "Admin@Sos@12")
            {
                 _ = await _userManager.AddToRoleAsync(newUser, "admin");
                
            }
            else{
                  _ = await _userManager.AddToRoleAsync(newUser, "user");
            }

          
            return View();
        }


        return View();
    }

    public IActionResult Forgot()
    {
        return View();
    }

    public IActionResult Logout()
    {

        _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
