
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO.Compression;
using Yeni.DTO;
using Yeni.Filters;
using Yeni.Models;
using Yeni.Repositories;

namespace Yeni.Controllers
{
    [AdminFilter]
    public class UrunController : Controller
    {
        AlısVerisContext context = new AlısVerisContext();
        UrunRepository urunRepository=  new UrunRepository();   
        public IActionResult Index()
        {
           

            return View(urunRepository.TList("Kategori"));
        }
        public IActionResult UrunEkle()
        {
             
            
            List<SelectListItem> kategoriler = (from k in context.Kategorilers.ToList()
                                                select new SelectListItem
                                                {   
                                                    Text = k.KategoriAdi,
                                                    Value = k.KategoriId.ToString()
                                                }
                ).ToList();
            ViewBag.kategori1 = kategoriler;


            return View();
        }
        [HttpPost]
        public IActionResult UrunEkle(UrunEkleDTO urun)
        {   
        Urunler urunler=new Urunler();
            if (urun.UrunResmi != null)
            {
                var extension = Path.GetExtension(urun.UrunResmi.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Resimler",newimagename);
                var stream=new FileStream(location,FileMode.Create);
                urun.UrunResmi.CopyTo(stream);
                urunler.UrunResmi = newimagename;

            }
            urunler.UrunAdi = urun.UrunAdi;
            urunler.UrunAciklamasi = urun.UrunAciklamasi;
            urunler.KategoriId= urun.KategoriId;

            urunRepository.TEkle(urunler);

            return RedirectToAction("Index");
        }
        public IActionResult UrunSil(int id)
        {
            urunRepository.Tsil(new Urunler { UrunId = id });
            return RedirectToAction("Index");
      
        }
        public IActionResult UrunGuncelle(int id)
        {
            var urunler = urunRepository.TGET(id);
            List<SelectListItem> kategoriler = (from k in context.Kategorilers.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = k.KategoriAdi,
                                                    Value = k.KategoriId.ToString()
                                                }  ).ToList();
             
            ViewBag.kategori1 = kategoriler;

            Urunler urun = new Urunler()
            {
                UrunAdi = urunler.UrunAdi,
                UrunResmi = urunler.UrunResmi,
                UrunId = urunler.UrunId,
                UrunAciklamasi = urunler.UrunAciklamasi,
                KategoriId = urunler.KategoriId
            };


            return View(urun);
        }
        [HttpPost]
        public IActionResult UrunGuncelle(Urunler urunler)
        {
            
                var urun = urunRepository.TGET(urunler.UrunId);
            urun.UrunAdi = urunler.UrunAdi;
            urun.UrunResmi = urunler.UrunResmi;
            urun.KategoriId = urunler.KategoriId;
            urun.UrunAciklamasi = urunler.UrunAciklamasi;
            urun.UrunId = urunler.UrunId;
            urunRepository.TGuncelle(urun);
            return RedirectToAction("Index");

        }


    }
}
