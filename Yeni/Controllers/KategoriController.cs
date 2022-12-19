using Microsoft.AspNetCore.Mvc;
using Yeni.DTO;
using Yeni.Filters;
using Yeni.Models;
using Yeni.Repositories;

namespace Yeni.Controllers
{
    [AdminFilter]
    public class KategoriController : Controller

    { AlısVerisContext context = new AlısVerisContext();
        KategoriRepository kategoriRepository= new KategoriRepository();
        public IActionResult Index()
        {
          
            return View(kategoriRepository.TList());
        }
        [HttpGet]
        public IActionResult KategoriEkle() {

           
            return View();
                }
        [HttpPost]
        public IActionResult KategoriEkle(KategoriEkleDTO kategori)
            {
            Kategoriler kategoriler1 = new Kategoriler();
            if (kategori.KategoriResmi != null)
            {
                var extension = Path.GetExtension(kategori.KategoriResmi.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                kategori.KategoriResmi.CopyTo(stream);
                kategoriler1.KategoriResmi = newimagename;

            }
            kategoriler1.KategoriAdi = kategori.KategoriAdi;

            var kategoriler=context.Kategorilers.Where(a=>a.KategoriAdi==kategoriler1.KategoriAdi).FirstOrDefault();
            
            if (!ModelState.IsValid||kategoriler!=null)
            {

                return View();
            }
          kategoriRepository.TEkle(kategoriler1);

            return RedirectToAction("Index");
        }

        public IActionResult KategoriSil(int id)
        {

            kategoriRepository.Tsil(new Kategoriler{  KategoriId=id  });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult KategoriGet(int id)
        {
            var x=kategoriRepository.TGET(id);

            Kategoriler kategori = new Kategoriler() {
            KategoriAdi=x.KategoriAdi,
            KategoriResmi=x.KategoriResmi,
            KategoriId=x.KategoriId,
            };

            return View(kategori);
        }
        [HttpPost]
        public IActionResult KategoriGuncelle(Kategoriler kategoriler)
        {
            var kategori = kategoriRepository.TGET(kategoriler.KategoriId);

            kategori.KategoriAdi = kategoriler.KategoriAdi;
            kategori.KategoriResmi=kategoriler.KategoriResmi;
            kategori.KategoriId=kategoriler.KategoriId;
            kategoriRepository.TGuncelle(kategori);
               return RedirectToAction("Index");
        }
    } 
    
}
