using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zia.Data;
using Zia.Models;
using Zia.Models.ViewModel;
using Zia.Utility;


namespace Zia.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class _HomeController : Controller
    {
        private readonly ApplicationDbContext _db;


        public _HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    IndexViewModel IndexVM = new IndexViewModel()
        //    {
        //        Item = await _db.Items.Include(m => m.Category).ToListAsync(),
        //        Category = await _db.Categories.ToListAsync(),


        //    };

        //    var claimsIdentity = (ClaimsIdentity) User.Identity;
        //    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        //    if (claim != null)
        //    {
        //        var cnt = _db.ShopingCarts.Where(u => u.ApplicationUserId == claim.Value).ToList().Count;
        //        HttpContext.Session.SetInt32(SD.customerEndUser, cnt);

        //    }

        //    return View(IndexVM);
        //}

        //[Authorize]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var menuItemFromDb = await _db.Items.Include(m => m.Category).Where(m => m.Id == id).FirstOrDefaultAsync();

        //    ShopingCart cartObj = new ShopingCart()
        //    {
        //        Item = menuItemFromDb,
        //        ItemId = menuItemFromDb.Id
        //    };

        //    return View(cartObj);
        //}


        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Details(ShopingCart CartObject)
        //{
        //    CartObject.Id = 0;
        //    if (ModelState.IsValid)
        //    {
        //        var claimsIdentity = (ClaimsIdentity) this.User.Identity;
        //        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //        CartObject.ApplicationUserId = claim.Value;
        //        ShopingCart cartFromDb = await _db.ShopingCarts.Where(c =>
        //            c.ApplicationUserId == CartObject.ApplicationUserId
        //            && c.ItemId == CartObject.ItemId).FirstOrDefaultAsync();

        //        if (cartFromDb == null)
        //        {
        //            await _db.ShopingCarts.AddAsync(CartObject);
        //        }
        //        else
        //        {
        //            cartFromDb.coutnt = cartFromDb.coutnt + CartObject.coutnt;
        //        }

        //        await _db.SaveChangesAsync();

        //        var count = _db.ShopingCarts.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).ToList()
        //            .Count();
        //        HttpContext.Session.SetInt32(SD.customerEndUser, count);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {

        //        var menuItemFromDb = await _db.Items.Include(m => m.Category).Where(m => m.Id == CartObject.ItemId)
        //            .FirstOrDefaultAsync();

        //        ShopingCart cartObj = new ShopingCart()
        //        {
        //            Item = menuItemFromDb,
        //            ItemId = menuItemFromDb.Id
        //        };

        //        return View(cartObj);
        //    }
        //}
    }
}