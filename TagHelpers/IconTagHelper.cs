﻿using LC.Assets.Components.Extensions;
using LC.Creator.Code;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("icon", Attributes = "type", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IconTagHelper : TagHelperBase
    {
        public IconTagHelper(IHostingEnvironment env, IHtmlHelper html) : base(env, html)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, ref output);

            TagBuilder wrap = default;
            TagBuilder tag = GetTagBuilder(context, "span");
            tag.AddCssClass("wrap");
            tag.InnerHtml.AppendHtml(GetSymbol());

            if (!this.NoColor)
            {
                tag.AddCssClass(GetColorProfile());
            }

            if (this.FixedWidth)
            {
                tag.AddCssClass("fa-fw");
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
            wrap.AddCssClass("icon");

            if (this.Clickable)
            {
                wrap.AddCssClass("clickable");
            }

            if (!this.NoText)
            {
                TagBuilder text = new TagBuilder("span");
                text.AddCssClass("label");

                if (!this.Size.Equals(SymbolSizes.None))
                {
                    text.AddCssClass(this.Size.ToString().ToLower());
                }

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

                case Symbols.SaveFull:
                    pre = "fas";
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

                case Symbols.EditFull:
                    pre = "fas";
                    name = "edit";
                    break;

                case Symbols.Copy:
                    name = "copy";
                    break;

                case Symbols.CopyFull:
                    pre = "fas";
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

                case Symbols.CalendarFull:
                    pre = "fas";
                    name = "calendar-alt";
                    break;

                case Symbols.Building:
                    name = "building";
                    break;

                case Symbols.BuildingFull:
                    pre = "fas";
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
                    name = "phone";
                    break;

                case Symbols.PhoneFull:
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

                case Symbols.Comment:
                    name = "comment";
                    break;

                case Symbols.CommentFull:
                    pre = "fas";
                    name = "comment";
                    break;

                case Symbols.Comments:
                    name = "comments";
                    break;

                case Symbols.CommentsFull:
                    pre = "fas";
                    name = "comments";
                    break;

                case Symbols.Like:
                    name = "thumbs-up";
                    break;

                case Symbols.LikeFull:
                    pre = "fas";
                    name = "thumbs-up";
                    break;

                case Symbols.Home:
                    pre = "fas";
                    name = "home";
                    break;

                case Symbols.Bars:
                    pre = "fas";
                    name = "bars";
                    break;

                case Symbols.GitHub:
                    pre = "fab";
                    name = "github";
                    break;

                case Symbols.Globe:
                    pre = "fas";
                    name = "globe";
                    break;

                case Symbols.AddressBook:
                    name = "address-book";
                    break;

                case Symbols.AddressBookFull:
                    pre = "fas";
                    name = "address-book";
                    break;

                case Symbols.Folder:
                    name = "folder";
                    break;

                case Symbols.FolderFull:
                    pre = "fas";
                    name = "folder-open";
                    break;

                case Symbols.FolderMinusFull:
                    pre = "fas";
                    name = "folder-minus";
                    break;

                case Symbols.FolderPlusFull:
                    pre = "fas";
                    name = "folder-plus";
                    break;

                case Symbols.FolderOpen:
                    name = "folder-open";
                    break;

                case Symbols.FolderOpenFull:
                    pre = "fas";
                    name = "folder-open";
                    break;

                case Symbols.UserCircle:
                    name = "user-circle";
                    break;

                case Symbols.UserCircleFull:
                    pre = "fas";
                    name = "user-circle";
                    break;

                case Symbols.UserNinja:
                    pre = "fas";
                    name = "user-ninja";
                    break;

                case Symbols.UserSpy:
                    pre = "fas";
                    name = "user-secret";
                    break;

                case Symbols.UserTie:
                    pre = "fas";
                    name = "user-tie";
                    break;

                case Symbols.User:
                    name = "user";
                    break;

                case Symbols.UserFull:
                    pre = "fas";
                    name = "user";
                    break;

                case Symbols.Code:
                    pre = "fas";
                    name = "code";
                    break;

                case Symbols.CodeLaptop:
                    pre = "fas";
                    name = "laptop-code";
                    break;

                case Symbols.Diagram:
                    pre = "fas";
                    name = "project-diagram";
                    break;

                case Symbols.Add:
                    name = "plus-square";
                    break;

                case Symbols.AddFull:
                    pre = "fas";
                    name = "plus-square";
                    break;
            }

            switch (this.Size)
            {
                case SymbolSizes.XXS:
                    result.AddCssClass("fa-xs");
                    break;

                case SymbolSizes.XS:
                    result.AddCssClass("fa-sm");
                    break;

                case SymbolSizes.SM:
                    result.AddCssClass("fa-lg");
                    break;

                case SymbolSizes.MD:
                    result.AddCssClass("fa-2x");
                    break;

                case SymbolSizes.LG:
                    result.AddCssClass("fa-5x");
                    break;

                case SymbolSizes.XL:
                    result.AddCssClass("fa-7x");
                    break;

                case SymbolSizes.XXL:
                    result.AddCssClass("fa-10x");
                    break;
            }

            if (!this.Pre.IsNull())
            {
                pre = this.Pre;
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
                    case Symbols.SaveFull:
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
                    case Symbols.EditFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Rediger" : "Edit";
                        break;

                    case Symbols.Copy:
                    case Symbols.CopyFull:
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

                    case Symbols.Comment:
                    case Symbols.CommentFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Kommenter" : "Comment";
                        break;

                    case Symbols.Comments:
                    case Symbols.CommentsFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Kommentarer" : "Comments";
                        break;

                    case Symbols.Like:
                    case Symbols.LikeFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Lik" : "Like";
                        break;

                    case Symbols.Home:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Hjem" : "Home";
                        break;

                    case Symbols.GitHub:
                        result = "GitHub";
                        break;

                    case Symbols.Folder:
                    case Symbols.FolderFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Mappe" : "Folder";
                        break;

                    case Symbols.Add:
                    case Symbols.AddFull:
                        result = lang.Equals(SymbolLanguages.Norwegian) ? "Legg til" : "Add";
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
                case Symbols.SaveFull:
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
                case Symbols.EditFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Rediger" : "Edit";
                    break;

                case Symbols.Copy:
                case Symbols.CopyFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Kopier" : "Copy";
                    break;

                case Symbols.AddressCard:
                case Symbols.AddressCardFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Adresse" : "Address";
                    break;

                case Symbols.Calendar:
                case Symbols.CalendarFull:
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
                case Symbols.PhoneFull:
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

                case Symbols.Comment:
                case Symbols.CommentFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Kommenter" : "Comment";
                    break;

                case Symbols.Comments:
                case Symbols.CommentsFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Kommentarer" : "Comments";
                    break;

                case Symbols.Like:
                case Symbols.LikeFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Lik" : "Like";
                    break;

                case Symbols.Home:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Hjem" : "Home";
                    break;

                case Symbols.GitHub:
                    result = "GitHub";
                    break;

                case Symbols.Folder:
                case Symbols.FolderFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Mappe" : "Folder";
                    break;

                case Symbols.Add:
                case Symbols.AddFull:
                    result = lang.Equals(SymbolLanguages.Norwegian) ? "Legg til" : "Add";
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

        /// <summary>
        /// Output format. Default is Span
        /// </summary>
        [HtmlAttributeName("out")]
        public SymbolOutputs Output { get; set; } = SymbolOutputs.Span;

        [HtmlAttributeName("size")]
        public SymbolSizes Size { get; set; } = SymbolSizes.SM;

        [HtmlAttributeName("language")]
        public SymbolLanguages Language { get; set; } = SymbolLanguages.Auto;

        [HtmlAttributeName("text")]
        public string Text { get; set; }

        [HtmlAttributeName("href")]
        public string Href { get; set; } = "#";

        /// <summary>
        /// Cursor turns to pointer when mouseover icon
        /// </summary>
        [HtmlAttributeName("clickable")]
        public bool Clickable { get; set; } = true;

        [HtmlAttributeName("no-text")]
        public bool NoText { get; set; } = true;

        [HtmlAttributeName("no-color")]
        public bool NoColor { get; set; } = false;

        [HtmlAttributeName("color")]
        public ColorProfiles Color { get; set; } = ColorProfiles.Default;

        [HtmlAttributeName("pre")]
        public string Pre { get; set; } = default;

        [HtmlAttributeName("fixed-width")]
        public bool FixedWidth { get; set; } = false;
    }

    public enum Symbols
    {
        Save,
        SaveFull,
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
        EditFull,
        Copy,
        CopyFull,
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
        CalendarFull,
        Building,
        BuildingFull,
        AddressCard,
        AddressCardFull,
        Envelope,
        EnvelopeFull,
        EnvelopeOpen,
        EnvelopeOpenFull,
        Phone,
        PhoneFull,
        Mobile,
        MobileFull,
        Facebook,
        Messenger,
        LaptopCode,
        Comment,
        CommentFull,
        Comments,
        CommentsFull,
        Like,
        LikeFull,
        Home,
        Bars,
        GitHub,
        Globe,
        AddressBook,
        AddressBookFull,
        Folder,
        FolderFull,
        FolderMinusFull,
        FolderPlusFull,
        FolderOpen,
        FolderOpenFull,
        UserCircle,
        UserCircleFull,
        UserNinja,
        UserSpy,
        UserTie,
        User,
        UserFull,
        Code,
        CodeLaptop,
        Diagram,
        Add,
        AddFull
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
        None,
        XXS,
        XS,
        SM,
        MD,
        LG,
        XL,
        XXL
    }

    public enum SymbolLanguages
    {
        Auto,
        Norwegian,
        English
    }
}