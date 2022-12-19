using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yeni.Filters;
using Yeni.Models;
using Yeni.DTO;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Yeni.Controllers
{
    public class AccountController : Controller
    {
        AlısVerisContext context = new AlısVerisContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Index");
            }

            return View();
        }
        public IActionResult Login(string email, string password)
        {
            var user = context.Kullanicilers.FirstOrDefault(x => x.Eposta.Equals(email) && x.Sifre.Equals(password));
            if (user != null)
            {
                HttpContext.Session.SetInt32("id", user.KullaniciId);
                HttpContext.Session.SetString("fullName", user.Adi + "" + user.Soyadi);
                return Redirect("/Market/Index");
            }

            return Redirect("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("Index");
        }

        public IActionResult SingUp()
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Market/Index");
            }

            return View();
        }

        public async Task<IActionResult> Register(Kullaniciler user)
        {
            Regex s = new Regex("^(?=.*[a - z])(?=.*[A - Z])(?=.*\\d)(?=.*[$@! % *? +#&'()[=\"€])[A-Za-z\\d$@!%*?+#&'()[=\"€']{8,}");
            var eposta=context.Kullanicilers.FirstOrDefault(x => x.Eposta == user.Eposta);
            if (user.Eposta == null || user.Sifre.Length <8||s.IsMatch(user.Sifre))
            {
                return RedirectToAction("SingUp");
                if (eposta == null) { 
                return RedirectToAction("SingUp");
                }
            }
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}


