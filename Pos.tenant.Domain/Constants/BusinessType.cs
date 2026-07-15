using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Constants
{
    public static class BusinessType
    {
        public const string Restaurant = "RESTAURANT";
        public const string Cafe = "CAFE";
        public const string Grocery = "GROCERY";
        public const string MiniMarket = "MINI_MARKET";
        public const string Pharmacy = "PHARMACY";
        public const string Clothing = "CLOTHING";
        public const string Furniture = "FURNITURE";
        public const string Cosmetics = "COSMETICS";
        public const string Accessories = "ACCESSORIES";
        public const string Electronics = "ELECTRONICS";
        public const string Salon = "SALON";
        public const string GeneralRetail = "GENERAL_RETAIL";

        public static readonly IReadOnlyCollection<string> All = new[]
        {
            Restaurant,
            Cafe,
            Grocery,
            MiniMarket,
            Pharmacy,
            Clothing,
            Furniture,
            Cosmetics,
            Accessories,
            Electronics,
            Salon,
            GeneralRetail
        };

        public static bool IsSupported(string businessType)
        {
            return All.Contains(businessType);
        }
    }
}
