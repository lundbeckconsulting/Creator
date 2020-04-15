using Creator.TagHelpers.Models;
using LC.Assets;
using LC.Assets.Components.Extensions;
using LC.Assets.Core;
using LC.Assets.Core.Components.TagHelpers;
using LC.Assets.Components.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    [HtmlTargetElement("list", Attributes = "items", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ListTagHelper : TagHelperBase
    {
        public ListTagHelper(IWebHostEnvironment environment, IAssetsDBContextAccessor db, IAssetsConfigWrapper config, IHtmlHelper html) : base(environment, db, config, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, output);

            TagBuilder tag = new TagBuilder(this.Type.Equals(ListTypes.Ordered) ? "ol" : "ul");
            
            if (!this.Class.IsNone())
            {
                tag.AddCssClass(this.Class);
            }

            foreach(TagBuilder item in GetItems())
            {
                tag.InnerHtml.AppendLine(item);
            }

            output.SuppressOutput();
            output.PostElement.AppendLine(tag);

            await base.ProcessAsync(context, output);
        }

        private IEnumerable<TagBuilder> GetItems()
        {
            ICollection<TagBuilder> result = new Collection<TagBuilder>();

            foreach(IListTagHelperModel item in this.Items)
            {
                TagBuilder tag = new TagBuilder("li");
                

                if (!item.ItemType.Equals(OrderedListItemTypes.None))
                {
                    tag.Attributes.Add("type", item.ItemType.ToString());
                }

                if (!item.Href.IsNone())
                {
                    TagBuilder a = new TagBuilder("a");
                    a.InnerHtml.SetContent(item.Text);
                    a.Attributes.Add("href", item.Href);

                    if (!item.Class.IsNone())
                    {
                        a.AddCssClass(item.Class);
                    }

                    if (!item.Target.Equal(HrefTargets.None))
                    {
                        a.Attributes.Add("target", item.Target.ToLower());
                    }

                    tag.InnerHtml.AppendLine(a);
                }
                else
                {
                    tag.InnerHtml.SetContent(item.Text);
                }

                result.Add(tag);
            }

            return result.AsEnumerable();
        }

        /// <summary>
        /// The type of list to generate
        /// </summary>
        [HtmlAttributeName("type")]
        public ListTypes Type { get; set; } = ListTypes.Ordered;

        /// <summary>
        /// Collection of list items
        /// </summary>
        [HtmlAttributeName("items")]
        public IEnumerable<IListTagHelperModel> Items { get; set; } = new Stack<IListTagHelperModel>();

        /// <summary>
        /// Suppoerted list types
        /// </summary>
        public enum ListTypes
        {
            Ordered,
            Unordered
        }

        /// <summary>
        /// Supported type attribute types of items in an ordered list
        /// </summary>
        public enum OrderedListItemTypes
        {
            None,
            One,
            A,
            a,
            I,
            i
        }
    }
}
