using LC.Assets.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LC.Creator.TagHelpers
{
    [HtmlTargetElement("icon", Attributes = "type", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class IconTagHelper : TagHelperBase
    {
        public IconTagHelper(IHostingEnvironment env, IHtmlHelper html) : base(env, html) { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            base.PreProcess(context, ref output);

            output.SuppressOutput();
            output.PostContent.AppendHtml(GetTag());

            await base.ProcessAsync(context, output);
        }

        public TagBuilder GetTag()
        {
            TagBuilder result = default;
            TagBuilder tag = GetTagBuilder("span");
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

            if (this.Out.Equals(SymbolOutputs.Button))
            {
                result = GetTagBuilder("button");
            }
            else if (this.Out.Equals(SymbolOutputs.Href))
            {
                result = GetTagBuilder("a");
                result.Attributes.Add("href", this.Href);
            }
            else if (this.Out.Equals(SymbolOutputs.Div))
            {
                result = GetTagBuilder("div");
            }
            else if (this.Out.Equals(SymbolOutputs.Span))
            {
                result = GetTagBuilder("span");
            }

            result.AddCssClass("creator");
            result.AddCssClass("icon");

            if (this.Clickable)
            {
                result.AddCssClass("clickable");
            }

            if (!this.NoText)
            {
                string foo = this.Text.IsNull() ? GetCaption() : this.Text;
                TagBuilder text = new TagBuilder("span");
                text.AddCssClass("label");

                if (!this.Size.Equals(SymbolSizes.None))
                {
                    text.AddCssClass(this.Size.ToString().ToLower());
                }

                if (!foo.IsNull())
                {
                    text.InnerHtml.Append(foo);
                }

                tag.InnerHtml.AppendHtml(text);
            }

            if (!this.NoTitle)
            {
                string foo = default;

                if (!this.Title.IsNull())
                {
                    foo = this.Title;
                }
                else if (!this.Context.AllAttributes.ContainsName("title"))
                {
                    foo = GetCaption(CaptionTypes.Title);
                }

                if (!foo.IsNull())
                {
                    result.Attributes.Add("title", foo);
                }
            }

            result.InnerHtml.AppendHtml(tag);

            return result;
        }

        private TagBuilder GetSymbol()
        {
            TagBuilder result = new TagBuilder("i");

            string pre = "far";
            string name = default;

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

                case Symbols.FileDownload:
                    pre = "fas";
                    name = "file-download";
                    break;

                case Symbols.FileExport:
                    pre = "fas";
                    name = "file-export";
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

                case Symbols.HomeDamage:
                    pre = "fas";
                    name = "house-damage";
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

                case Symbols.AddCircle:
                    name = "plus-circle";
                    break;

                case Symbols.AddCircleFull:
                    pre = "fas";
                    name = "plus-circle";
                    break;

                case Symbols.ShoppingBasket:        
                    pre = "fas";
                    name = "shopping-basket";
                    break;

                case Symbols.ShoppingCart:
                    pre = "fas";
                    name = "shopping-cart";
                    break;

                case Symbols.ShoppingCartPlus:
                    pre = "fas";
                    name = "cart-plus";
                    break;

                case Symbols.ShoppingBag:
                    pre = "fas";
                    name = "shopping-bag";
                    break;

                case Symbols.Handshake:
                    name = "handshake";
                    break;

                case Symbols.HandshakeFull:
                    pre = "fas";
                    name = "handshake";
                    break;

                case Symbols.ThumbsUp:
                    name = "thumbs-up";
                    break;

                case Symbols.ThumbsUpFull:
                    pre = "fas";
                    name = "thumbs-up";
                    break;

                case Symbols.ThumbsDown:
                    name = "thumbs-down";
                    break;

                case Symbols.ThumbsDownFull:
                    pre = "fas";
                    name = "thumbs-down";
                    break;

                case Symbols.MoneyCheckout:
                    pre = "fas";
                    name = "money-checkout-alt";
                    break;

                case Symbols.Truck:
                    pre = "fas";
                    name = "truck";
                    break;

                case Symbols.Visa:
                    pre = "fab";
                    name = "cc-visa";
                    break;

                case Symbols.Stripe:
                    pre = "fab";
                    name = "cc-stripe";
                    break;

                case Symbols.PayPal:
                    pre = "fab";
                    name = "cc-paypal";
                    break;

                case Symbols.PayPalIcon:
                    pre = "fab";
                    name = "paypal";
                    break;

                case Symbols.Mastercard:
                    pre = "fab";
                    name = "cc-mastercard";
                    break;

                case Symbols.ApplePay:
                    pre = "fab";
                    name = "cc-apple-pay";
                    break;

                case Symbols.ApplePayIcon:
                    pre = "fab";
                    name = "apple-pay";
                    break;

                case Symbols.AmericanExpress:
                    pre = "fab";
                    name = "cc-amex";
                    break;

                case Symbols.AmazonPay:
                    pre = "fab";
                    name = "cc-amazon-pay";
                    break;

                case Symbols.AmazonPayIcon:
                    pre = "fab";
                    name = "amazon-pay";
                    break;

                case Symbols.GoogleWallet:
                    pre = "fab";
                    name = "google-wallet";
                    break;

                case Symbols.Upload:
                    pre = "fas";
                    name = "upload";
                    break;

                case Symbols.UploadCloud:
                    pre = "fas";
                    name = "cloud-upload-alt";
                    break;

                case Symbols.DownloadCloud:
                    pre = "fas";
                    name = "cloud-download-alt";
                    break;

                case Symbols.Bullhorn:
                    pre = "fas";
                    name = "bullhorn";
                    break;

                case Symbols.Smile:
                    name = "smile";
                    break;

                case Symbols.SmileFull:
                    pre = "fas";
                    name = "smile";
                    break;

                case Symbols.SmileWink:
                    name = "smile-wink";
                    break;

                case Symbols.SmileWinkFull:
                    pre = "fas";
                    name = "smile-wink";
                    break;

                case Symbols.SmileLaugh:
                    name = "grin";
                    break;

                case Symbols.SmileLaughFull:
                    pre = "fas";
                    name = "grin";
                    break;

                case Symbols.UserPlusFull:
                    pre = "fas";
                    name = "user-plus";
                    break;

                case Symbols.Check:
                    pre = "fas";
                    name = "check";
                    break;

                case Symbols.CheckSquare:
                    name = "check-square";
                    break;

                case Symbols.CheckSquareFull:
                    pre = "fas";
                    name = "check-square";
                    break;

                case Symbols.CheckCircle:
                    name = "check-circle";
                    break;

                case Symbols.CheckCircleFull:
                    pre = "fas";
                    name = "check-circle";
                    break;

                case Symbols.UserShield:
                    pre = "fas";
                    name = "user-shield";
                    break;

                case Symbols.Search:
                    pre = "fas";
                    name = "search";
                    break;

                case Symbols.SearchPlus:
                    pre = "fas";
                    name = "search-plus";
                    break;

                case Symbols.SearchMinus:
                    pre = "fas";
                    name = "search-minus";
                    break;

                case Symbols.SearchDollar:
                    pre = "fas";
                    name = "search-dollar";
                    break;

                case Symbols.SearchLocation:
                    pre = "fas";
                    name = "search-location";
                    break;

                case Symbols.Shield:
                    pre = "fas";
                    name = "shield-alt";
                    break;

                case Symbols.UserLock:
                    pre = "fas";
                    name = "user-lock";
                    break;

                case Symbols.Thumbstick:
                    pre = "fas";
                    name = "thumbstick";
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

        private string GetCaption(CaptionTypes type = CaptionTypes.Content)
        {
            string result = default;

            if (this.Text.IsNull())
            {
                switch (this.Type)
                {
                    case Symbols.Save:
                    case Symbols.SaveFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å lagre", "Click to save") : GetTextByLanguage("Lagre", "Save");
                        break;

                    case Symbols.Close:
                    case Symbols.CloseFull:
                    case Symbols.CloseCircle:
                    case Symbols.CloseCircleFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å lukke", "Click to close") : GetTextByLanguage("Lukk", "Close");
                        break;

                    case Symbols.SignIn:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å logge inn", "Click to sign in") : GetTextByLanguage("Logg inn", "Sign in");
                        break;

                    case Symbols.SignOut:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å logge ut", "Click to sign out") : GetTextByLanguage("Logg ut", "Sign out");
                        break;

                    case Symbols.FileUpload:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste opp", "Click to upload") : GetTextByLanguage("Last opp", "Upload");
                        break;

                    case Symbols.FileDownload:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste ned fil", "Click to download file") : GetTextByLanguage("Last ned", "Download");
                        break;

                    case Symbols.FileExport:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste eksportere", "Click to export") : GetTextByLanguage("Eksporter", "Export");
                        break;

                    case Symbols.DocImage:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se bilde", "Click to see image") : GetTextByLanguage("Se bilde", "See image");
                        break;

                    case Symbols.Edit:
                    case Symbols.EditFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å rediger", "Click to edit") : GetTextByLanguage("Rediger", "Edit");
                        break;

                    case Symbols.Copy:
                    case Symbols.CopyFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å kopiere", "Click to copy") : GetTextByLanguage("Kopier", "Copy");
                        break;

                    case Symbols.AddressCard:
                    case Symbols.AddressCardFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se adresse", "Click to see address") : GetTextByLanguage("Adresse", "Address");
                        break;

                    case Symbols.Calendar:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se kalender", "Click to see calendar") : GetTextByLanguage("Kalender", "Calendar");
                        break;

                    case Symbols.Envelope:
                    case Symbols.EnvelopeFull:
                        result = GetTextByLanguage("E-post", "Email");
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å ende e-post", "Click to send email") : GetTextByLanguage("E-post", "Email");
                        break;

                    case Symbols.Phone:
                    case Symbols.Mobile:
                    case Symbols.MobileFull:
                        result = GetTextByLanguage("Ring", "Call");
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å ringe", "Click to call") : GetTextByLanguage("Ring", "Call");
                        break;

                    case Symbols.Facebook:
                        result = "Facebook";
                        break;

                    case Symbols.Messenger:
                        result = "Messenger";
                        break;

                    case Symbols.DocCode:
                        result = GetTextByLanguage("Kode", "Code");
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se kode", "Click to see code") : GetTextByLanguage("Kode", "Code");
                        break;

                    case Symbols.Comment:
                    case Symbols.CommentFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å skrive kommentar", "Click to leave comment") : GetTextByLanguage("Kommenter", "Comment");
                        break;

                    case Symbols.Comments:
                    case Symbols.CommentsFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se kommentarer", "Click to see comments") : GetTextByLanguage("Kommentarer", "Comments");
                        break;

                    case Symbols.Like:
                    case Symbols.LikeFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å like", "Click to like") : GetTextByLanguage("Lik", "Like");
                        break;

                    case Symbols.Home:
                    case Symbols.HomeDamage:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å gå til hovedside", "Click to go to start page") : GetTextByLanguage("Hjem", "Home");
                        break;

                    case Symbols.GitHub:
                        result = "GitHub";
                        break;

                    case Symbols.Folder:
                    case Symbols.FolderFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å åpne mappe", "Click to open folder") : GetTextByLanguage("Mappe", "Folder");
                        break;

                    case Symbols.Add:
                    case Symbols.AddFull:
                    case Symbols.AddCircle:
                    case Symbols.AddCircleFull:
                        result = GetTextByLanguage("Legg til", "Add");
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å legge til", "Click to add") : GetTextByLanguage("Legg til", "Add");
                        break;

                    case Symbols.ShoppingBasket:
                    case Symbols.ShoppingCart:
                    case Symbols.ShoppingBag:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se handlekurv", "Click to se cart") : GetTextByLanguage("Handlekurv", "Cart");
                        break;

                    case Symbols.ShoppingCartPlus:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å legge til i handlekurv", "Click to add to cart") : GetTextByLanguage("Legg til handlekurv", "Add to cart");
                        break;

                    case Symbols.Handshake:
                    case Symbols.HandshakeFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bekrefte", "Click to confirm") : GetTextByLanguage("Bekreft", "Confirm");
                        break;

                    case Symbols.ThumbsUp:
                    case Symbols.ThumbsUpFull:
                        result = GetTextByLanguage("Tommel opp", "Thumbs up");
                        break;

                    case Symbols.ThumbsDown:
                    case Symbols.ThumbsDownFull:
                        result = GetTextByLanguage("Tommel ned", "Thumbs down");
                        break;

                    case Symbols.Visa:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bruke Visa", "Click to use Visa") : "Visa";
                        break;

                    case Symbols.Stripe:
                        result = "Stripe";
                        break;

                    case Symbols.PayPal:
                    case Symbols.PayPalIcon:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bruke PayPal", "Click to use PayPal") : "PayPal";
                        break;

                    case Symbols.Mastercard:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bruke Mastercard", "Click to use Mastercard") : "Mastercard";
                        break;

                    case Symbols.ApplePay:
                    case Symbols.ApplePayIcon:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bruke Apple Pay", "Click to use Apple Pay") : "Apple Pay";
                        break;

                    case Symbols.AmericanExpress:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bruke American Express", "Click to use American Express") : "American Express";
                        break;

                    case Symbols.AmazonPay:
                    case Symbols.AmazonPayIcon:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bruke Amazon Pay", "Click to use Amazon Pay") : "Amazon Pay";
                        break;

                    case Symbols.GoogleWallet:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å bruke Google Wallet", "Click to use Google Wallet") : "Google Wallet";
                        break;

                    case Symbols.Bullhorn:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å kommentere", "Click to comment") : GetTextByLanguage("kommenter", "Comment");
                        break;

                    case Symbols.Upload:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste opp", "Click to upload") : GetTextByLanguage("Last opp", "Upload");
                        break;

                    case Symbols.Download:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste ned", "Click to download") : GetTextByLanguage("Last ned", "Download");
                        break;

                    case Symbols.UploadCloud:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste opp til skyen", "Click to upload to the cloud") : GetTextByLanguage("Last opp", "Upload");
                        break;

                    case Symbols.DownloadCloud:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste ned fra skyen", "Click to download from the cloud") : GetTextByLanguage("Last ned", "Download");
                        break;

                    case Symbols.FileArchive:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se arkiv", "Click to see archive") : GetTextByLanguage("Arkiv", "Archive");
                        break;

                    case Symbols.FileArchiveFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å laste ned fra skyen", "Click to download from the cloud") : GetTextByLanguage("Last ned", "Download");
                        break;

                    case Symbols.Bars:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å se meny", "Click to see meny") : GetTextByLanguage("Meny", "Menu");
                        break;

                    case Symbols.UserPlusFull:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å legge til ny bruker", "Click to create new user") : GetTextByLanguage("Legg til bruker", "Add user");
                        break;

                    case Symbols.Search:
                        result = TextDeluxe ? GetTextByLanguage("Trykk for å søke", "Click to search") : GetTextByLanguage("Søk", "Search");
                        break;
                }
            }

            string GetTextByLanguage(string no, string us)
            {
                string rslt = this.Lang.Equals(SymbolLanguages.Norwegian) ? no : us;

                return rslt;
            }

            return result;
        }

        /// <summary>
        /// Type of symbol
        /// </summary>
        [HtmlAttributeName("type")]
        public Symbols Type { get; set; }

        /// <summary>
        /// Output format. Default is Span
        /// </summary>
        [HtmlAttributeName("out")]
        public SymbolOutputs Out { get; set; } = SymbolOutputs.Span;

        /// <summary>
        /// Size of symbol. Default is SM
        /// </summary>
        [HtmlAttributeName("size")]
        public SymbolSizes Size { get; set; } = SymbolSizes.SM;

        /// <summary>
        /// The language to use in Content and Title. Default is auto which get's language from Current Culture
        /// </summary>
        [HtmlAttributeName("language")]
        public SymbolLanguages Language { get; set; } = SymbolLanguages.Auto;

        /// <summary>
        /// Text that overrides the default value
        /// </summary>
        [HtmlAttributeName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Define a custom to title to be used by the icon
        /// </summary>
        [HtmlAttributeName("custom-title")]
        public string Title { get; set; }

        /// <summary>
        /// URL to use when output is Href. Default is #
        /// </summary>
        [HtmlAttributeName("href")]
        public string Href { get; set; } = "#";

        /// <summary>
        /// Indicates if a icon supports clicking. Default is true
        /// </summary>
        [HtmlAttributeName("clickable")]
        public bool Clickable { get; set; } = true;

        /// <summary>
        /// If true icon renders without text. Default is true
        /// </summary>
        [HtmlAttributeName("no-text")]
        public bool NoText { get; set; } = true;

        /// <summary>
        /// If true icon will have no title defined. Default is false
        /// </summary>
        [HtmlAttributeName("no-title")]
        public bool NoTitle { get; set; } = false;

        /// <summary>
        /// If true icon renders with default color setting. Default is false
        /// </summary>
        [HtmlAttributeName("no-color")]
        public bool NoColor { get; set; } = false;

        /// <summary>
        /// The Color Profile to use
        /// </summary>
        [HtmlAttributeName("color")]
        public ColorProfiles Color { get; set; } = ColorProfiles.Default;

        /// <summary>
        /// Set to true if need for fixed width. Default is false
        /// </summary>
        [HtmlAttributeName("fixed-width")]
        public bool FixedWidth { get; set; } = false;

        /// <summary>
        /// Adds extra text to icon text and title. Default is false
        /// </summary>
        [HtmlAttributeName("text-deluxe")]
        public bool TextDeluxe { get; set; } = false;

        /// <summary>
        /// Processed Language
        /// </summary>
        private SymbolLanguages Lang
        {
            get
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
        }

        private enum CaptionTypes
        {
            Content,
            Title
        }
    }

    /// <summary>
    /// Supported symbols
    /// </summary>
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
        FileDownload,
        FileExport,
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
        HomeDamage,
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
        UserPlusFull,
        Code,
        CodeLaptop,
        Diagram,
        Add,
        AddFull,
        AddCircle,
        AddCircleFull,
        ShoppingBasket,
        ShoppingCart,
        ShoppingCartPlus,
        ShoppingBag,
        Handshake,
        HandshakeFull,
        ThumbsUp,
        ThumbsUpFull,
        ThumbsDown,
        ThumbsDownFull,
        MoneyCheckout,
        Truck,
        Visa,
        Stripe,
        PayPal,
        PayPalIcon,
        Mastercard,
        ApplePay,
        ApplePayIcon,
        AmericanExpress,
        AmazonPay,
        AmazonPayIcon,
        GoogleWallet,
        Bullhorn,
        Upload,
        Download,
        UploadCloud,
        DownloadCloud,
        FileArchive,
        FileArchiveFull,
        Smile,
        SmileFull,
        SmileWink,
        SmileWinkFull,
        SmileLaugh,
        SmileLaughFull,
        Check,
        CheckSquare,
        CheckSquareFull,
        CheckCircle,
        CheckCircleFull,
        UserShield,
        Search,
        SearchPlus,
        SearchMinus,
        SearchDollar,
        SearchLocation,
        Shield,
        UserLock,
        Thumbstick
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
