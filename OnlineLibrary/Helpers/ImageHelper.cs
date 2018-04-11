using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Helpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string width, string height, string style, string styleClass)
        {
            TagBuilder tagBuilder = new TagBuilder("img");
            tagBuilder.MergeAttribute("src", src);
            tagBuilder.MergeAttribute("height", height);
            tagBuilder.MergeAttribute("width", width);
            tagBuilder.MergeAttribute("style", style);
            tagBuilder.MergeAttribute("class", styleClass);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}