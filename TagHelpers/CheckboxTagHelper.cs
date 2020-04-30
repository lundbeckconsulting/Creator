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
        public CheckboxTagHelper(IWebHostEnvironment environment, IAssetsDBContextAccessor db, IAssetsConfigAccessor config, IHtmlHelper html) : base(environment, db, config, html)
        {
            
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcess(context, output);         

            TagBuilderCustom outer = new TagBuilderCustom("div");
            outer.AddCssClassRange(new string[] { "creator", "checkbox-wrap", $"cp-{this.Color.ToLower()}" });

            TagBuilderCustom label = new TagBuilderCustom("label");
            label.AddCssClass("container");

            TagBuilderCustom input = new TagBuilderCustom("input", TagRenderMode.SelfClosing, true, new string[] { "id", "name" });
            input.Attributes.Add("type", "checkbox");
            input.AddCssClass("chk");

            if (this.Checked)
            {
                input.Attributes.Add("checked", "checked");
            }

            TagBuilderCustom span = new TagBuilderCustom("span");
            span.AddCssClass("checkmark");

            label.AddChildRange(input, span);
            outer.AddChild(label);

            if (!this.Text.IsNull())
            {
                TagBuilderCustom txt = new TagBuilderCustom("span");
                txt.AddCssClass("text");
                txt.InnerHtml.Append(this.Text);

                outer.AddChild(txt);
            }

            AddContent(outer);

            await base.ProcessAsync(true, true);
        }

        [HtmlAttributeName("color")]
        public CreatorColorProfiles Color { get; set; } = CreatorColorProfiles.Default;

        [HtmlAttributeName("checked")]
        public bool Checked { get; set; } = false;

        [HtmlAttributeName("text")]
        public string Text { get; set; }
    }
}
