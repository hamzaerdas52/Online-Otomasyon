using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.SatisHarekets.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urun = 
                (from x in context.Uruns.ToList()
                select new SelectListItem
                {
                Text = x.UrunAd,
                Value = x.UrunId.ToString()
                }).ToList();
            List<SelectListItem> cr = 
                (from x in context.Caris.ToList()
                select new SelectListItem
                {
                Text = x.CariAd + " " + x.CariSoyad,
                Value = x.CariId.ToString()
                }).ToList();
            List<SelectListItem> per = 
                (from x in context.Personels.ToList()
                select new SelectListItem
                {
                Text = x.PersonelAd + " " + x.PersonelSoyad,
                Value = x.PersonelId.ToString()
                }).ToList();
            ViewBag.urn = urun;
            ViewBag.cri = cr;
            ViewBag.pers = per;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(SatisHareket sh)
        {
            sh.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHarekets.Add(sh);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> urun = (from x in context.Uruns.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.UrunAd,
                                             Value = x.UrunId.ToString()
                                         }).ToList();
            List<SelectListItem> cr = (from x in context.Caris.ToList()
                                       select new SelectListItem
                                       {
                                           Text = x.CariAd + " " + x.CariSoyad,
                                           Value = x.CariId.ToString()
                                       }).ToList();
            List<SelectListItem> per = (from x in context.Personels.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.PersonelAd + " " + x.PersonelSoyad,
                                            Value = x.PersonelId.ToString()
                                        }).ToList();

            ViewBag.urn = urun;
            ViewBag.cri = cr;
            ViewBag.pers = per;
            var satis = context.SatisHarekets.Find(id);
            return View("SatisGetir", satis);
        }

        public ActionResult SatisGuncelle(SatisHareket sh)
        {
            var satis = context.SatisHarekets.Find(sh.SatisId);
            satis.CariId = sh.CariId;
            satis.Adet = sh.Adet;
            satis.PersonelId = sh.PersonelId;
            satis.Fiyat = sh.Fiyat;
            satis.ToplamTutar = sh.ToplamTutar;
            satis.Tarih = sh.Tarih;
            satis.UrunId = sh.UrunId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisDetay(int id)
        {
            var degerler = context.SatisHarekets.
                Where(x => x.SatisId == id).ToList();
            return View(degerler);
        }
    }
}