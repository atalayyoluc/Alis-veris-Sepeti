using Microsoft.AspNetCore.Mvc;

namespace Yeni.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {            if (HttpContext.Session.GetInt32("AdminId").HasValue)
            {
                return Redirect("/Kategori/Index");
            }

            return View();
        }
        public IActionResult Login(string kullaniciAd, string password)
        {

            if (kullaniciAd == "admin" && password == "123")
            {
                HttpContext.Session.SetInt32("AdminId", 1);

                return Redirect("/Kategori/Index");
            }

            return Redirect("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("Index");
        }
    }
}
