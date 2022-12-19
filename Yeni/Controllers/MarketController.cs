using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yeni.DTO;
using Yeni.Filters;
using Yeni.Models;
using Yeni.Repositories;

namespace Yeni.Controllers
{
    [UserFilter]
    public class MarketController : Controller
        
    {
        UrunRepository urunRepository= new UrunRepository();
        AlısVerisContext context = new AlısVerisContext();
        public IActionResult Index()
        {
            var Sepet = (from sd in context.SatisDetaylaris
                         join s in context.Satislers
                         on sd.SatisId equals s.SatisId
                         where s.KullaniciId == (int)HttpContext.Session.GetInt32("id")
                         group s by new {s.SatisId,s.SatisAdi,s.AlisVerisKontrol} into g
                         select new SepetDetayDTO
                         {
                             ID=g.Key.SatisId,
                             Ad=g.Key.SatisAdi,
                             Kontrol=g.Key.AlisVerisKontrol,
                             Miktar=g.Count()
                             
                             
                         }).ToList();
            return View(Sepet);
        }

        public IActionResult Market()
        {
        
            
            return View(urunRepository.TList());
        }


        public IActionResult SepeteEkle()
        {
            List<SelectListItem> SatisId = (from s in context.Satislers
                                            where s.KullaniciId==(int)HttpContext.Session.GetInt32("id")
                                                select new SelectListItem
                                                {
                                                    Text = s.SatisAdi,
                                                    Value = s.SatisId.ToString()
                                                }
                          ).ToList();
            ViewBag.satis = SatisId;



            return View();
        }
        [HttpPost]
        public IActionResult SepeteEkle(SatisDetaylari satisDetaylari)
        {
            SatisDetaylariRepository satisDetaylariRepository= new SatisDetaylariRepository();
            var y = context.SatisDetaylaris.Where(x=>x.SatisId==satisDetaylari.SatisId).FirstOrDefault(x => x.UrunId == satisDetaylari.UrunId);
            var x = context.Urunlers.FirstOrDefault(x => x.UrunId == satisDetaylari.UrunId);
            if (x == null||y==null)
            {   satisDetaylariRepository.TEkle(satisDetaylari);
                return RedirectToAction("Market");
            }
            
            return RedirectToAction("Market");
        }

        public IActionResult AlisVerisKontrol(int id)
        {
            SatislerRepository satisler = new SatislerRepository(); 
                var x = satisler.TGET(id);
            x.AlisVerisKontrol = false;
            satisler.TGuncelle(x);
            return RedirectToAction("Index");
        }
        
        public IActionResult AlisVerisiBitir(int id)
        {

            SatislerRepository satisler = new SatislerRepository();
            var x = satisler.TGET(id);
            x.AlisVerisKontrol = true;
            satisler.TGuncelle(x);
            return RedirectToAction("Index");
        }

        public IActionResult SepetDetaylari(int id)
        {
            var sepetDetay = (from s in context.Satislers
                              where s.KullaniciId == (int)HttpContext.Session.GetInt32("id") && s.SatisId==id 
                              join sd in context.SatisDetaylaris
                              on s.SatisId equals sd.SatisId
                              join u in context.Urunlers
                              on sd.UrunId equals u.UrunId  
                              select new UrunDTO
                              {                
                                                
                                                UrunAdi=u.UrunAdi,
                                                UrunAciklamasi=u.UrunAciklamasi,
                                                UrunResmi=u.UrunResmi,
                                                UrunMiktari=sd.UrunMiktari
                              }).ToList();
                            
            return View(sepetDetay);  
        }
     



    }
    
}
