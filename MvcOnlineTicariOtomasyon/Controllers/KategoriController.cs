using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context context = new Context();
        public ActionResult Index()
        {
            var kategoriler = context.Kategoris.ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            context.Kategoris.Add(k);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var ktg = context.Kategoris.Find(id);
            context.Kategoris.Remove(ktg);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori = context.Kategoris.Find(id);
            return View("KategoriGetir", kategori);
        }

        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktg = context.Kategoris.Find(k.KategoriId);
            ktg.KategoriAd = k.KategoriAd;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}