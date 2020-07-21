/*
    @Date			: 10.12.2019
    @Author         : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core;
using LundbeckConsulting.Components.Core.Repos;
using LundbeckConsulting.Components.Core.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    [HtmlTargetElement("img-over", Attributes = "src", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ImageOverTagHelper : TagHelperCustom, ITagHelperCustom
    {
        public ImageOverTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        {

        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcess(context, output);

            ITagBuilderCustom tag = new TagBuilderCustom("img", TagRenderMode.SelfClosing);
            tag.ConsumeAttributes = true;
            tag.AddAttribute("src", this.Src, false);
            tag.AddAttribute(GetScript(true));
            tag.AddAttribute(GetScript(false));

            AddContent(tag);

            await base.ProcessCustom();
        }

        private string GetOverName() => this.SrcOver.Null() ? this.Src.Substring(0, this.Src.LastIndexOf(".")) + this.Suffix + this.Src.Substring(this.Src.LastIndexOf(".")) : this.SrcOver;

        private ITagBuilderCustomAttribute GetScript(bool onOver)
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

            ITagBuilderCustomAttribute result = new TagBuilderCustomAttribute(onOver ? "onmouseover" : "onmouseout", val, false);

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
        [HtmlAttributeName("src-over")]
        public string SrcOver { get; set; }
    }
}
