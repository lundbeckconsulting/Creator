using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    public class TagHelperBase : TagHelper
    {
        private bool _preProcessed = false;

        public TagHelperBase(IHostingEnvironment environment, IHtmlHelper html)
        {
            this.Environment = environment;
            this.HtmlHelper = html;
        }

        public void PreProcess(TagHelperContext context, TagHelperOutput output)
        {
            (this.HtmlHelper as IViewContextAware).Contextualize(ViewContext);

            _preProcessed = true;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!_preProcessed)
            {
                throw new Exception("PreProcess must be called before ProcessAsync");
            }

            await base.ProcessAsync(context, output);
        }

        protected TagBuilder GetTagBuilder(TagHelperContext context, string tagName)
        {
            TagBuilder result = new TagBuilder(tagName);

            if (context.AllAttributes.ContainsName("class"))
            {
                result.AddCssClass(context.AllAttributes["class"].Value.ToString());
            }

            if (context.AllAttributes.ContainsName("style"))
            {
                result.Attributes.Add("style", context.AllAttributes["style"].Value.ToString());
            }

            if (context.AllAttributes.ContainsName("title"))
            {
                result.Attributes.Add("title", context.AllAttributes["title"].Value.ToString());
            }

            return result;
        }

        [HtmlAttributeNotBound]
        public IHostingEnvironment Environment { get; }

        [HtmlAttributeNotBound]
        public IHtmlHelper HtmlHelper { get; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeNotBound]
        public ViewDataDictionary ViewData { get { return this.ViewContext.ViewData; } }

        [HtmlAttributeNotBound]
        public CultureInfo CurrentCulture { get { return Thread.CurrentThread.CurrentCulture; } }
    }
}
