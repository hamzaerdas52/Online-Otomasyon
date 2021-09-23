using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sınıflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context context = new Context();
        public ActionResult Index()
        {
            var cari = context.Caris.Count().ToString();
            ViewBag.cariSayisi = cari;

            var urun = context.Uruns.Count().ToString();
            ViewBag.urunSayisi = urun;

            var personel = context.Personels.Count().ToString();
            ViewBag.personelSayisi = personel;

            var kategori = context.Kategoris.Count().ToString();
            ViewBag.kategoriSayisi = kategori;

            var stok = context.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.toplamStok = stok;

            var marka = (from x in context.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.markaSayisi = marka;

            var kritikSeviye = context.Uruns.Count(x => x.Stok <= 60).ToString();
            ViewBag.kritikSeviye = kritikSeviye;

            var maxFiyat = (from x in context.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.maxFiyat = maxFiyat;

            var minFiyat = (from x in context.Uruns orderby x.SatisFiyat select x.UrunAd).FirstOrDefault();
            ViewBag.minFiyat = minFiyat;

            var maxMarka = context.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.maxMarka = maxMarka;

            var televizyon = context.Uruns.Count(x => x.UrunAd == "Televizyon").ToString();
            ViewBag.televizyonSayisi = televizyon;

            var laptop = context.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.laptopSayisi = laptop;

            var enCokSatan = context.Uruns.Where( 
                u => u.UrunId == 
                context.SatisHarekets.GroupBy(x => x.UrunId).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())
                .Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.enCokSatan = enCokSatan;

            var kasa = context.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.kasa = kasa;

            DateTime today = DateTime.Today;
            var bugunSatis = context.SatisHarekets.Count(x => x.Tarih == today).ToString();
            ViewBag.bugunSatis = bugunSatis;

            var bugunKasa = context.SatisHarekets.Where(x => x.Tarih == today).Sum(y => y.ToplamTutar).ToString();
            ViewBag.bugunKasa = bugunKasa;
            return View();
        }
    }
}