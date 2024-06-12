using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static OOO_ChemistShopWeb.ViewModels.ModelMedicines;
using System.Web.Mvc;

namespace OOO_ChemistShopWeb.Classes
{
    public static class PagingHelpers
    {
        /// <summary>
        /// статистический метод для генерации страниц в списке товаров в каталоге
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageInfo"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("txtSelectNum");
                }
                tag.AddCssClass("btn btn-default");
                tag.AddCssClass("txtPagination");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}