using LC.Assets.Components.Exceptions;
using LC.Assets.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
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

        protected void PreProcess(TagHelperContext context, ref TagHelperOutput output, string clasz = default, string style = default, string title = default)
        {
            if (_preProcessed)
            {
                throw new MethodInvokedException("PreProcess");
            }

            if (context.AllAttributes.ContainsName("class"))
            {
                output.Attributes.Add("class", context.AllAttributes["class"]);
            }

            if (!clasz.IsNull())
            {
                output.Attributes.Add("class", clasz);
            }

            if (context.AllAttributes.ContainsName("style"))
            {
                output.Attributes.Add("style", context.AllAttributes["style"]);
            }

            if (!style.IsNull())
            {
                output.Attributes.Add("style", style);
            }

            if (context.AllAttributes.ContainsName("title"))
            {
                output.Attributes.Add("title", context.AllAttributes["title"]);
            }

            if (!title.IsNull())
            {
                output.Attributes.Add("title", title);
            }

            (this.HtmlHelper as IViewContextAware).Contextualize(ViewContext);

            _preProcessed = true;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!_preProcessed)
            {
                throw new MethodNotInvokedException("PreProces", "ProcessAsync");
            }

            await base.ProcessAsync(context, output);
        }

        protected TagBuilder GetTagBuilder(TagHelperContext context, string tagName)
        {
            TagBuilder result = new TagBuilder(tagName);

            if (context.AllAttributes.ContainsName("id"))
            {
                result.Attributes.Add("id", context.AllAttributes["id"].Value.ToString());
            }

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
