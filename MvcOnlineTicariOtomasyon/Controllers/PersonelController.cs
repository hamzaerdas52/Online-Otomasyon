using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;
using MvcOnlineTicariOtomasyon.Controllers;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.Personels.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> departman = (from x in context.Departmans.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.DepartmanAd,
                                                  Value = x.DepartmanId.ToString()
                                              }).ToList();
            ViewBag.dprt = departman;
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            context.Personels.Add(p);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> departman = (from x in context.Departmans.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.DepartmanAd,
                                                  Value = x.DepartmanId.ToString()
                                              }).ToList();
            ViewBag.dprt = departman;
            var personelDeger = context.Personels.Find(id);
            return View("PersonelGetir", personelDeger);
        }

        public ActionResult PersonelGuncelle(Personel p)
        {
            var personel = context.Personels.Find(p.PersonelId);
            personel.PersonelAd = p.PersonelAd;
            personel.PersonelSoyad = p.PersonelSoyad;
            personel.PersonelGorsel = p.PersonelGorsel;
            personel.DepartmanId = p.DepartmanId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}