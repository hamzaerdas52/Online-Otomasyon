using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context context = new Context();
        public ActionResult Index()
        {
            var fatura = context.Faturas.ToList();
            return View(fatura);
        }

        public ActionResult FaturaDetay(int id)
        {
            var fatura = context.Faturas.Where(x => x.FaturaId == id).ToList();
            return View("FaturaDetay", fatura);
        }
        public ActionResult FaturaKalem(int id)
        {
            var faturaKalem = context.FaturaKalems.Where(x => x.FaturaId == id).ToList();
            return View("FaturaKalem", faturaKalem);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            List<SelectListItem> personel = (from x in context.Personels.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.PersonelAd + " " + x.PersonelSoyad,
                                             Value = x.PersonelId.ToString()
                                         }).ToList();
            ViewBag.pers = personel;
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Fatura f)
        {
            context.Faturas.Add(f);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            List<SelectListItem> personel = (from x in context.Personels.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                  Value = x.PersonelId.ToString()
                                              }).ToList();
            ViewBag.pers = personel;
            var faturaDeger = context.Faturas.Find(id);
            return View("FaturaGetir", faturaDeger);
        }

        public ActionResult FaturaGuncelle(Fatura f)
        {
            var fatura = context.Faturas.Find(f.FaturaId);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.VergiDairesi = f.VergiDairesi;
            fatura.Tarih = f.Tarih;
            fatura.Saat = f.Saat;
            fatura.TeslimEden = f.TeslimEden;
            fatura.TeslimAlan = f.TeslimAlan;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            List<SelectListItem> urun = (from x in context.Uruns.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.UrunAd,
                                                 Value = x.UrunAd.ToString()
                                             }).ToList();
            ViewBag.urun = urun;
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem fk)
        {
            context.FaturaKalems.Add(fk);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}