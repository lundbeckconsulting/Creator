using LC.Assets.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static Creator.TagHelpers.ListTagHelper;

namespace Creator.TagHelpers.Models
{
    public interface IListTagHelperModel
    {
        /// <summary>
        /// List item text
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The type of attribute of an item in an ordered list. Defualt if None
        /// </summary>
        OrderedListItemTypes ItemType { get; set; }

        /// <summary>
        /// Wraps item in an anchor tag if set
        /// </summary>
        string Href { get; set; }

        /// <summary>
        /// Target attribute value if Href is set
        /// </summary>
        HrefTargets Target { get; set; } 

        /// <summary>
        /// Class attribute value of Class tag
        /// </summary>
        string Class { get; set; }
    }

    public class ListTagHelperModel : IListTagHelperModel
    {
        public ListTagHelperModel(string text) : this(text, OrderedListItemTypes.None) { }

        public ListTagHelperModel(string text, OrderedListItemTypes itemType) : this(text, itemType, default) { }

        public ListTagHelperModel(string text, OrderedListItemTypes itemType, string href, HrefTargets target = HrefTargets.None)
        {
            this.Text = text;
            this.ItemType = itemType;
            this.Href = href;
            this.Target = target;
        }

        public string Text { get; set; }
        public OrderedListItemTypes ItemType { get; set; } = OrderedListItemTypes.None;
        public string Href { get; set; }
        public HrefTargets Target { get; set; } = HrefTargets.None;
        public string Class { get; set; }
    }
}
