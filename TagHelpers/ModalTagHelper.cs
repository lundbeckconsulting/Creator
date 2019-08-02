using LC.Assets.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("modal", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ModalTagHelper : TagHelperBase
    {
        private readonly TagHelperRepo _helper = new TagHelperRepo();

        public ModalTagHelper(IHostingEnvironment env, IHtmlHelper html) : base(env, html) { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, ref output);

            TagBuilder tmp = GetTag();

            output.SuppressOutput();
            output.Content.AppendHtml(tmp);

            await base.ProcessAsync(context, output);

            TagBuilder GetTag()
            {
                TagBuilder result = GetTagBuilder("dialog");
                TagBuilder content = GetTagBuilder("content");
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
                    closeIcon.InnerHtml.AppendHtml(_helper.GetIconTag(Symbols.CloseFull, SymbolOutputs.Span, SymbolSizes.None, SymbolLanguages.Auto, default, "Lukk", "#", true, false, false, ColorProfiles.Danger));
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