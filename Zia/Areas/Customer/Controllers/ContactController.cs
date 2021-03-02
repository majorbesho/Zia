using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Zia.Data;
using Zia.Models;

namespace Zia.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext db;
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;

        public ContactController(ApplicationDbContext db, EmailAddress _fromAddress,
            IEmailService _emailService)
        {
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
            this.db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", new Contact());
        }
        

        
       
        [HttpPost]
        public async Task<IActionResult> Index(Contact contact)
        {

            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    db.Contacts.Add(contact);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                return RedirectToAction();

              
            }
            return View(contact);
        }
    }

};
