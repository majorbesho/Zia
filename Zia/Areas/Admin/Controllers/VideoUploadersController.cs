using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zia.Data;
using Zia.Models;

namespace Zia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VideoUploadersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VideoUploadersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/VideoUploaders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VideoUploaders.Include(v => v.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/VideoUploaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoUploader = await _context.VideoUploaders
                .Include(v => v.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoUploader == null)
            {
                return NotFound();
            }

            return View(videoUploader);
        }

        // GET: Admin/VideoUploaders/Create
        public IActionResult Create()
        {
            ViewData["categoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/VideoUploaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VideoUploader videoUploader)
        {
            if (ModelState.IsValid)
            {
                string imgDefaultpath = @"\images\zlogo.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webrootPath = _webHostEnvironment.WebRootPath;
                    string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(webrootPath, "video", imgName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    imgDefaultpath = @"\video\" + imgName;
                }
                videoUploader.Url = imgDefaultpath;
                _context.VideoUploaders.Add(videoUploader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryId"] = new SelectList(_context.Categories, "Id", "Name", videoUploader.categoryId);
            return View(videoUploader);
        }

        // GET: Admin/VideoUploaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoUploader = await _context.VideoUploaders.FindAsync(id);
            if (videoUploader == null)
            {
                return NotFound();
            }
            ViewData["categoryId"] = new SelectList(_context.Categories, "Id", "Name", videoUploader.categoryId);
            return View(videoUploader);
        }

        // POST: Admin/VideoUploaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,categoryId")] VideoUploader videoUploader)
        {
            if (id != videoUploader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string imgDefaultpath = @"\images\zlogo.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webrootPath = _webHostEnvironment.WebRootPath;
                    string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(webrootPath, "video", imgName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    imgDefaultpath = @"\video\" + imgName;
                }
                videoUploader.Url = imgDefaultpath;
                _context.VideoUploaders.Update(videoUploader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryId"] = new SelectList(_context.Categories, "Id", "Name", videoUploader.categoryId);
            return View(videoUploader);
            
        }

        // GET: Admin/VideoUploaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoUploader = await _context.VideoUploaders
                .Include(v => v.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoUploader == null)
            {
                return NotFound();
            }

            return View(videoUploader);
        }

        // POST: Admin/VideoUploaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoUploader = await _context.VideoUploaders.FindAsync(id);
            _context.VideoUploaders.Remove(videoUploader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoUploaderExists(int id)
        {
            return _context.VideoUploaders.Any(e => e.Id == id);
        }
    }
}
