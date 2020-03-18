using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.Infrastructure
{
    public class SortParamContract
    {
        public string NameAsc { get; } = "name_asc";
        public string NameDesc { get; } = "name_desc";
        public string DescriptionAsc { get; } = "description_asc";
        public string DescriptionDesc { get; } = "description_desc";
        public string CompositionAsc { get; } = "composition_asc";
        public string CompositionDesc { get; } = "composition_desc";
        public string PriceAsc { get; } = "priceasc";
        public string PriceDesc { get; } = "pricedesc";
        public string MassAsc { get; } = "massasc";
        public string MassDesc { get; } = "massdesc";
        public string CalorieContentAsc { get; } = "caloriecontentasc";
        public string CalorieContentDesc { get; } = "caloriecontentdesc";
        public string CookingTimeAsc { get; } = "cookingtimeasc";
        public string CookingTimeDesc { get; } = "cookingtimedesc";
        public string AddingDateAsc { get; } = "addingdateasc";
        public string AddingDateDesc { get; } = "addingdatedesc";
    }
}
