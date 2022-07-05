using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace DEF.Shared.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString PartialSection(this HtmlHelper htmlHelper, string type, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items[string.Concat("_", type, "_") + Guid.NewGuid()] = template;
            return MvcHtmlString.Empty;
        }
        public static IHtmlString RenderPartialSection(this HtmlHelper htmlHelper, string type)
        {
            List<Func<object, HelperResult>> temp = new List<Func<object, HelperResult>>();
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith(string.Concat("_", type, "_")))
                {
                    var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        temp.Add(template);
                    }
                }
            }

            if (temp.Any())
            {
                IList<string> htmlInvokedList = new List<string>();
                foreach (Func<object, HelperResult> item in temp)
                {
                    string invokedItem = item.Invoke(null).ToString();
                    if (!htmlInvokedList.Contains(invokedItem))
                    {
                        htmlInvokedList.Add(invokedItem);
                        htmlHelper.ViewContext.Writer.Write(item(null));
                    }
                }
            }
            return MvcHtmlString.Empty;
        }
    }
}
