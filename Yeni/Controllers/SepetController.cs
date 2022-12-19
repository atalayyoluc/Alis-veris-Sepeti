using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yeni.Filters;
using Yeni.Models;
using Yeni.DTO;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc.Rendering;
using Yeni.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Yeni.Controllers
{
    [UserFilter]
    public class SepetController : Controller
    {
     
        SatislerRepository satislerRepository=  new SatislerRepository(); 

      
        public SepetController()
        {   
            
        }
        public IActionResult Index()
            {
            int id = (int)HttpContext.Session.GetInt32("id");
          
                           
                return View(satislerRepository.GetSatisler(id));
            }

        public IActionResult SepetEkle()
                 {

                     return View();
                 }

          [HttpPost]
        public IActionResult SepetEkle(Satisler satis)
                        {
            satis.KullaniciId = (int)HttpContext.Session.GetInt32("id");
                            satislerRepository.TEkle(satis);
                
                            return RedirectToAction("Index");
                        }
        public IActionResult SatisSil(int id)
        {
            satislerRepository.Tsil(new Satisler { SatisId = id });
            return RedirectToAction("Index");
            
        }
       
        public IActionResult SepetGuncelle(int id)
        {
            var Sepet = satislerRepository.TGET(id);
            Satisler satis = new Satisler()
            {
                SatisId = Sepet.SatisId,
                KullaniciId = (int)HttpContext.Session.GetInt32("id"),
                SatisAdi = Sepet.SatisAdi,
            };
            return View(satis);
        }

        [HttpPost]
        public IActionResult SepetGuncelle(Satisler satis)
        {
            var satisler = satislerRepository.TGET(satis.SatisId);
            satisler.SatisId = satis.SatisId;
            satisler.KullaniciId = (int)HttpContext.Session.GetInt32("id");
            satisler.SatisAdi= satis.SatisAdi;
           satislerRepository.TGuncelle(satisler);

            
            return RedirectToAction("Index");
        }

       
    }
}