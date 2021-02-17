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
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            this.db = db;
        }

        // GET: Admin/Items
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Items.Include(i => i.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await db.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Admin/Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem( Item item)
        {
            if (ModelState.IsValid)
            {
                string imgDefaultpath = @"\images\302.jpg";
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                     string webrootPath = _webHostEnvironment.WebRootPath;
                    string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(webrootPath, "images", imgName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    imgDefaultpath = @"\images\" + imgName;
                }
                
                item.img = imgDefaultpath;
                db.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Admin/Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // POST: Admin/Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            if (!ItemExists(item.Id))
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
                    item.img = imgDefaultpath;
                    db.Update(item);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

            }

            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Admin/Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await db.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Admin/Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return db.Items.Any(e => e.Id == id);
        }
    }
}
