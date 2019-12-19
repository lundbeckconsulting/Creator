/*
    @Date			: 10.12.2019
    @Author         : Stein Lundbeck
*/

using LC.Assets;
using LC.Assets.Components;
using LC.Assets.Components.Extensions;
using LC.Assets.Core.Components.TagHelpers;
using LC.Assets.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    [HtmlTargetElement("chapter", TagStructure = TagStructure.NormalOrSelfClosing)]
    [RestrictChildren("content")]
    public class ChapterTagHelper : TagHelperBase
    {
        public ChapterTagHelper(IWebHostEnvironment environment, IAssetsDBContextAccessor db, IAssetsConfigWrapper config, IHtmlHelper html) : base(environment, db, config, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, output);

            ChapterContext cntx = new ChapterContext();
            context.Items.Add(typeof(ChapterTagHelper), cntx);
            await output.GetChildContentAsync();

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass("chapter");
            tag.AddCssClass("cp-" + this.Color.ToString().ToLower());

            if (!this.Border.Equals(ChapterBorderStyles.None))
            {
                tag.AddCssClass("border-" + this.Border.ToString().ToLower());
            }

            string pre = cntx.Pre, main = cntx.Main;

            if (this.Pre != default)
            {
                pre = this.Pre;
            }

            if (this.Main != default)
            {
                main = this.Main;
            }

            if (this.LinkStyle.Equals(ChapterLinkStyles.Link))
            {
                pre += " " + GetReadMoreLink().ToStringValue();
            }

            tag.InnerHtml.AppendLine(GetContentTag(ChapterContentTypes.Pre, pre));

            if (this.LinkStyle.Equals(ChapterLinkStyles.Button))
            {
                tag.InnerHtml.AppendLine(GetReadMoreButton());
            }

            tag.InnerHtml.AppendLine(GetContentTag(ChapterContentTypes.Main, main));

            output.SuppressOutput();
            output.PostElement.AppendLine(tag);

            await base.ProcessAsync(context, output);
        }

        private TagBuilder GetContentTag(ChapterContentTypes type, string content)
        {
            TagBuilder result = new TagBuilder("div");
            result.AddCssClass("content");
            result.AddCssClass(type.ToLower());

            if (type.Equals(ChapterContentTypes.Main))
            {
                result.AddCssClass("bp-" + this.Breakpoint.ToLower());
            }

            result.InnerHtml.AppendHtmlLine(content);

            return result;
        }

        private TagBuilder GetReadMoreButton()
        {
            TagBuilder wrap = new TagBuilder("div");
            wrap.AddCssClass("wrap");
            wrap.AddCssClass("more");
            wrap.AddCssClass("pad-doc");
            wrap.AddCssClass("align-center");
            wrap.AddCssClass($"bp-{this.Breakpoint.ToLower()}");


            TagBuilder btn = new TagBuilder("input");
            btn.AddCssClass("more");
            btn.AddCssClass("invoke");
            btn.AddCssClass("button");
            btn.AddCssClass("cp-" + this.Color.ToLower());
            btn.Attributes.Add("type", "button");
            btn.Attributes.Add("value", "Les mer");
            btn.Attributes.Add("title", "Les mer");

            wrap.InnerHtml.SetHtmlContent(btn);

            return wrap;
        }

        private TagBuilder GetReadMoreLink()
        {
            TagBuilder result = new TagBuilder("a");
            result.Attributes.Add("href", "#");
            result.AddCssClass("more");
            result.AddCssClass("link");
            result.AddCssClass("invoke");
            result.AddCssClass($"bp-{this.Breakpoint.ToLower()}");
            result.Attributes.Add("title", "Les mer");
            result.InnerHtml.SetHtmlContent("Les mer");

            return result;
        }

        /// <summary>
        /// Text that overrides pre Content
        /// </summary>
        [HtmlAttributeName("pre")]
        public string Pre { get; set; } = default;

        /// <summary>
        /// Text that overrides main Content
        /// </summary>
        [HtmlAttributeName("main")]
        public string Main { get; set; } = default;

        /// <summary>
        /// Style with specified Color Profile
        /// </summary>
        [HtmlAttributeName("color")]
        public ColorProfiles Color { get; set; } = ColorProfiles.Default;

        /// <summary>
        /// The breakpoint where all text is displayed
        /// </summary>
        [HtmlAttributeName("breakpoint")]
        public MediaBreakpoints Breakpoint { get; set; } = MediaBreakpoints.MD;

        /// <summary>
        /// Border style on main tag
        /// </summary>
        [HtmlAttributeNotBound]
        public ChapterBorderStyles Border { get; set; } = ChapterBorderStyles.None;

        /// <summary>
        /// The type of element that displays Main
        /// </summary>
        [HtmlAttributeNotBound]
        public ChapterLinkStyles LinkStyle { get; set; } = ChapterLinkStyles.Button;
    }

    public sealed class ChapterContext
    {
        public string Pre { get; set; }
        public string Main { get; set; }
    }

    public enum ChapterBorderStyles
    {
        None,
        Solid,
        Glow
    }

    public enum ChapterLinkStyles
    {
        Link,
        Button
    }
}
