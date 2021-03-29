using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMPMGT22.Models;
using EMPMGT22.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EMPMGT22.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<IdentityUser> userManager;
        //private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        //public AccountController( UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }


       


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public  async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, City=model.City };
                var result= await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                  await  signInManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("Index", "Home");
                }

                foreach( var error in result.Errors)

                    {

                    ModelState.AddModelError(" ", error.Description);

                    }

            }



            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)

                { 
                    if (!string.IsNullOrEmpty(returnUrl))
                    {

                        return LocalRedirect(returnUrl);
                    }              
           
                    else
                   {

                     return RedirectToAction("index", "home");

                    }


                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
    }
}


