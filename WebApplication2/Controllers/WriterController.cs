using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Admin,Moderator,Writer")]
    public class WriterController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        WriterManager wm = new WriterManager(new EfWriterRepository());
        UserManager<AppUser> _userManager;
        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = wm.TGetByFilter(x=>x.WriterMail==usermail).WriterName;
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var writer = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.namesurname = writer.NameSurname;
            model.stringimageurl = writer.ImageUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel p)
        {
            var writer = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.namesurname = writer.NameSurname;
            model.stringimageurl = writer.ImageUrl;
            if (p.imageurl != null)
            {
                var extension = Path.GetExtension(p.imageurl.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.imageurl.CopyTo(stream);
                writer.ImageUrl = "/WriterImageFiles/" + newimagename;
            }
            writer.NameSurname = p.namesurname;
            bool pasword = await _userManager.CheckPasswordAsync(writer, p.password);
            if (pasword)
            {
                var result =await _userManager.UpdateAsync(writer);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("PasswordError", "Girdiğiniz Şifre Yanlış");
            }
            return View(model);
           
        }
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();
            if (p.WriterImage != null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                w.WriterImage = "wwwroot/WriterImageFiles/" +newimagename;
            }
            w.WriterMail = p.WriterMail;
            w.WriterName = p.WriterName;
            w.WriterPassword = p.WriterPassword;
            w.WriterStatus = true;
            w.WriterAbout = p.WriterAbout;
            wm.TAdd(w);
            return RedirectToAction("Index", "Dashboard");
           
        }
        [HttpGet]
        public async Task<IActionResult> WriterProfile()
        {
            var writer =await _userManager.FindByNameAsync(User.Identity.Name);
            return View(writer);
        }
        [HttpGet]
        public async Task<IActionResult> WriterChangePassword()
        {
            UserPasswordChangeModel model = new UserPasswordChangeModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> WriterChangePassword(UserPasswordChangeModel user)
        {
            var writer = await _userManager.FindByNameAsync(User.Identity.Name);
            var pasword = await _userManager.ChangePasswordAsync(writer, user.oldpassword, user.newpassword);
            if (pasword.Succeeded)
            {
                var result = await _userManager.UpdateAsync(writer);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }    
            }
            else
            {
                foreach (var item in pasword.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
               
            }

            return View(user);
        }

    }
}
