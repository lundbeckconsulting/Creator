/*
    @Date			: 10.12.2019
    @Author         : Stein Lundbeck
*/

using LC.Assets;
using LC.Assets.Components.Extensions;
using LC.Assets.Core;
using LC.Assets.Core.Components.TagHelpers;
using LC.Assets.Components.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    [HtmlTargetElement("dialog", Attributes = "size", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DialogTagHelper : TagHelperBase
    {
        private const string _backgroundID = "dialogBackground";
        private const string _viewDataBackgroundKey = "dialogBackgroundExists";
        private const string _backgroundCssName = "dialog-background";

        public DialogTagHelper(IWebHostEnvironment environment, IAssetsDBContextAccessor db, IAssetsConfigAccessor config, IHtmlHelper html) : base(environment, db, config, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcess(context, output);
            
            output.Content.AppendHtml(GetTag());
            AddBackgroundTag();

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

            void AddBackgroundTag()
            {
                bool exists = this.HtmlHelper.ViewData.ContainsKey(_viewDataBackgroundKey) ? this.HtmlHelper.ViewData.GetValue<bool>(_viewDataBackgroundKey) : false;
                TagBuilder result = new TagBuilder("div");
                result.Attributes.Add("id", _backgroundID);
                result.AddCssClass(_backgroundCssName);

                if (!exists)
                {
                    output.PostContent.AppendHtml(result);

                    this.HtmlHelper.ViewData.Add(_viewDataBackgroundKey, true);
                }
            }
        }

        [HtmlAttributeName("color")]
        public CreatorColorProfiles Color { get; set; } = CreatorColorProfiles.Default;

        [HtmlAttributeName("size")]
        public ModalSizes Size { get; set; }

        [HtmlAttributeName("title")]
        public string Header { get; set; } = default;

        [HtmlAttributeName("show")]
        public bool Show { get; set; } = false;
    }
}