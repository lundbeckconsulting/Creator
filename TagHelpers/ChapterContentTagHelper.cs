using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("content", Attributes = "type", ParentTag = "chapter", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ChapterContentTagHelper : TagHelperBase
    {
        public ChapterContentTagHelper(IHostingEnvironment env, IHtmlHelper html) : base(env, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            ChapterContext cntx = (ChapterContext)context.Items[typeof(ChapterTagHelper)];
            TagHelperContent child = await output.GetChildContentAsync();
            string inner = child.GetContent();

            switch (this.Type)
            {
                case ChapterContentTypes.Pre:
                    cntx.Pre = inner;
                    break;

                case ChapterContentTypes.Main:
                    cntx.Main = inner;
                    break;
            }

            output.SuppressOutput();
        }

        [HtmlAttributeName("type")]
        public ChapterContentTypes Type { get; set; }
    }

    public enum ChapterContentTypes
    {
        Pre,
        Main
    }
}