using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Zia.Data;
using Zia.Utility;

namespace Zia.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;

        public UserController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            var claimesIdentity = (ClaimsIdentity) User.Identity;
            var claims = claimesIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claims.Value;

            return View(await db.ApplicationUsers.Where(m=>m.Id != userId).ToListAsync());

        }
        public async Task<IActionResult> LockUnLock(string? id )
        {
            if (id ==null)
            {
                return NotFound();
            }

            var user = await db.ApplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(999);
            }
            else
            {
                user.LockoutEnd = DateTime.Now;
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
