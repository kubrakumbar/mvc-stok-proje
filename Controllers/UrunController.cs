using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProje.Models.Entity;

namespace MvcStokProje.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        //listeleme
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        //ekleme
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = db.TBLKATEGORILER
                                 .Select(i => new SelectListItem
                                 {
                                     Text = i.KATEGORIAD,
                                     Value = i.KATEGORIID.ToString()
                                 }).ToList();

            ViewBag.dgr = degerler;
            //hata veriyor !!
            // var ktg = db.TBLKATEGORILER.ToList();
            //ViewBag.dgr = ktg.Select(i => new SelectListItem
            //{
            //    Text = i.KATEGORIAD,
            //    Value = i.KATEGORIID.ToString()
            //}).ToList();

            return View();
        }
        [HttpPost] //kullanıcıdan veri geleekse
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(x => x.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urn = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urn = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = db.TBLKATEGORILER
                                .Select(i => new SelectListItem
                                {
                                    Text = i.KATEGORIAD,
                                    Value = i.KATEGORIID.ToString()
                                }).ToList();

            ViewBag.dgr = degerler;
            return View("UrunGetir", urn);
        }
        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urn = db.TBLURUNLER.Find(p.URUNID);
            urn.URUNAD = p.URUNAD;
            urn.MARKA = p.MARKA;
            urn.STOK = p.STOK;
            urn.FIYAT = p.FIYAT;
            //urn.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(x => x.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI= ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}