using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace telefonRehberi.Controllers
{
    public class LoginController : Controller
    {
        rehberEntities database = new rehberEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Giris(string hata = "")
        {
            ViewBag.Hata = hata;

            return View();
        }

        public ActionResult GirisKontrol(Login gelen)
        {
            var kullanıcı = database.Login.FirstOrDefault(x => x.kullaniciAdi == gelen.kullaniciAdi && x.sifre == gelen.sifre);
            if (kullanıcı != null)
            {
                Session["kullaniciAdi"] = kullanıcı;
                return RedirectToAction("Index", "Panel");
            }
            

            return RedirectToAction("Giris","Login",new { hata="Kullanıcı Adı veya Şifre Yanlış"});
        }

    }
}