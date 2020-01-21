using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SMTPConfifuration.Helper;
using SMTPConfifuration.Model;

namespace SMTPConfifuration.Controllers
{
    public class HomeController : Controller
    {

        private EmailHelper _emailHelper;
        public HomeController(IConfiguration iConfiguration)
        {
            _emailHelper = new EmailHelper(iConfiguration);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SendEmail()
        {
            try
            {
                var emailModel = new EmailModel("devtest318@gmail.com", // To  
                    "Email Test", // Subject  
                    "Sending Email using Asp.Net Core.", // Message  
                    true // IsBodyHTML  
                );
                _emailHelper.SendEmail(emailModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index");
        }
    }
}