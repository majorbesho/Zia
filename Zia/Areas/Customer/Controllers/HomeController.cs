using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
{[Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                List<ShopingCart> shopingCarts= await db.ShopingCarts.Where(m => m.ApplicationUserId == claim.Value).ToListAsync();
                var cnt = db.ShopingCarts.Where(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ShoppingCartCount, shopingCarts.Count);

            }

            IndexViewModel indexVm = new IndexViewModel()
            {
                Category = await db.Categories
                    .ToListAsync(),
                Item =await db.Items
                    .Include(m=>m.Category)
                    .ToListAsync(),
                Uislides = await db.Uislides.ToListAsync()

            };
            return View(indexVm);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int idItem)
        {
            var menuItemFromDb = await db.Items.Include(m => m.Category)
                .Where(m => m.Id == idItem)
                .FirstOrDefaultAsync();

            ShopingCart cartObj = new ShopingCart()
            {
                Item = menuItemFromDb,
                ItemId = menuItemFromDb.Id
            };
            

            

            return View(cartObj);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShopingCart shopingCart)
        {
            
            if (ModelState.IsValid)
            {
                shopingCart.Id = 0;
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                shopingCart.ApplicationUserId = claim.Value;
                ShopingCart cartFromDb = await db.ShopingCarts.Where(c =>
                    c.ApplicationUserId == shopingCart.ApplicationUserId
                    && c.ItemId == shopingCart.ItemId).FirstOrDefaultAsync();

                if (cartFromDb == null)
                {
                    await db.ShopingCarts.AddAsync(shopingCart);
                }
                else
                {
                    cartFromDb.coutnt = cartFromDb.coutnt + shopingCart.coutnt;
                }

                await db.SaveChangesAsync();

                var count = db.ShopingCarts.Where(c => c.ApplicationUserId == shopingCart.ApplicationUserId).ToList()
                    .Count();
                HttpContext.Session.SetInt32(SD.ShoppingCartCount,count);

                return RedirectToAction("Index");
            }
            else
            {

                var menuItemFromDb = await db.Items.Include(m => m.Category).Where(m => m.Id == shopingCart.ItemId)
                    .FirstOrDefaultAsync();

                ShopingCart cartObj = new ShopingCart()
                {
                    Item = menuItemFromDb,
                    ItemId = menuItemFromDb.Id
                };

                return View(cartObj);

            }
        }
    }
}
