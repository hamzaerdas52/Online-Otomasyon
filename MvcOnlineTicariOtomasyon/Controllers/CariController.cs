using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Controllers;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context context = new Context();
        public ActionResult Index()
        {
            var cari = context.Caris.Where(x => x.Durum==true).ToList();
            return View(cari);
        }

        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cari c)
        {
            c.Durum = true;
            context.Caris.Add(c);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var cari = context.Caris.Find(id);
            cari.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var cari = context.Caris.Find(id);
            return View("CariGetir", cari);
        }

        public ActionResult CariGuncelle(Cari c)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cari = context.Caris.Find(c.CariId);
            cari.CariAd = c.CariAd;
            cari.CariSoyad = c.CariSoyad;
            cari.CariSehir = c.CariSehir;
            cari.CariMail = c.CariMail;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGecmis(int id)
        {
            var degerler = context.SatisHarekets.Where(x => x.CariId == id).ToList();
            var cari = context.Caris.Where(x => x.CariId == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cari;
            return View(degerler);
        }

    }
}