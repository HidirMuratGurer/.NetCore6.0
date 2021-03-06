using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class NewsLatterController : Controller
    {
        NewsLatterManager nm = new NewsLatterManager(new EfNewsLatterRepository());
        [HttpGet]
        
        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }

        [HttpPost]

        public PartialViewResult SubscribeMail(NewsLatter p)
        {
            p.MailStatus = true;
            nm.TAdd(p);    
            return PartialView();
        }
    }
}
