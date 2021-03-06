using Microsoft.AspNetCore.Mvc.Rendering;
using rentend.Extensions;
public static class IEnumerableExtension
{
    public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue, string v)
    {
        return from item in items
            select new SelectListItem
            {
                Text = item.GetPropertyValue(v),
                Value = item.GetPropertyValue("Id"),
                Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
            };
    }

}