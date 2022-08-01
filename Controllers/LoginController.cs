using CoreDepartman.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreDepartman.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }
        public async Task<IActionResult> GirisYap(Admin p)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.Kullanıcı == p.Kullanıcı && x.Sifre == p.Sifre);
            if(bilgiler != null)
            {
                //işlemler
                var clams = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.Kullanıcı)
                };
                var useridentity = new ClaimsIdentity(clams, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Default");
            }
            else
            {
                ViewBag.LoginError = "Hatalı Kullanıcı Adı veya Şifre";
            }
            return View();
        }
    }
}
