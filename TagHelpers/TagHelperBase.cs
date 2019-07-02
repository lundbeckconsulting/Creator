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

        protected void PreProcess(TagHelperContext context, ref TagHelperOutput output, string id = default, string clasz = default, string style = default, string title = default)
        {
            this.Context = context;
            this.Output = output;

            if (_preProcessed)
            {
                throw new MethodInvokedException("PreProcess");
            }

            ProcessBaseTags(id, clasz, style, title);

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

        protected TagBuilder GetTagBuilder(string tagName)
        {
            TagBuilder result = new TagBuilder(tagName);

            if (this.Context.AllAttributes.ContainsName("id"))
            {
                result.Attributes.Add("id", this.Context.AllAttributes["id"].Value.ToString());
            }

            if (this.Context.AllAttributes.ContainsName("class"))
            {
                result.AddCssClass(this.Context.AllAttributes["class"].Value.ToString());
            }

            if (this.Context.AllAttributes.ContainsName("style"))
            {
                result.Attributes.Add("style", this.Context.AllAttributes["style"].Value.ToString());
            }

            if (this.Context.AllAttributes.ContainsName("title"))
            {
                result.Attributes.Add("title", this.Context.AllAttributes["title"].Value.ToString());
            }

            return result;
        }

        private void ProcessBaseTags(string id = default, string clasz = default, string style = default, string title = default)
        {
            if (!clasz.IsNull())
            {
                this.Output.Attributes.Add("class", clasz);
            }
            else if (this.Context.AllAttributes.ContainsName("class"))
            {
                this.Output.Attributes.Add("class", this.Context.AllAttributes["class"]);
            }

            if (!style.IsNull())
            {
                this.Output.Attributes.Add("style", style);
            }
            else if (this.Context.AllAttributes.ContainsName("style"))
            {
                this.Output.Attributes.Add("style", this.Context.AllAttributes["style"]);
            }

            if (!title.IsNull())
            {
                this.Output.Attributes.Add("title", title);
            }
            else if (this.Context.AllAttributes.ContainsName("title"))
            {
                this.Output.Attributes.Add("title", this.Context.AllAttributes["title"]);
            }

            if (!id.IsNull())
            {
                this.Output.Attributes.Add("id", id);
            }
            else if (this.Context.AllAttributes.ContainsName("id"))
            {
                this.Output.Attributes.Add("id", this.Context.AllAttributes["id"]);
            }
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

        [HtmlAttributeNotBound]
        public TagHelperContext Context { get; private set; }

        [HtmlAttributeNotBound]
        public TagHelperOutput Output { get; private set; }
    }
}
