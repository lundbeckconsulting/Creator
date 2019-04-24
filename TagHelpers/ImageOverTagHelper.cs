﻿using LC.Assets.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("img-over", Attributes = "src", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ImageOverTagHelper : TagHelperBase
    {
        public ImageOverTagHelper(IHostingEnvironment environment, IHtmlHelper html) : base(environment, html)
        {
            
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, ref output);

            string clss = context.AllAttributes.ContainsName("class") ? context.AllAttributes["class"].Value.ToString() : default;

            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Clear();

            output.Attributes.Add("src", this.Src);
            output.Attributes.Add(GetScript(true));
            output.Attributes.Add(GetScript(false));

            if (!clss.IsNull())
            {
                output.Attributes.Add("class", clss);
            }

            await base.ProcessAsync(context, output);
        }

        private string GetOverName()
        {
            string result = this.OverName.IsNull() ? this.Src.Substring(0, this.Src.LastIndexOf(".")) + this.Suffix + this.Src.Substring(this.Src.LastIndexOf(".")) : this.OverName;

            return result;
        }

        private TagHelperAttribute GetScript(bool onOver)
        {
            string val;

            if (onOver)
            {
                val = "this.src='" + GetOverName() + "'";
            }
            else
            {
                val = "this.src='" + this.Src + "'";
            }

            TagHelperAttribute result = new TagHelperAttribute(onOver ? "onmouseover" : "onmouseout", new HtmlString(val), HtmlAttributeValueStyle.DoubleQuotes);

            return result;
        }

        [HtmlAttributeName("src")]
        public string Src { get; set; }

        /// <summary>
        /// The suffix to add to Src. Default is "-over"
        /// </summary>
        /// <example>
        /// Src = "img.jpg", Suffix = "-alt". On over result: "img-alt.jpg"
        /// </example>
        [HtmlAttributeName("suffix")]
        public string Suffix { get; set; } = "-over";

        /// <summary>
        /// Overrides the default naming method and uses the supplied value
        /// </summary>
        [HtmlAttributeName("over-name")]
        public string OverName { get; set; }
    }
}