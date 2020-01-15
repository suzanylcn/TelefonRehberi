using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace telefonRehberi.Controllers
{
    
    public class HomeController : Controller
    {
        rehberEntities database = new rehberEntities();
        public ActionResult Index()
        {


            return View();
        }
        
        public ActionResult CalisanListele()
        {
            List<calisanlar> calisanListe = database.calisanlar.ToList();


            return View(calisanListe);
        }

       
       

    }
}