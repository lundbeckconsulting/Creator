using LC.Assets.Components.Extensions;
using LC.Creator.Code;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("c:symbol", Attributes = "type", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SymbolTagHelper : TagHelperBase
    {
        public SymbolTagHelper(IHostingEnvironment env, IHtmlHelper html) : base(env, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, output);

            TagBuilder wrap = default;
            TagBuilder tag = new TagBuilder("span");
            tag.AddCssClass("wrap");
            tag.InnerHtml.AppendHtml(GetSymbol());

            if (!this.NoColor)
            {
                tag.AddCssClass(GetColorProfile());
            }

            if (this.Output.Equals(SymbolOutputs.Button))
            {
                wrap = GetTagBuilder(context, "button");
            }
            else if (this.Output.Equals(SymbolOutputs.Href))
            {
                wrap = GetTagBuilder(context, "a");
                wrap.Attributes.Add("href", this.Href);
            }
            else if (this.Output.Equals(SymbolOutputs.Div))
            {
                wrap = GetTagBuilder(context, "div");
            }
            else if (this.Output.Equals(SymbolOutputs.Span))
            {
                wrap = GetTagBuilder(context, "span");
            }

            wrap.AddCssClass("creator");
            wrap.AddCssClass("symbol");

            if (this.Clickable)
            {
                wrap.AddCssClass("clickable");
            }

            if (!this.NoText)
            {
                TagBuilder text = new TagBuilder("span");
                text.AddCssClass("label");
                text.AddCssClass(this.Size.ToString().ToLower());
                text.InnerHtml.Append(GetText());

                tag.InnerHtml.AppendHtml(text);
            }

            if (!context.AllAttributes.ContainsName("title"))
            {
                string title = GetTitle();

                if (!title.IsNull())
                {
                    wrap.Attributes.Add("title", title);
                }
            }

            wrap.InnerHtml.AppendHtml(tag);

            output.SuppressOutput();
            output.PostContent.AppendHtml(wrap);

            await base.ProcessAsync(context, output);
        }

        private TagBuilder GetSymbol()
        {
            TagBuilder result = new TagBuilder("i");

            string pre = "far", name = default;

            switch (this.Type)
            {
                case Symbols.Save:
                    name = "save";
                    break;

                case Symbols.Close:
                    name = "window-close";
                    break;

                case Symbols.CloseFull:
                    pre = "fas";
                    name = "window-close";
                    break;

                case Symbols.CloseCircle:
                    name = "times-circle";
                    break;

                case Symbols.CloseCircleFull:
                    pre = "fas";
                    name = "times-circle";
                    break;

                case Symbols.SignIn:
                    pre = "fas";
                    name = "sign-in-alt";
                    break;

                case Symbols.SignOut:
                    pre = "fas";
                    name = "sign-out-alt";
                    break;

                case Symbols.Doc:
                    name = "file-alt";
                    break;

                case Symbols.DocCode:
                    name = "file-code";
                    break;

                case Symbols.DocImage:
                    name = "file-image";
                    break;

                case Symbols.DocPDF:
                    name = "file-pdf";
                    break;

                case Symbols.FileUpload:
                    pre = "fas";
                    name = "file-upload";
                    break;

                case Symbols.Edit:
                    name = "edit";
                    break;

                case Symbols.Copy:
                    name = "copy";
                    break;

                case Symbols.X:
                    pre = "fas";
                    name = "times";
                    break;

                case Symbols.ArrowLeft:
                    pre = "fas";
                    name = "arrow-left";
                    break;

                case Symbols.ArrowRight:
                    pre = "fas";
                    name = "arrow-right";
                    break;

                case Symbols.ArrowUp:
                    pre = "fas";
                    name = "arrow-up";
                    break;

                case Symbols.ArrowDown:
                    pre = "fas";
                    name = "arrow-down";
                    break;

                case Symbols.AngleLeft:
                    pre = "fas";
                    name = "angle-left";
                    break;

                case Symbols.AngleRight:
                    pre = "fas";
                    name = "angle-right";
                    break;

                case Symbols.AngleUp:
                    pre = "fas";
                    name = "angle-up";
                    break;

                case Symbols.AngleDown:
                    pre = "fas";
                    name = "angle-down";
                    break;

                case Symbols.Calendar:
                    name = "calendar-alt";
                    break;

                case Symbols.Building:
                    name = "building";
                    break;

                case Symbols.Envelope:
                    name = "envelope";
                    break;

                case Symbols.EnvelopeFull:
                    pre = "fas";
                    name = "envelope";
                    break;

                case Symbols.EnvelopeOpen:
                    name = "envelope-open";
                    break;

                case Symbols.EnvelopeOpenFull:
                    pre = "fas";
                    name = "envelope-open";
                    break;

                case Symbols.AddressCard:
                    name = "address-card";
                    break;

                case Symbols.AddressCardFull:
                    pre = "fas";
                    name = "address-card";
                    break;

                case Symbols.Phone:
                    pre = "fas";
                    name = "phone-square";
                    break;

                case Symbols.Mobile:
                    pre = "fas";
                    name = "mobile-alt";
                    break;

                case Symbols.MobileFull:
                    pre = "fas";
                    name = "mobile";
                    break;

                case Symbols.Facebook:
                    pre = "fab";
                    name = "facebook-square";
                    break;

                case Symbols.Messenger:
                    pre = "fab";
                    name = "facebook-messenger";
                    break;

                case Symbols.LaptopCode:
                    pre = "fas";
                    name = "laptop-code";
                    break;
            }

            switch (this.Size)
            {
                case SymbolSizes.SM:
                    result.AddCssClass("fa-lg");
                    break;

                case SymbolSizes.MD:
                    result.AddCssClass("fa-3x");
                    break;

                case SymbolSizes.LG:
                    result.AddCssClass("fa-5x");
                    break;

                case SymbolSizes.XL:
                    result.AddCssClass("fa-7x");
                    break;
            }

            if (this.FaPre != default)
            {
                pre = this.FaPre;
            }

            name = "fa-" + name;

            result.AddCssClass("symbol");
            result.AddCssClass(pre);
            result.AddCssClass(name);

            return result;
        }

        private string GetColorProfile()
        {
            string result = this.Color.ToString().ToLower();

            switch (this.Type)
            {
                case Symbols.Close:
                case Symbols.CloseFull:
                case Symbols.CloseCircle:
                case Symbols.CloseCircleFull:
                    result = ColorProfiles.Danger.ToString();
                    break;
            }

            return "cp-" + result.ToLower();
        }

        private string GetText()
        {
            string result = default;
            SymbolLanguages lang = GetLanguage();

            if (this.Text.IsNull())
            {
                switch (this.Type)
                {
                    case Symbols.Save:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Lagre" : "Save";
                        break;

                    case Symbols.Close:
                    case Symbols.CloseFull:
                    case Symbols.CloseCircle:
                    case Symbols.CloseCircleFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Lukk" : "Close";
                        break;

                    case Symbols.SignIn:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Logg inn" : "Sign in";
                        break;

                    case Symbols.SignOut:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Logg ut" : "Sign out";
                        break;

                    case Symbols.FileUpload:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Last opp" : "Upload";
                        break;

                    case Symbols.DocImage:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Se bilde" : "See image";
                        break;

                    case Symbols.Edit:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Rediger" : "Edit";
                        break;

                    case Symbols.Copy:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Kopier" : "Copy";
                        break;

                    case Symbols.AddressCard:
                    case Symbols.AddressCardFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Adresse" : "Address";
                        break;

                    case Symbols.Calendar:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Kalender" : "Calendar";
                        break;

                    case Symbols.Envelope:
                    case Symbols.EnvelopeFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Email" : "Email";
                        break;

                    case Symbols.Phone:
                    case Symbols.Mobile:
                    case Symbols.MobileFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Ring" : "Call";
                        break;

                    case Symbols.Facebook:
                        result = "Facebook";
                        break;

                    case Symbols.Messenger:
                        result = "Messenger";
                        break;

                    case Symbols.DocCode:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Kode" : "Code";
                        break;
                }
            }
            else
            {
                result = this.Text;
            }

            return result;
        }

        private string GetTitle()
        {
            string result = default;
            SymbolLanguages lang = GetLanguage();

            switch (this.Type)
            {
                case Symbols.Save:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Lagre" : "Save";
                    break;

                case Symbols.Close:
                case Symbols.CloseFull:
                case Symbols.CloseCircle:
                case Symbols.CloseCircleFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Lukk" : "Close";
                    break;

                case Symbols.SignIn:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Logg inn" : "Sign in";
                    break;

                case Symbols.SignOut:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Logg ut" : "Sign out";
                    break;

                case Symbols.FileUpload:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Last opp" : "Upload";
                    break;

                case Symbols.DocPDF:
                    result = "PDF";
                    break;

                case Symbols.DocImage:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Se bilde" : "See image";
                    break;

                case Symbols.Edit:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Rediger" : "Edit";
                    break;

                case Symbols.Copy:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Kopier" : "Copy";
                    break;

                case Symbols.AddressCard:
                case Symbols.AddressCardFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Adresse" : "Address";
                    break;

                case Symbols.Calendar:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Kalender" : "Calendar";
                    break;

                case Symbols.Envelope:
                case Symbols.EnvelopeFull:
                    result = "Email";
                    break;

                case Symbols.ArrowLeft:
                case Symbols.AngleLeft:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Forrige" : "Previous";
                    break;

                case Symbols.ArrowRight:
                case Symbols.AngleRight:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Neste" : "Next";
                    break;

                case Symbols.ArrowUp:
                case Symbols.AngleUp:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Opp" : "Up";
                    break;

                case Symbols.ArrowDown:
                case Symbols.AngleDown:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Ned" : "Down";
                    break;

                case Symbols.Phone:
                case Symbols.Mobile:
                case Symbols.MobileFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Ring" : "Call";
                    break;

                case Symbols.Facebook:
                    result = "Facebook";
                    break;

                case Symbols.Messenger:
                    result = "Messenger";
                    break;

                case Symbols.DocCode:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Se kode" : "See code";
                    break;
            }

            return result;
        }

        private SymbolLanguages GetLanguage()
        {
            SymbolLanguages result = SymbolLanguages.English;

            if (this.Language.Equals(SymbolLanguages.Auto))
            {
                if (this.CurrentCulture.Name.Equals("no-NB", "no-NN"))
                {
                    result = SymbolLanguages.Norwegian;
                }
            }
            else
            {
                result = this.Language;
            }

            return result;
        }

        [HtmlAttributeName("type")]
        public Symbols Type { get; set; }

        [HtmlAttributeName("out")]
        public SymbolOutputs Output { get; set; } = SymbolOutputs.Button;

        [HtmlAttributeName("size")]
        public SymbolSizes Size { get; set; } = SymbolSizes.MD;

        [HtmlAttributeName("language")]
        public SymbolLanguages Language { get; set; } = SymbolLanguages.Auto;

        [HtmlAttributeName("text")]
        public string Text { get; set; }

        [HtmlAttributeName("href")]
        public string Href { get; set; } = "#";

        [HtmlAttributeName("clickable")]
        public bool Clickable { get; set; } = true;

        [HtmlAttributeName("no-text")]
        public bool NoText { get; set; } = true;

        [HtmlAttributeName("no-color")]
        public bool NoColor { get; set; } = false;

        [HtmlAttributeName("color")]
        public ColorProfiles Color { get; set; } = ColorProfiles.Default;

        [HtmlAttributeName("fa-pre")]
        public string FaPre { get; set; } = default;
    }

    public enum Symbols
    {
        Save,
        Close,
        CloseFull,
        CloseCircle,
        CloseCircleFull,
        SignIn,
        SignOut,
        Doc,
        DocPDF,
        DocImage,
        DocCode,
        FileUpload,
        Edit,
        Copy,
        X,
        ArrowLeft,
        ArrowRight,
        ArrowUp,
        ArrowDown,
        AngleLeft,
        AngleRight,
        AngleUp,
        AngleDown,
        Calendar,
        Building,
        AddressCard,
        AddressCardFull,
        Envelope,
        EnvelopeFull,
        EnvelopeOpen,
        EnvelopeOpenFull,
        Phone,
        Mobile,
        MobileFull,
        Facebook,
        Messenger,
        LaptopCode
    }

    public enum SymbolOutputs
    {
        Button,
        Href,
        Div,
        Span
    }

    public enum SymbolSizes
    {
        SM,
        MD,
        LG,
        XL
    }

    public enum SymbolLanguages
    {
        Auto,
        Norwegian,
        English
    }
}
