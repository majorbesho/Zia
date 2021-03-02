using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zia.Data;
using Zia.Models;
using Zia.Utility;

namespace Zia.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;
        [TempData]
        public string StatusMessage { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            this.db = db;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Categories.Include(c => c.Family);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await db.Categories
                .Include(c => c.Family)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            ViewData["FamilyId"] = new SelectList(db.Families, "Id", "Name");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Category category)
        {
            
            if (ModelState.IsValid)
            {
                string imgDefaultpath = @"\images\302.jpg";
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webrootPath = _webHostEnvironment.WebRootPath;
                    string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(webrootPath, "images", imgName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    imgDefaultpath = @"\images\" + imgName;
                }

                category.CatImg = imgDefaultpath;
                db.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyId"] = new SelectList(db.Families, "Id", "Name", category.FamilyId);
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["FamilyId"] = new SelectList(db.Families, "Id", "Name", category.FamilyId);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
          
            if (ModelState.IsValid)
            {
                string imgDefaultpath = @"\images\302.jpg";
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webrootPath = _webHostEnvironment.WebRootPath;
                    string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream =
                        new FileStream(Path.Combine(webrootPath, "images", imgName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    imgDefaultpath = @"\images\" + imgName;
                }
                category.CatImg = imgDefaultpath;
                db.Categories.Update(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
 
               
            }
            ViewData["FamilyId"] = new SelectList(db.Families, "Id", "Name", category.FamilyId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await db.Categories
                .Include(c => c.Family)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await db.Categories.FindAsync(id);
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Any(e => e.Id == id);
        }
    }
}
