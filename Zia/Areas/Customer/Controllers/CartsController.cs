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
{
    [Area("Customer")]
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext db;

        public CartsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public OrderDetailsCartViewModel OrderDetailsCartVM { get; set; }

        public IActionResult Index()
        {
             OrderDetailsCartVM = new OrderDetailsCartViewModel()
            {
                 OrderHeader = new Models.OrderHeader()
            };
             OrderDetailsCartVM.OrderHeader.orderTotal = 0;
             var claimsIdentity = (ClaimsIdentity) User.Identity;
             var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

             var shoppingcarts = db.ShopingCarts.Where(c => c.ApplicationUserId == claims.Value);
             if (shoppingcarts!= null)
             {  
                 OrderDetailsCartVM.ShopingCartsList = shoppingcarts.ToList();
             }

             foreach (var items in OrderDetailsCartVM.ShopingCartsList)
             {
                items.Items = db.Items.FirstOrDefault(m => m.Id == items.ItemId);
                OrderDetailsCartVM.OrderHeader.orderTotal += items.Items.priceAfterDisCont* items.coutnt;
                if (!string.IsNullOrEmpty(items.Items.shortDis))
                {
                    items.Items.shortDis = SD.ConvertToRawHtml(items.Items.shortDis);
                }
                
             }

             OrderDetailsCartVM.OrderHeader.OrderTotalOrginal = OrderDetailsCartVM.OrderHeader.orderTotal;



            return View(OrderDetailsCartVM);
        }


        public async Task<IActionResult> Summary()
        {

            OrderDetailsCartVM = new OrderDetailsCartViewModel()
            {
                OrderHeader = new Models.OrderHeader()
            };

            OrderDetailsCartVM.OrderHeader.orderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = await db.ApplicationUsers.Where(c => c.Id == claim.Value).FirstOrDefaultAsync();
            var cart = db.ShopingCarts.Where(c => c.ApplicationUserId == claim.Value);
            if (cart != null)
            {
                OrderDetailsCartVM.ShopingCartsList = cart.ToList();
            }

            foreach (var list in OrderDetailsCartVM.ShopingCartsList)
            {
                list.Items = await db.Items.FirstOrDefaultAsync(m => m.Id == list.ItemId);
                OrderDetailsCartVM.OrderHeader.orderTotal = OrderDetailsCartVM.OrderHeader.orderTotal + (list.Items.priceAfterDisCont * list.coutnt);

            }
            OrderDetailsCartVM.OrderHeader.OrderTotalOrginal = OrderDetailsCartVM.OrderHeader.orderTotal;
            OrderDetailsCartVM.OrderHeader.PickupName = applicationUser.Name;
            OrderDetailsCartVM.OrderHeader.TelePhone = applicationUser.PhoneNumber;
            OrderDetailsCartVM.OrderHeader.PickUptime = DateTime.Now;


            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                OrderDetailsCartVM.OrderHeader.CoupinCode = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await db.Coupons.Where(c => c.Name.ToLower()
                                                               == OrderDetailsCartVM.OrderHeader.CoupinCode
                                                                   .ToLower()).FirstOrDefaultAsync();
                OrderDetailsCartVM.OrderHeader.orderTotal = SD.DiscountedPrice(couponFromDb, OrderDetailsCartVM.OrderHeader.OrderTotalOrginal);
            }


            return View(OrderDetailsCartVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            OrderDetailsCartVM.ShopingCartsList = await db.ShopingCarts
                .Where(c => c.ApplicationUserId == claim.Value)
                .ToListAsync();

            OrderDetailsCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            OrderDetailsCartVM.OrderHeader.OrderDate = DateTime.Now;
            OrderDetailsCartVM.OrderHeader.UserId = claim.Value;
            OrderDetailsCartVM.OrderHeader.Status = SD.PaymentStatusPending;
            OrderDetailsCartVM.OrderHeader.PickUptime = Convert.ToDateTime(OrderDetailsCartVM.OrderHeader.PickupDate.ToShortDateString() + " " + OrderDetailsCartVM.OrderHeader.PickUptime.ToShortTimeString());

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            db.OrderHeaders.Add(OrderDetailsCartVM.OrderHeader);
            await db.SaveChangesAsync();

            OrderDetailsCartVM.OrderHeader.OrderTotalOrginal = 0;


            foreach (var item in OrderDetailsCartVM.ShopingCartsList)
            {
                item.Items = await db.Items.FirstOrDefaultAsync(m => m.Id == item.ItemId);
                OrderDetails orderDetails = new OrderDetails
                {
                    ItemId = item.ItemId,
                    OrderId = OrderDetailsCartVM.OrderHeader.Id,
                    Discription = item.Items.Specifications,
                    Name = item.Items.Name,
                    Price = item.Items.publicPrice,
                     count= item.coutnt
                };
                OrderDetailsCartVM.OrderHeader.OrderTotalOrginal += orderDetails.count * orderDetails.Price;
                db.OrderDetailses.Add(orderDetails);

            }

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                OrderDetailsCartVM.OrderHeader.CoupinCode = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await db.Coupons.Where(c => c.Name.ToLower() == OrderDetailsCartVM.OrderHeader
                                                                   .CoupinCode.ToLower()).FirstOrDefaultAsync();
                OrderDetailsCartVM.OrderHeader.orderTotal = SD.DiscountedPrice(couponFromDb, OrderDetailsCartVM.OrderHeader.OrderTotalOrginal);
            }
            else
            {
                OrderDetailsCartVM.OrderHeader.orderTotal = OrderDetailsCartVM.OrderHeader.OrderTotalOrginal;
            }
            OrderDetailsCartVM.OrderHeader.coupinDiscount = OrderDetailsCartVM.OrderHeader.OrderTotalOrginal - OrderDetailsCartVM.OrderHeader.orderTotal;

            db.ShopingCarts.RemoveRange(OrderDetailsCartVM.ShopingCartsList);
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, 0);
            await db.SaveChangesAsync();

            //var options = new ChargeCreateOptions
            //{
            //    Amount = Convert.ToInt32(detailCart.OrderHeader.OrderTotal * 100),
            //    Currency = "usd",
            //    Description = "Order ID : " + detailCart.OrderHeader.Id,
            //    Source = stripeToken

            //};
            //var service = new ChargeService();
            //Charge charge = service.Create(options);

            //if (charge.BalanceTransactionId == null)
            //{
            //    detailCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
            //}
            //else
            //{
            //    detailCart.OrderHeader.TransactionId = charge.BalanceTransactionId;
            //}

            //if (charge.Status.ToLower() == "succeeded")
            //{
            //    detailCart.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
            //    detailCart.OrderHeader.Status = SD.StatusSubmitted;
            //}
            //else
            //{
            //    detailCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
            //}

            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }


        public IActionResult AddCoupon()
        {
            if (OrderDetailsCartVM.OrderHeader.CoupinCode == null)
            {
                OrderDetailsCartVM.OrderHeader.CoupinCode = "";
            }
            HttpContext.Session.SetString(SD.ssCouponCode, OrderDetailsCartVM.OrderHeader.CoupinCode);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveCoupon()
        {

            HttpContext.Session.SetString(SD.ssCouponCode, string.Empty);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Plus(int cartId)
        {
            var cart = await db.ShopingCarts.FirstOrDefaultAsync(c => c.Id == cartId);
            cart.coutnt += 1;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int cartId)
        {
            var cart = await db.ShopingCarts.FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart.coutnt == 1)
            {
                db.ShopingCarts.Remove(cart);
                await db.SaveChangesAsync();

                var cnt = db.ShopingCarts.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);
            }
            else
            {
                cart.coutnt -= 1;
                await db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int cartId)
        {
            var cart = await db.ShopingCarts.FirstOrDefaultAsync(c => c.Id == cartId);

            db.ShopingCarts.Remove(cart);
            await db.SaveChangesAsync();

            var cnt = db.ShopingCarts.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);


            return RedirectToAction(nameof(Index));
        }



    }
}
