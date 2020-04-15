using LC.Assets;
using LC.Assets.Components.Data;
using LC.Assets.Components.Extensions;
using LC.Assets.Core;
using LC.Assets.Core.Components.TagHelpers;
using LC.Assets.Core.Components.TagHelpers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    [HtmlTargetElement("check", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CheckboxTagHelper : TagHelperBase
    {
        public CheckboxTagHelper(IWebHostEnvironment environment, IAssetsDBContextAccessor db, IAssetsConfigWrapper config, IHtmlHelper html) : base(environment, db, config, html)
        {
            
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, output);         

            TagBuilder wrap = new TagBuilder("div");
            wrap.AddCssClass("creator");
            wrap.AddCssClass("checkbox-wrap");
            wrap.AddCssClass($"cp-{this.Color.ToLower()}");

            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("container");

            TagBuilder input = new TagBuilder("input");
            input.TagRenderMode = TagRenderMode.SelfClosing;
            input.Attributes.Add("type", "checkbox");
            input.AddCssClass("chk");

            if (!this.IdName.IsNull())
            {
                input.Attributes.Add("id", this.IdName);
                input.Attributes.Add("name", this.IdName);
            }
            else
            {
                if (!this.Id_.IsNull())
                {
                    input.Attributes.Add("id", this.Id_);
                }

                if (!this.Name.IsNull())
                {
                    input.Attributes.Add("name", this.Name);
                }
            }

            if (this.Checked)
            {
                input.Attributes.Add("checked", "checked");
            }

            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("checkmark");

            label.InnerHtml.AppendHtml(input);
            label.InnerHtml.AppendHtml(span);

            wrap.InnerHtml.AppendHtml(label);

            if (!this.Text.IsNull())
            {
                TagBuilder txt = new TagBuilder("span");
                txt.AddCssClass("text");
                txt.InnerHtml.Append(this.Text);

                wrap.InnerHtml.AppendHtml(txt);
            }

            AddContent(wrap);

            await base.ProcessAsync();
        }

        [HtmlAttributeName("color")]
        public CreatorColorProfiles Color { get; set; } = CreatorColorProfiles.Default;

        [HtmlAttributeName("checked")]
        public bool Checked { get; set; } = false;

        [HtmlAttributeName("text")]
        public string Text { get; set; }
    }
}
