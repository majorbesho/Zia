using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zia.Data;

namespace Zia.Utility
{
    public static class SD
    {
        public const string ManagerUser = "Manager";
        public const string StorUser = "store";
        public const string AccoutningUser = "Accoutning";
        public const string customerEndUser = "customer";
        public const string SalesUser = "Sales";
        public const string salesManageruser = "SalesManager";
        public const string PurchasesManagerUser = "Purchases Manager";
        public const string PurchasesUserUser = "Purchases User";
        public const string ShoppingCartCount = "ShoppingCartCount";

        public const string ssShoppingCartCount = "ssCartCount";
        public const string ssCouponCode = "ssCouponCode";

        public const string StatusSubmitted = "Submitted";
        public const string StatusInProcess = "Being Prepared";
        public const string StatusReady = "Ready for Pickup";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";



        public static double DiscountedPrice(Models.Coupon couponFromDb, double OriginalOrderTotal)
        {
            if (couponFromDb == null)
            {
                return OriginalOrderTotal;
            }
            else
            {
                if (couponFromDb.MinimumAmount > OriginalOrderTotal)
                {
                    return OriginalOrderTotal;
                }
                else
                {
                    //everything is valid
                    if (Convert.ToInt32(couponFromDb.Type) == (int)Models.Coupon.etype.total)
                    {
                        //$10 off $100
                        return Math.Round(OriginalOrderTotal - couponFromDb.Dicount, 2);
                    }
                    if (Convert.ToInt32(couponFromDb.Type) == (int)Models.Coupon.etype.percent)
                    {
                        //10% off $100
                        return Math.Round(OriginalOrderTotal - (OriginalOrderTotal * couponFromDb.Dicount / 100), 2);
                    }
                }
            }
            return OriginalOrderTotal;
        }

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

    }


   
}
