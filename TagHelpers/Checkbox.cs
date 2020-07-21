/*
    @Date			              : 10.07.2020
    @Author                       : Stein Lundbeck
*/

using Creator.Components;
using LundbeckConsulting.Components.Core;
using LundbeckConsulting.Components.Core.Repos;
using LundbeckConsulting.Components.Core.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    public interface ICheckBoxTagHelper : ITagHelperCustom
    {
        /// <summary>
        /// Color of checkbox
        /// </summary>
        CreatorColorProfiles Color { get; set; }

        /// <summary>
        /// Indicates if the checkbox should be checked
        /// </summary>
        bool Checked { get; set; }

        /// <summary>
        /// Text of checkbox
        /// </summary>
        string Text { get; set; }
    }

    /// <summary>
    /// Custom checkbox implementing color profiles from Creator
    /// </summary>
    [HtmlTargetElement("check", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CheckboxTagHelper : TagHelperCustom, ICheckBoxTagHelper
    {
        public CheckboxTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        {

        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcess(context, output);

            ITagBuilderCustom outer = new TagBuilderCustom("div");
            outer.AddCssClassRange(new string[] { "creator", "checkbox-wrap", $"cp-{this.Color.ToLower()}" });

            ITagBuilderCustom label = new TagBuilderCustom("label");
            label.AddCssClass("container");

            ITagBuilderCustom input = new TagBuilderCustom("input", ContentPositions.PostElement, TagRenderMode.SelfClosing, true, new string[] { "id", "name" }, false, true);
            input.AddAttribute("type", "checkbox", false);
            input.AddCssClass("chk");

            if (this.Checked)
            {
                input.AddAttribute("checked", "checked", false);
            }

            ITagBuilderCustom span = new TagBuilderCustom("span");
            span.AddCssClass("checkmark");

            label.AddChildRange(input, span);
            outer.AddChild(label);

            if (!this.Text.Null())
            {
                ITagBuilderCustom txt = new TagBuilderCustom("span");
                txt.AddCssClass("text");
                txt.InnerHtml.Append(this.Text);

                outer.AddChild(txt);
            }

            AddContent(outer);

            await base.ProcessCustom();
        }

        [HtmlAttributeName("color")]
        public CreatorColorProfiles Color { get; set; } = CreatorColorProfiles.Default;

        [HtmlAttributeName("checked")]
        public bool Checked { get; set; } = false;

        [HtmlAttributeName("text")]
        public string Text { get; set; }
    }
}
