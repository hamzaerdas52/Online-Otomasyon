using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using MvcOnlineTicariOtomasyon.Controllers;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context context = new Context();
        public ActionResult Index()
        {
            var urunler = context.Uruns.Where(x => x.Durum==true).ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(Urun u)
        {
            u.Durum = true;
            context.Uruns.Add(u);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger = context.Uruns.Find(id);
            deger.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var urunDeger = context.Uruns.Find(id);
            return View("UrunGetir", urunDeger);
        }

        public ActionResult UrunGuncelle(Urun u)
        {
            var urun = context.Uruns.Find(u.UrunId);
            urun.UrunAd = u.UrunAd;
            urun.Marka = u.Marka;
            urun.Stok = u.Stok;
            urun.AlisFiyat = u.AlisFiyat;
            urun.SatisFiyat = u.SatisFiyat;
            urun.KategoriId = u.KategoriId;
            urun.Durum = u.Durum;
            urun.UrunGorsel = u.UrunGorsel;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunListesi()
        {
            var degerler = context.Uruns.ToList();
            return View(degerler);
        }

    }
}