using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace telefonRehberi.Controllers
{
    public class PanelController : Controller
    {

        rehberEntities database = new rehberEntities();
        // GET: Panel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DepartmanListele(string mesaj = "")
        {
            List<departman> departmanListe = database.departman.ToList();
            ViewBag.mesaj = mesaj;
            return View(departmanListe);
        }
        public ActionResult DepartmanEkle()
        {
            
            return View();
        }

        public ActionResult DepartmanEkleDb(departman gelen)
        {
            database.departman.Add(gelen);
            database.SaveChanges();

            return RedirectToAction("DepartmanListele");
        }
        public ActionResult DepartmanSil(int silinecekid)
        {
            departman silinecekDepartman = database.departman.FirstOrDefault(d => d.id == silinecekid);


            bool departmanVarmi = database.calisanlar.Any(c => c.departmanId == silinecekDepartman.id);
            if (departmanVarmi==false)
            {
                database.departman.Remove(silinecekDepartman);
                database.SaveChanges();
            }
            else
            {
                return RedirectToAction("DepartmanListele", new { mesaj = "Bu Departmanı Silemezsiniz" });
            }
          
            return RedirectToAction("DepartmanListele");

        }

        public ActionResult DepartmanGuncelle(int guncellenecekid)
        {
            departman guncellenecekDepartman = database.departman.FirstOrDefault(d => d.id == guncellenecekid);
            return View(guncellenecekDepartman);
        }

        public ActionResult DepartmanGuncelleDb(departman gelen)
        {

            departman guncellenecekDepartman = database.departman.FirstOrDefault(d => d.id == gelen.id);
            guncellenecekDepartman.departmanAdi = gelen.departmanAdi;
            database.SaveChanges();
            return RedirectToAction("DepartmanListele");
        }

        public ActionResult CalisanListele(string mesaj = "")
        {
            List<calisanlar> calisanListe = database.calisanlar.ToList();

            ViewBag.mesaj = mesaj;
            
            return View(calisanListe);
        }
        public ActionResult CalisanEkle()
        {
            List<departman> departmanListe = database.departman.ToList();
            ViewBag.DepartmanListem = departmanListe; 

            List<calisanlar> calisanListe = database.calisanlar.ToList();
            ViewBag.CalisanListem = calisanListe;
            return View();
        }
        public ActionResult CalisanEkleDb(calisanlar gelen)
        {
            if (gelen.yoneticiId == 0)
            {
                gelen.yoneticiId = null;
            }
            database.calisanlar.Add(gelen);
            database.SaveChanges();
            return RedirectToAction("CalisanListele");
        }
        public ActionResult CalisanSil(int id)
        {
            calisanlar silinecekCalisan = database.calisanlar.FirstOrDefault(c => c.id == id);

            List<calisanlar> calisanList = new List<calisanlar>();


            bool calisanVarmi = database.calisanlar.Any(c => c.yoneticiId == silinecekCalisan.id);

            if (calisanVarmi == false)
            {
                database.calisanlar.Remove(silinecekCalisan);
                database.SaveChanges();
            }
            else
            {

                return RedirectToAction("CalisanListele",new { mesaj="Bu Kişiyi Silemezsiniz"});
            }

            return RedirectToAction("CalisanListele");
        }
        public ActionResult CalisanGuncelle(int id)
        {
            List<departman> departmanListe = database.departman.ToList();
            ViewBag.DepartmanListem = departmanListe;

            List<calisanlar> calisanListe = database.calisanlar.Where(c => c.id != id).ToList();
            ViewBag.CalisanListem = calisanListe;

            calisanlar guncellenecekCalisan = database.calisanlar.FirstOrDefault(c => c.id == id);
            return View(guncellenecekCalisan);
        }
        public ActionResult CalisanGuncelleDb(calisanlar gelen)
        {
            if (gelen.yoneticiId == 0)
            {
                gelen.yoneticiId = null;
            }
            calisanlar guncellenecekCalisan = database.calisanlar.FirstOrDefault(c => c.id == gelen.id);
            guncellenecekCalisan.ad = gelen.ad;
            guncellenecekCalisan.soyad = gelen.soyad;
            guncellenecekCalisan.tel = gelen.tel;
            guncellenecekCalisan.departmanId = gelen.departmanId;
            guncellenecekCalisan.yoneticiId = gelen.yoneticiId;
            database.SaveChanges();
            return RedirectToAction("CalisanListele");
        }


    }
}