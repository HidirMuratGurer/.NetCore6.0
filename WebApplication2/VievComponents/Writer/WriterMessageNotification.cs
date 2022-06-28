using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.VievComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        WriterManager wm = new WriterManager(new EfWriterRepository());
        UserManager<AppUser> _userManager;

        public WriterMessageNotification(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var usermail = User.Identity.Name;
            //int id = wm.TGetByFilter(x => x.WriterMail == usermail).WriterID;
            //var values = mm.GetInboxListByWriter(id);

            var usermail = await _userManager.FindByNameAsync(User.Identity.Name);
            int id = usermail.Id;
            var values = mm.GetInboxListByWriter(id);
            return View(values);
        }
    }
}
