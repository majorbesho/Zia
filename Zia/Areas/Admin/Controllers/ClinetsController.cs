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
    public class ClinetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClinetsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
           _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Clinets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clinets.ToListAsync());
        }

        // GET: Admin/Clinets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinet = await _context.Clinets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinet == null)
            {
                return NotFound();
            }

            return View(clinet);
        }

        // GET: Admin/Clinets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Clinets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Clinet clinet)
        {
            if (ModelState.IsValid)
            {
                string imgDefaultpath = @"\images\zlogo.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webrootPath = _webHostEnvironment.WebRootPath;
                    string imgName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(webrootPath, "clinte", imgName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    imgDefaultpath = @"\clinte\" + imgName;
                }

                clinet.Img = imgDefaultpath;
                _context.Clinets.Add(clinet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinet);
        }

        // GET: Admin/Clinets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinet = await _context.Clinets.FindAsync(id);
            if (clinet == null)
            {
                return NotFound();
            }
            return View(clinet);
        }

        // POST: Admin/Clinets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Img,IsActive")] Clinet clinet)
        {
            if (id != clinet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinetExists(clinet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clinet);
        }

        // GET: Admin/Clinets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinet = await _context.Clinets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinet == null)
            {
                return NotFound();
            }

            return View(clinet);
        }

        // POST: Admin/Clinets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clinet = await _context.Clinets.FindAsync(id);
            _context.Clinets.Remove(clinet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinetExists(int id)
        {
            return _context.Clinets.Any(e => e.Id == id);
        }
    }
}
