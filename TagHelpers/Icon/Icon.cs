/* 
    @Date           : 25.09.2019
    @Author         : Stein Lundbeck
*/

using Creator.Components;
using LC.Assets.Core;
using LundbeckConsulting.Components.Core;
using LundbeckConsulting.Components.Core.Repos;
using LundbeckConsulting.Components.Core.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Creator.TagHelpers.Icon
{
    /// <summary>
    /// Displays icons from FontAwesome, Friconix, Captain Icon and DevIcons
    /// </summary>
    [HtmlTargetElement("icon", Attributes = "symbol", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("icon", Attributes = "fi-symbol", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("icon", Attributes = "ci-symbol", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("icon", Attributes = "di-symbol", TagStructure = TagStructure.WithoutEndTag)]
    public sealed class IconTagHelper : TagHelperCustom, ITagHelperCustom
    {
        private readonly IIconHelper _iconHelper = new IconHelper();

        public IconTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        {

        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcess(context, output);

            if (this.Symbol != FontAwesomeIcons.None)
            {
                AddContent(GetFontAwesomeTag());
            }
            else if (this.FriconixSymbol != FriconixIcons.None)
            {
                AddContent(GetFriconixTag());
            }
            else if (this.CaptainIconSymbol != CaptainIcons.None)
            {
                AddContent(GetCaptainIconTag());
            }
            else if (this.DevIconSymbol != DevIcons.None)
            {
                AddContent(GetDevIconTag());
            }
            else
            {
                throw new ArgumentException("No symbol property is set");
            }

            await base.ProcessCustom();
        }

        private ITagBuilderCustom GetDevIconTag()
        {
            if (this.DevIconSymbol == DevIcons.None)
            {
                throw new ArgumentException("DevIcon icon not set");
            }

            IIconRecord icon = _iconHelper.GetIcon(this.DevIconSymbol);
            ITagBuilderCustom tag = new TagBuilderCustom("i", TagRenderMode.Normal, false);
            tag.AddCssClassRange("di-icon", $"devicon-{icon.Name}", $"size-{this.Size}");

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetFontAwesomeTag()
        {
            IIconRecord icon = _iconHelper.GetIcon(this.Symbol);

            if (this.Symbol == FontAwesomeIcons.None)
            {
                throw new ArgumentException("FontAwesome icon not set");
            }

            ITagBuilderCustom tag = new TagBuilderCustom("i", TagRenderMode.Normal, false);
            tag.AddCssClassRange("fa-icon", icon.FontAwesomePrefix, $"fa-{icon.Name.ToLower()}", _iconHelper.GetSize(this.Size).FontAwesomeSize);

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetFriconixTag()
        {
            IIconRecord icon = _iconHelper.GetIcon(this.FriconixSymbol);

            if (this.FriconixSymbol == FriconixIcons.None)
            {
                throw new ArgumentException("Friconix icon not set");
            }

            ITagBuilderCustom tag = new TagBuilderCustom("i", TagRenderMode.Normal, false);
            tag.AddCssClass("fi-icon");
            tag.AddCssClass($"fi-{GetShape(this.Shape)}{GetThickness(this.Thickness)}{GetStyle(this.FriconixStyle)}{GetDirection(this.Direction)}{GetEffect(this.Effect)}{_iconHelper.GetSize(this.Size).FriconixSize}-{icon.Name.ToLower()}");

            #region Misc Friconix functions
            char GetShape(FriconixShapes shape)
            {
                char result = 'x';

                switch (shape)
                {
                    case FriconixShapes.Triangle:
                        result = 't';
                        break;

                    case FriconixShapes.Equilateral:
                        result = 'e';
                        break;

                    case FriconixShapes.Circle:
                        result = 'c';
                        break;

                    case FriconixShapes.Square:
                        result = 's';
                        break;

                    case FriconixShapes.Hexagon:
                        result = 'h';
                        break;

                    case FriconixShapes.Octagon:
                        result = 'o';
                        break;
                }

                return result;
            }

            char GetThickness(FriconixThickness thickness)
            {
                char result = 'x';

                switch (thickness)
                {
                    case FriconixThickness.Thin:
                        result = 't';
                        break;

                    case FriconixThickness.Normal:
                        result = 'n';
                        break;

                    case FriconixThickness.Wide:
                        result = 'w';
                        break;
                }

                return result;
            }

            char GetStyle(FriconixStyle style)
            {
                char result = 'x';

                switch (style)
                {
                    case FriconixStyle.Line:
                        result = 'l';
                        break;

                    case FriconixStyle.Solid:
                        result = 's';
                        break;

                    case FriconixStyle.Prohibited:
                        result = 'p';
                        break;
                }

                return result;
            }

            char GetDirection(FriconixDirection direction)
            {
                char result = 'x';

                switch (direction)
                {
                    case FriconixDirection.Up:
                        result = 'u';
                        break;

                    case FriconixDirection.Down:
                        result = 'd';
                        break;

                    case FriconixDirection.Left:
                        result = 'l';
                        break;

                    case FriconixDirection.Right:
                        result = 'r';
                        break;
                }

                return result;
            }

            char GetEffect(FriconixEffect effect)
            {
                char result = 'x';

                switch (effect)
                {
                    case FriconixEffect.Horizontal:
                        result = 'h';
                        break;

                    case FriconixEffect.Vertical:
                        result = 'v';
                        break;

                    case FriconixEffect.Spin:
                        result = 's';
                        break;

                    case FriconixEffect.Pulse:
                        result = 'p';
                        break;
                }

                return result;
            }
            #endregion

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetCaptainIconTag()
        {
            IIconRecord icon = _iconHelper.GetIcon(this.CaptainIconSymbol);

            if (this.CaptainIconSymbol == CaptainIcons.None)
            {
                throw new ArgumentException("Captain Icon not set");
            }

            ITagBuilderCustom tag = new TagBuilderCustom("i");
            tag.AddCssClass("ci-icon");
            tag.AddCssClass($"ci-icon-{_iconHelper.GetCaptainIconName(icon.CaptainIcon)}");
            tag.AddAttribute("style", $"font-size: {_iconHelper.GetSize(this.Size).CaptainIconSize};", false);

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetWrapTag(ITagBuilderCustom iconTag)
        {
            ITagBuilderCustom result = new TagBuilderCustom(_iconHelper.GetFormatTagName(this.Format), false);
            result.AddCssClass("icon-wrap");
            result.AddCssClass("flex-center-center");
            result.AddCssClass(this.GetColorProfile());
            result.ConsumeAttributes = true;
            result.AddChild(iconTag);

            if (Symbol != FontAwesomeIcons.None)
            {
                result.AddCssClass("fa-icon");
            }
            else if (FriconixSymbol != FriconixIcons.None)
            {
                result.AddCssClass("fi-icon");
            }
            else if (CaptainIconSymbol != CaptainIcons.None)
            {
                result.AddCssClass("ci-icon");
            }
            else if (DevIconSymbol != DevIcons.None)
            {
                result.AddCssClass("di-icon");
            }

            if (this.Format == IconOutputFormats.Anchor)
            {
                result = _iconHelper.ProcessAnchorTag(result, this.Href, this.Target);
            }

            if (!this.Text.Null())
            {
                ITagBuilderCustom txt = new TagBuilderCustom("span");
                txt.AddCssClass("icon-text");
                txt.AddCssClass("mrg-sm-left");
                txt.InnerHtml.SetHtmlContent(this.Text);
                result.InnerHtml.AppendHtml(txt);
            }

            return result;
        }

        private string GetColorProfile()
        {
            string color = this.ColorProfile.ToString().ToLower();

            switch (this.Symbol)
            {
                case FontAwesomeIcons.Close:
                case FontAwesomeIcons.CloseFull:
                case FontAwesomeIcons.CloseCircle:
                case FontAwesomeIcons.CloseCircleFull:
                    color = CreatorColorProfiles.Danger.ToString();
                    break;
            }

            return "cp-" + color.ToLower();
        }

        #region Properties
        /// <summary>
        /// Name of the icon to use with Type equal FontAwesome
        /// </summary>
        [HtmlAttributeName("symbol")]
        public FontAwesomeIcons Symbol { get; set; } = FontAwesomeIcons.None;

        /// <summary>
        /// Name of icon in the Friconix set of icons. Overrides FontAwesome if set
        /// </summary>
        [HtmlAttributeName("fi-symbol")]
        public FriconixIcons FriconixSymbol { get; set; } = FriconixIcons.None;

        /// <summary>
        /// Name of icon in the Captain Icon set of icons. Overrides FontAwesome and Friconix if set
        /// </summary>
        [HtmlAttributeName("ci-symbol")]
        public CaptainIcons CaptainIconSymbol { get; set; } = CaptainIcons.None;

        [HtmlAttributeName("di-symbol")]
        public DevIcons DevIconSymbol { get; set; } = DevIcons.None;

        /// <summary>
        /// Text to appear to the right of the icon
        /// </summary>
        [HtmlAttributeName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Output format of Font Awesome icon
        /// </summary>
        [HtmlAttributeName("format")]
        public IconOutputFormats Format { get; set; } = IconOutputFormats.Italic;

        /// <summary>
        /// The method of button if Symbol Output is button
        /// </summary>
        [HtmlAttributeName("form-method")]
        public TagFormMethods FormMethod { get; set; } = TagFormMethods.Post;

        /// <summary>
        /// The color profile to apply the symbol
        /// </summary>
        [HtmlAttributeName("color")]
        public CreatorColorProfiles ColorProfile { get; set; } = CreatorColorProfiles.Default;

        /// <summary>
        /// Type of button if format is button
        /// </summary>
        [HtmlAttributeName("button")]
        public TagButtonTypes ButtonType { get; set; } = TagButtonTypes.Submit;

        /// <summary>
        /// Anchor href value if format is anchor
        /// </summary>
        [HtmlAttributeName("href")]
        public string Href { get; set; } = "#";

        /// <summary>
        /// Anchor target value if format er anchor
        /// </summary>
        [HtmlAttributeName("target")]
        public TagAnchorTargets Target { get; set; } = TagAnchorTargets.None;

        /// <summary>
        /// The type of icon set
        /// </summary>
        [HtmlAttributeName("type")]
        public IconSets Type { get; set; } = IconSets.FontAwesome;

        /// <summary>
        /// The shape of a Friconix icon
        /// </summary>
        [HtmlAttributeName("shape")]
        public FriconixShapes Shape { get; set; } = FriconixShapes.None;

        /// <summary>
        /// The direction of a Friconix icon
        /// </summary>
        [HtmlAttributeName("direction")]
        public FriconixDirection Direction { get; set; } = FriconixDirection.None;

        /// <summary>
        /// The style of a Friconix icon
        /// </summary>
        [HtmlAttributeName("friconix-style")]
        public FriconixStyle FriconixStyle { get; set; } = FriconixStyle.None;

        /// <summary>
        /// The thickness of a Friconix icon
        /// </summary>
        [HtmlAttributeName("thickness")]
        public FriconixThickness Thickness { get; set; } = FriconixThickness.None;

        /// <summary>
        /// The effect of a Friconix icon
        /// </summary>
        [HtmlAttributeName("effect")]
        public FriconixEffect Effect { get; set; } = FriconixEffect.None;

        /// <summary>
        /// Icon size
        /// </summary>
        [HtmlAttributeName("size")]
        public IconSizes Size { get; set; } = IconSizes.Normal;
        #endregion
    }
}
