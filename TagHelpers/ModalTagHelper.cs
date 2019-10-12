using LC.Assets;
using LC.Assets.Components.Extensions;
using LC.Assets.Core.Components.TagHelpers;
using LC.Assets.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("modal", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ModalTagHelper : TagHelperBase
    {
        public ModalTagHelper(IWebHostEnvironment environment, IAssetsDBContextAccessor db, IAssetsConfigWrapper config, IHtmlHelper html) : base(environment, db, config, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, output);

            TagBuilder tmp = GetTag();

            output.SuppressOutput();
            output.Content.AppendHtml(tmp);

            await base.ProcessAsync(context, output);

            TagBuilder GetTag()
            {
                TagBuilder result = new TagBuilder("dialog");
                TagBuilder content = new TagBuilder("content");
                TagBuilder header = new TagBuilder("header");
                TagBuilder title = new TagBuilder("span");
                TagBuilder closeIcon = new TagBuilder("span");
                TagBuilder body = new TagBuilder("section");
                body.AddCssClass("body");
                body.InnerHtml.AppendHtml(output.GetChildContentAsync().Result.GetContent());

                if (!this.Title.IsNull())
                {
                    title.InnerHtml.Append(this.Title);
                    title.AddCssClass("title");
                    header.InnerHtml.AppendHtml(title);
                    // closeIcon.InnerHtml.AppendHtml()
                    closeIcon.AddCssClass("hide-modal");
                    header.InnerHtml.AppendHtml(closeIcon);
                    content.InnerHtml.AppendHtml(header);
                }

                content.InnerHtml.AppendHtml(body);
                result.InnerHtml.AppendHtml(content);

                if (this.Show)
                {
                    result.Attributes.Add("open", "open");
                }

                result.AddCssClass("modal-" + this.Color.ToLower() + "-" + this.Size.ToLower());

                return result;
            }
        }

        [HtmlAttributeName("color")]
        public ColorProfiles Color { get; set; } = ColorProfiles.Default;

        [HtmlAttributeName("size")]
        public ModalSizes Size { get; set; } = ModalSizes.MD;

        [HtmlAttributeName("title")]
        public string Title { get; set; } = default;

        [HtmlAttributeName("show")]
        public bool Show { get; set; } = false;
    }
}