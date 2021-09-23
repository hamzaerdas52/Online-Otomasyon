using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using MvcOnlineTicariOtomasyon.Controllers;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context context = new Context();
        public ActionResult Index()
        {
            var departmanlar = context.Departmans.
            Where(x => x.Durum == true).ToList();
            return View(departmanlar);
        }

        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            d.Durum = true;
            context.Departmans.Add(d);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            var departman = context.Departmans.Find(id);
            departman.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanGetir(int id)
        {
            var departman = context.Departmans.Find(id);
            return View("DepartmanGetir", departman);
        }

        public ActionResult DepartmanGuncelle(Departman d)
        {
            var departman = context.Departmans.Find(d.DepartmanId);
            departman.DepartmanAd = d.DepartmanAd;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int id)
        {
            var degerler = context.Personels.Where(x => x.DepartmanId == id).ToList();
            var departman = context.Departmans.Where(x => x.DepartmanId == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.d = departman;
            return View(degerler);
        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = context.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            var personel = context.Personels.Where(x => x.PersonelId == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dper = personel;
            return View(degerler);
        }
    }
}