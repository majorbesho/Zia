using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Zia.Data;
using Zia.Models;
using Zia.Utility;

namespace Zia.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class FamilyController : Controller
    {
        private readonly ApplicationDbContext db;
       
        public FamilyController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.Families.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Family family)
        {

            if (ModelState.IsValid)
            {
                db.Families.Add(family);
               await db.SaveChangesAsync();
               return RedirectToAction(nameof(Index));

            }
            return View(family);
        }
        [HttpGet]
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var family =await db.Families.FindAsync(id);
            if (family==null)
            {
                return NotFound();
            }

            return View(family);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Families.Update(family);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(family);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await db.Families.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Families.Remove(family);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(family);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await db.Families.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }
         
        

    }
}
