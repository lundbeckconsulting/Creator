/*
    @Date			: 10.12.2019
    @Author         : Stein Lundbeck
*/

using LC.Assets;
using LC.Assets.Core.Components.TagHelpers;
using LC.Assets.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    [HtmlTargetElement("content", Attributes = "type", ParentTag = "chapter", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ChapterContentTagHelper : TagHelperBase
    {
        public ChapterContentTagHelper(IWebHostEnvironment environment, IAssetsDBContextAccessor db, IAssetsConfigWrapper config, IHtmlHelper html) : base(environment, db, config, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, output);

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

            await base.ProcessAsync(context, output);
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