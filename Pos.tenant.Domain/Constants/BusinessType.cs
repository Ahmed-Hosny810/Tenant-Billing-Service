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

        public static readonly IReadOnlyList<BusinessTypeOption> Options =
            new List<BusinessTypeOption>
            {
                new() { Code = Restaurant, NameEn = "Restaurant", NameAr = "مطعم" },
                new() { Code = Cafe, NameEn = "Cafe", NameAr = "كافيه" },
                new() { Code = Grocery, NameEn = "Grocery", NameAr = "بقالة" },
                new() { Code = MiniMarket, NameEn = "Mini Market", NameAr = "ميني ماركت" },
                new() { Code = Pharmacy, NameEn = "Pharmacy", NameAr = "صيدلية" },
                new() { Code = Clothing, NameEn = "Clothing Store", NameAr = "محل ملابس" },
                new() { Code = Furniture, NameEn = "Furniture Store", NameAr = "محل أثاث" },
                new() { Code = Cosmetics, NameEn = "Cosmetics Store", NameAr = "محل مستحضرات تجميل" },
                new() { Code = Accessories, NameEn = "Accessories Store", NameAr = "محل إكسسوارات" },
                new() { Code = Electronics, NameEn = "Electronics Store", NameAr = "محل إلكترونيات" },
                new() { Code = Salon, NameEn = "Salon", NameAr = "صالون" },
                new() { Code = GeneralRetail, NameEn = "General Retail", NameAr = "تجزئة عامة" }
            };

        public static bool IsSupported(string businessType)
        {
            return Options.Any(x => x.Code == businessType);
        }
    }
}
