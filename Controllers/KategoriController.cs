using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProje.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStokProje.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            // var degerler = db.TBLKATEGORILER.ToList();
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost] //formdan gönderme işlemi yapıldıgında
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();//değişiklikleri kaydet

            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var ktg = db.TBLKATEGORILER.Find(id);//arama yapar
            db.TBLKATEGORILER.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
             
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORILER.Find(id);

            return View("KategoriGetir", ktg);
        }
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg= db.TBLKATEGORILER.Find(p1.KATEGORIID);//ıd göre değiştirecek
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}