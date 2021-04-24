using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zia.Data;

using Zia.Models;

namespace Zia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UislideController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UislideController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var uilisde = await this.db.Uislides.ToListAsync();
            return View(uilisde);
        }

       
        public async Task<IActionResult> Create()
        {
            //var uiList = await db.Uislides.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(Uislide uislide)
        {
            if (ModelState.IsValid)
        {
            string imgDefaultpath = @"\images\zlogo.png";
            var files = HttpContext.Request.Form.Files;
            if (files.Count>0)
            {
                string webrootPath = _webHostEnvironment.WebRootPath;
                string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                FileStream fileStream = new FileStream(Path.Combine(webrootPath, "uislid", imgName), FileMode.Create);
                files[0].CopyTo(fileStream);
                imgDefaultpath = @"\uislid\" + imgName;
            }

            uislide.Img = imgDefaultpath;
            db.Uislides.Add(uislide);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            return View(uislide);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var Ulist = await db.Uislides.FindAsync(id);
            if (Ulist==null)
            {
                return NotFound();
            }

            //var uiList = await db.Uislides.ToListAsync();
            return View(Ulist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EaditPost(Uislide uislide)
        {
            if (ModelState.IsValid)
            {
                string imgDefaultpath = @"\images\zlogo.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webrootPath = _webHostEnvironment.WebRootPath;
                    string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(webrootPath, "uislid", imgName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    imgDefaultpath = @"\uislid\" + imgName;
                }

                uislide.Img = imgDefaultpath;
                db.Uislides.Update(uislide);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(uislide);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ulist = await db.Uislides.FindAsync(id);
            if (Ulist == null)
            {
                return NotFound();
            }

            //var uiList = await db.Uislides.ToListAsync();
            return View(Ulist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Uislide uilslide)
        {
            if (ModelState.IsValid)
            {
                
                db.Uislides.Remove(uilslide);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }

            return View(uilslide);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await db.Uislides.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }


    }
    }

