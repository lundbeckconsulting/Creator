using LC.Assets.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("fa", Attributes = "name", TagStructure = TagStructure.WithoutEndTag)]
    public class FontAwesomeTagHelper : TagHelperBase
    {
        public FontAwesomeTagHelper(IHostingEnvironment env, IHtmlHelper html) : base(env, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, ref output);

            string content = this.Prefix + " " + GetName() + GetSize() + GetAnimatedType() + GetFixedWidth() + GetListIcon();

            output.Attributes.SetAttribute("aria-hidden", "true");
            output.Attributes.Add("class", content.TrimEnd(new char[] { ' ' }));

            await base.ProcessAsync(context, output);
        }

        private string GetName()
        {
            return "fa-" + this.Name;
        }

        private string GetSize()
        {
            string result = string.Empty;

            switch (this.Size)
            {
                case Sizes.ExtraSmall:
                    result = "xs";
                    break;

                case Sizes.Small:
                    result = "sm";
                    break;

                case Sizes.Large:
                    result = "lg";
                    break;

                case Sizes.x2:
                    result = "2x";
                    break;

                case Sizes.x3:
                    result = "3x";
                    break;

                case Sizes.x4:
                    result = "4x";
                    break;

                case Sizes.x5:
                    result = "5x";
                    break;

                case Sizes.x6:
                    result = "6x";
                    break;

                case Sizes.x7:
                    result = "7x";
                    break;

                case Sizes.x8:
                    result = "8x";
                    break;

                case Sizes.x9:
                    result = "9x";
                    break;

                case Sizes.x10:
                    result = "10x";
                    break;
            }

            if (!result.IsNull())
            {
                result = " " + result;
            }

            return result;
        }

        private string GetAnimatedType()
        {
            string result = string.Empty;

            switch (this.AnimatedType)
            {
                case AnimatedTypes.Spin:
                    result = "fa-spin";
                    break;

                case AnimatedTypes.Pulse:
                    result = "fa-pulse";
                    break;
            }

            if (!result.IsNull())
            {
                result = " " + result;
            }

            return result;
        }

        private string GetFixedWidth()
        {
            string result = string.Empty;

            if (this.FixedWidth)
            {
                result = " fa-fw";
            }

            return result;
        }

        private string GetListIcon()
        {
            string result = string.Empty;

            if (this.ListIcon)
            {
                result = " fa-li";
            }

            return result;
        }

        /// <summary>
        /// Font Awesome prefix. Default is "fas"
        /// </summary>
        [HtmlAttributeName("prefix")]
        public string Prefix { get; set; } = "fas";

        /// <summary>
        /// Name of the icon
        /// </summary>
        [HtmlAttributeName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Predefined icon size
        /// </summary>
        [HtmlAttributeName("size")]
        public Sizes Size { get; set; } = Sizes.None;

        /// <summary>
        /// Is Fixed Width
        /// </summary>
        [HtmlAttributeName("fixed-width")]
        public bool FixedWidth { get; set; } = false;

        /// <summary>
        /// Is List Icon
        /// </summary>
        [HtmlAttributeName("list-icon")]
        public bool ListIcon { get; set; } = false;

        /// <summary>
        /// The Animated Type
        /// </summary>
        [HtmlAttributeName("animated-type")]
        public AnimatedTypes AnimatedType { get; set; } = AnimatedTypes.None;

        public enum Sizes
        {
            None,
            ExtraSmall,
            Small,
            Large,
            x2,
            x3,
            x4,
            x5,
            x6,
            x7,
            x8,
            x9,
            x10
        }

        public enum AnimatedTypes
        {
            None,
            Spin,
            Pulse
        }
    }
}
