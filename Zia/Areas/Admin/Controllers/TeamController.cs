using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zia.Data;
using Zia.Models;
using Zia.Utility;

namespace Zia.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Teams;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await db.Teams
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
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

                team.Img = imgDefaultpath;
                db.Teams.Add(team);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(team);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await db.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.Id)
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
                team.Img = imgDefaultpath;
                db.Teams.Update(team);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await db.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await db.Teams.FindAsync(id);
            db.Teams.Remove(team);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return db.Teams.Any(e => e.Id == id);
        }

    }
}
