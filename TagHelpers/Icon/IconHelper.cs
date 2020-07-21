/*
    @Date			              : 24.06.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core;
using LundbeckConsulting.Components.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Creator.TagHelpers.Icon
{
    internal interface IIconHelper
    {
        /// <summary>
        /// Gets the size record equal to size
        /// </summary>
        /// <param name="size">Icon size</param>
        /// <returns>Record equal to size</returns>
        IIconSizeRecord GetSize(IconSizes size);

        /// <summary>
        /// Gets the tag name based on output format
        /// </summary>
        string GetFormatTagName(IconOutputFormats format);

        /// <summary>
        /// Adds attributes for an anchor tag
        /// </summary>
        /// <param name="tag">Tag to process</param>
        /// <returns>Tag with added anchor attribtues</returns>
        ITagBuilderCustom ProcessAnchorTag(ITagBuilderCustom tag, string href, TagAnchorTargets target = TagAnchorTargets.None);

        /// <summary>
        /// Gets an icon of either type FontAwesome, Friconix or Captain Icon
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <returns>The icon</returns>
        IIconRecord GetIcon(Enum icon);

        /// <summary>
        /// Gets a formated name of the icon
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <returns>Formated icon name</returns>
        string GetCaptainIconName(CaptainIcons icon);
    }

    /// <summary>
    /// Contains different functions for the IconTagHelper
    /// </summary>
    internal sealed class IconHelper : IIconHelper
    {
        private readonly ICollection<IIconRecord> _iconArchive;
        private readonly ICollection<IIconSizeRecord> _sizeArchive;

        public IconHelper()
        {
            _iconArchive = new Collection<IIconRecord>();
            _sizeArchive = new Collection<IIconSizeRecord>()
            {
                new IconSizeRecord(IconSizes.XXS, "fa-xs", "t", "0.4rem"),
                new IconSizeRecord(IconSizes.XS, "fa-sm", "s", "0.8rem"),
                new IconSizeRecord(IconSizes.SM, "fa-lg", "m", "1rem"),
                new IconSizeRecord(IconSizes.MD, "fa-2x", "l", "1.4rem"),
                new IconSizeRecord(IconSizes.Normal, "fa-xs", "s", "1.6rem"),
                new IconSizeRecord(IconSizes.LG, "fa-4x", "h", "2rem"),
                new IconSizeRecord(IconSizes.XL, "fa-5x", "4", "2.6rem"),
                new IconSizeRecord(IconSizes.XXL, "fa-6x", "x", "3.1rem"),
                new IconSizeRecord(IconSizes.XXXL, "fa-7x", "x", "4rem"),
                new IconSizeRecord(IconSizes.X7, "fa-8x", "7", "4.9rem"),
                new IconSizeRecord(IconSizes.X8, "fa-9x", "8", "5.4rem"),
                new IconSizeRecord(IconSizes.X9, "fa-10x", "9", "6.2rem")
            };

            _iconArchive.AddRange(new IIconRecord[] {
                new IconRecord(FontAwesomeIcons.Save, "save", "far"),
                new IconRecord(FontAwesomeIcons.SaveFull, "save"),
                new IconRecord(FontAwesomeIcons.Close, "window-close", "far"),
                new IconRecord(FontAwesomeIcons.CloseFull, "save"),
                new IconRecord(FontAwesomeIcons.CloseCircle, "times-circle", "far"),
                new IconRecord(FontAwesomeIcons.CloseCircleFull, "save"),
                new IconRecord(FontAwesomeIcons.SignIn, "sign-in-alt"),
                new IconRecord(FontAwesomeIcons.SignOut, "sign-out-alt"),
                new IconRecord(FontAwesomeIcons.Doc, "file-alt", "far"),
                new IconRecord(FontAwesomeIcons.DocCode, "file-code", "far"),
                new IconRecord(FontAwesomeIcons.DocImage, "file-image", "far"),
                new IconRecord(FontAwesomeIcons.DocPDF, "file-pdf", "far"),
                new IconRecord(FontAwesomeIcons.FileUpload, "file-upload"),
                new IconRecord(FontAwesomeIcons.FileDownload, "file-download"),
                new IconRecord(FontAwesomeIcons.FileExport, "file-export"),
                new IconRecord(FontAwesomeIcons.Edit, "edit", "far"),
                new IconRecord(FontAwesomeIcons.EditFull, "edit"),
                new IconRecord(FontAwesomeIcons.Copy, "copy", "far"),
                new IconRecord(FontAwesomeIcons.CopyFull, "copy"),
                new IconRecord(FontAwesomeIcons.X, "times"),
                new IconRecord(FontAwesomeIcons.ArrowLeft, "arrow-left"),
                new IconRecord(FontAwesomeIcons.ArrowRight, "arrow-right"),
                new IconRecord(FontAwesomeIcons.ArrowUp, "arrow-up"),
                new IconRecord(FontAwesomeIcons.ArrowDown, "arrow-down"),
                new IconRecord(FontAwesomeIcons.AngleLeft, "angle-left"),
                new IconRecord(FontAwesomeIcons.AngleRight, "angle-right"),
                new IconRecord(FontAwesomeIcons.AngleUp, "angle-up"),
                new IconRecord(FontAwesomeIcons.AngleDown, "angle-down"),
                new IconRecord(FontAwesomeIcons.Calendar, "calendar-alt", "far"),
                new IconRecord(FontAwesomeIcons.CalendarFull, "clendar-alt"),
                new IconRecord(FontAwesomeIcons.Building, "building", "far"),
                new IconRecord(FontAwesomeIcons.BuildingFull, "building"),
                new IconRecord(FontAwesomeIcons.Envelope, "envelope", "far"),
                new IconRecord(FontAwesomeIcons.EnvelopeFull, "envelope"),
                new IconRecord(FontAwesomeIcons.EnvelopeOpen, "envelope-open", "far"),
                new IconRecord(FontAwesomeIcons.EnvelopeOpenFull, "envelope-open"),
                new IconRecord(FontAwesomeIcons.AddressCard, "address-card", "far"),
                new IconRecord(FontAwesomeIcons.AddressCardFull, "address-card"),
                new IconRecord(FontAwesomeIcons.Phone, "phone"),
                new IconRecord(FontAwesomeIcons.PhoneFull, "phone-square"),
                new IconRecord(FontAwesomeIcons.Mobile, "mobile-alt"),
                new IconRecord(FontAwesomeIcons.MobileFull, "mobile"),
                new IconRecord(FontAwesomeIcons.Messenger, "facebook-messenger", "fab"),
                new IconRecord(FontAwesomeIcons.LaptopCode, "laptop-code"),
                new IconRecord(FontAwesomeIcons.Comment, "comment", "far"),
                new IconRecord(FontAwesomeIcons.CommentFull, "comment"),
                new IconRecord(FontAwesomeIcons.Comments, "comments", "far"),
                new IconRecord(FontAwesomeIcons.CommentsFull, "comments"),
                new IconRecord(FontAwesomeIcons.Like, "thumbs-up", "far"),
                new IconRecord(FontAwesomeIcons.LikeFull, "thumbs-up"),
                new IconRecord(FontAwesomeIcons.Home, "home"),
                new IconRecord(FontAwesomeIcons.HomeDamage, "house-damage"),
                new IconRecord(FontAwesomeIcons.Bars, "bars"),
                new IconRecord(FontAwesomeIcons.GitHub, "github", "fab"),
                new IconRecord(FontAwesomeIcons.Globe, "globe"),
                new IconRecord(FontAwesomeIcons.AddressBook, "address-book", "far"),
                new IconRecord(FontAwesomeIcons.AddressBookFull, "address-book"),
                new IconRecord(FontAwesomeIcons.Folder, "folder", "far"),
                new IconRecord(FontAwesomeIcons.FolderFull, "folder"),
                new IconRecord(FontAwesomeIcons.FolderMinusFull, "folder-minus"),
                new IconRecord(FontAwesomeIcons.FolderPlusFull, "folder-plus"),
                new IconRecord(FontAwesomeIcons.FolderOpen, "folder-open", "far"),
                new IconRecord(FontAwesomeIcons.FolderOpenFull, "folder-open"),
                new IconRecord(FontAwesomeIcons.UserCircle, "user-circle", "far"),
                new IconRecord(FontAwesomeIcons.UserCircleFull, "user-circle"),
                new IconRecord(FontAwesomeIcons.UserNinja, "user-ninja"),
                new IconRecord(FontAwesomeIcons.UserSpy, "user-secret"),
                new IconRecord(FontAwesomeIcons.UserTie, "user-tie"),
                new IconRecord(FontAwesomeIcons.User, "user", "far"),
                new IconRecord(FontAwesomeIcons.UserFull, "user"),
                new IconRecord(FontAwesomeIcons.Code, "code"),
                new IconRecord(FontAwesomeIcons.Diagram, "project-diagram"),
                new IconRecord(FontAwesomeIcons.Add, "plus-square", "far"),
                new IconRecord(FontAwesomeIcons.AddFull, "plus-square"),
                new IconRecord(FontAwesomeIcons.AddCircle, "plus-circle", "far"),
                new IconRecord(FontAwesomeIcons.AddCircleFull, "plus-circle"),
                new IconRecord(FontAwesomeIcons.ShoppingBasket, "shopping-basket"),
                new IconRecord(FontAwesomeIcons.ShoppingCart, "shopping-cart"),
                new IconRecord(FontAwesomeIcons.ShoppingCartPlus, "cart-plus"),
                new IconRecord(FontAwesomeIcons.ShoppingBag, "shopping-bag"),
                new IconRecord(FontAwesomeIcons.Handshake, "handshake"),
                new IconRecord(FontAwesomeIcons.HandshakeFull, "handshake", "far"),
                new IconRecord(FontAwesomeIcons.ThumbsUp, "thumbs-up", "far"),
                new IconRecord(FontAwesomeIcons.ThumbsUpFull, "thumbs-up"),
                new IconRecord(FontAwesomeIcons.ThumbsDown, "thumbs-down", "far"),
                new IconRecord(FontAwesomeIcons.ThumbsDownFull, "thumbs-down"),
                new IconRecord(FontAwesomeIcons.MoneyCheckout, "money-checkout-alt"),
                new IconRecord(FontAwesomeIcons.Truck, "truck"),
                new IconRecord(FontAwesomeIcons.Visa, "cc-visa", "fab"),
                new IconRecord(FontAwesomeIcons.Stripe, "cc-stripe", "fab"),
                new IconRecord(FontAwesomeIcons.PayPal, "cc-paypal", "fab"),
                new IconRecord(FontAwesomeIcons.PayPalIcon, "paypal", "fab"),
                new IconRecord(FontAwesomeIcons.Mastercard, "cc-mastercard", "fab"),
                new IconRecord(FontAwesomeIcons.ApplePay, "cc-apple-pay", "fab"),
                new IconRecord(FontAwesomeIcons.ApplePayIcon, "apple-pay", "fab"),
                new IconRecord(FontAwesomeIcons.AmericanExpress, "cc-amex", "fab"),
                new IconRecord(FontAwesomeIcons.AmazonPay, "cc-amazon-pay", "fab"),
                new IconRecord(FontAwesomeIcons.AmazonPayIcon, "amazon-pay", "fab"),
                new IconRecord(FontAwesomeIcons.GoogleWallet, "google-wallet", "fab"),
                new IconRecord(FontAwesomeIcons.Upload, "upload"),
                new IconRecord(FontAwesomeIcons.UploadCloud, "cloud-upload-alt"),
                new IconRecord(FontAwesomeIcons.DownloadCloud, "cloud-download-alt"),
                new IconRecord(FontAwesomeIcons.Bullhorn, "bullhorn"),
                new IconRecord(FontAwesomeIcons.Smile, "smile", "far"),
                new IconRecord(FontAwesomeIcons.SmileFull, "smile"),
                new IconRecord(FontAwesomeIcons.SmileWink, "smile-wink", "far"),
                new IconRecord(FontAwesomeIcons.SmileWinkFull, "smile-wink"),
                new IconRecord(FontAwesomeIcons.SmileLaugh, "grin", "far"),
                new IconRecord(FontAwesomeIcons.SmileLaughFull, "grin"),
                new IconRecord(FontAwesomeIcons.UserPlusFull, "user-plus"),
                new IconRecord(FontAwesomeIcons.Check, "check"),
                new IconRecord(FontAwesomeIcons.CheckSquare, "check-square"),
                new IconRecord(FontAwesomeIcons.CheckSquareFull, "check-square"),
                new IconRecord(FontAwesomeIcons.CheckCircle, "check-circle", "far"),
                new IconRecord(FontAwesomeIcons.CheckCircleFull, "check-circle"),
                new IconRecord(FontAwesomeIcons.UserShield, "user-shield"),
                new IconRecord(FontAwesomeIcons.Search, "search"),
                new IconRecord(FontAwesomeIcons.SearchPlus, "search-plus"),
                new IconRecord(FontAwesomeIcons.SearchMinus, "search-minus"),
                new IconRecord(FontAwesomeIcons.SearchDollar, "search-dollar"),
                new IconRecord(FontAwesomeIcons.SearchLocation, "search-location"),
                new IconRecord(FontAwesomeIcons.Shield, "shield"),
                new IconRecord(FontAwesomeIcons.UserLock, "user-lock"),
                new IconRecord(FontAwesomeIcons.Thumbstick, "thumbstick"),
                new IconRecord(FontAwesomeIcons.FacebookLogo, "facebook-f", "fab"),
                new IconRecord(FontAwesomeIcons.FacebookLogoCircle, "facebook", "fab"),
                new IconRecord(FontAwesomeIcons.FacebookLogoSquare, "facebook-square", "fab"),
                new IconRecord(FontAwesomeIcons.LinkedInLogo, "linkedin-in", "fab"),
                new IconRecord(FontAwesomeIcons.LinkedInLogoSquare, "linkedin", "fab"),
                new IconRecord(FontAwesomeIcons.DeleteTrash, "trast-alt", "far"),
                new IconRecord(FontAwesomeIcons.DeleteTrashBold, "trash-alt"),
                new IconRecord(FontAwesomeIcons.DeleteTrashFull, "trash"),
                new IconRecord(FontAwesomeIcons.DeleteMinusCircleFull, "minus-circle"),
                new IconRecord(FontAwesomeIcons.DeleteMinus, "minus"),
                new IconRecord(FontAwesomeIcons.DeleteMinusSquare, "minus-square", "far"),
                new IconRecord(FontAwesomeIcons.DeleteMinusSquareFull, "minus-square"),
                new IconRecord(FontAwesomeIcons.DeleteUser, "user-times"),
                new IconRecord(FontAwesomeIcons.DeleteUserMinus, "user-minus"),
                new IconRecord(FontAwesomeIcons.GoogleIcon, "google", "fab"),
                new IconRecord(FontAwesomeIcons.GooglePlay, "google-play", "fab"),
                new IconRecord(FontAwesomeIcons.GoogleDrive, "google-drive", "fab"),
                new IconRecord(FontAwesomeIcons.Database, "database"),
                new IconRecord(FontAwesomeIcons.Pager, "pager"),
                new IconRecord(FontAwesomeIcons.Tag, "tag"),
                new IconRecord(FontAwesomeIcons.Tags, "tags"),
                new IconRecord(FontAwesomeIcons.InstagramIcon, "instagram", "fab"),
                new IconRecord(FontAwesomeIcons.InstagramSquare, "instagram-square", "fab"),
                new IconRecord(FontAwesomeIcons.Percentage, "percentage"),
                new IconRecord(FontAwesomeIcons.Hashtag, "hashtag"),
                new IconRecord(FontAwesomeIcons.Users, "users"),
                new IconRecord(FontAwesomeIcons.UserFriends, "user-friends"),
                new IconRecord(FontAwesomeIcons.Info, "info"),
                new IconRecord(FontAwesomeIcons.InfoCircle, "info-circle"),
                new IconRecord(FontAwesomeIcons.Swatchbook, "swatchbook"),
                new IconRecord(FontAwesomeIcons.Key, "key"),
                new IconRecord(FontAwesomeIcons.Times, "times"),
                new IconRecord(FontAwesomeIcons.PhotoVideo, "photo-video"),
                new IconRecord(FontAwesomeIcons.PaintBrush, "paint-brush"),
                new IconRecord(FontAwesomeIcons.PaintBrushWide, "brush"),
                new IconRecord(FontAwesomeIcons.PaintRoller, "paint-roller"),
                new IconRecord(FontAwesomeIcons.Palette, "palette")

            });

            _iconArchive.AddRange(new IIconRecord[] {
                new IconRecord(FriconixIcons.Facebook, "facebook"),
                new IconRecord(FriconixIcons.GoogleLogo, "google-logo"),
                new IconRecord(FriconixIcons.AmazonLogo, "amazon"),
                new IconRecord(FriconixIcons.Shopify, "shopify"),
                new IconRecord(FriconixIcons.ShoppingCart, "shopping-cart"),
                new IconRecord(FriconixIcons.ShoppingCartSolid, "shopping-cart-solid"),
                new IconRecord(FriconixIcons.ShoppingCartWide, "shopping-cart-wide"),
                new IconRecord(FriconixIcons.ShoppingCartWide, "shopping-cart-wide"),
                new IconRecord(FriconixIcons.UserThin, "user-thin"),
                new IconRecord(FriconixIcons.UserCircle, "user-circle"),
                new IconRecord(FriconixIcons.UserSolid, "user-solid"),
                new IconRecord(FriconixIcons.User, "user"),
                new IconRecord(FriconixIcons.UserCircleSolid, "user-circle-solid"),
                new IconRecord(FriconixIcons.UserCircleThin, "user-circle-thin"),
                new IconRecord(FriconixIcons.UserLockSolid, "user-lock-solid"),
                new IconRecord(FriconixIcons.UserLockWide, "user-lock-wide"),
                new IconRecord(FriconixIcons.UserLock, "user-lock"),
                new IconRecord(FriconixIcons.UserUnlockSolid, "user-unlock-solid"),
                new IconRecord(FriconixIcons.UserLockThin, "user-lock-thin"),
                new IconRecord(FriconixIcons.UserUnlock, "user-unlock"),
                new IconRecord(FriconixIcons.UserCircleWide, "user-circle-wide"),
                new IconRecord(FriconixIcons.JavaScriptSquare, "js"),
                new IconRecord(FriconixIcons.PlusSolid, "plus-solid"),
                new IconRecord(FriconixIcons.WarningSolid, "warning-solid"),
                new IconRecord(FriconixIcons.Update, "update"),
                new IconRecord(FriconixIcons.TeamSolid, "team-solid"),
                new IconRecord(FriconixIcons.Check, "check"),
                new IconRecord(FriconixIcons.NetworkSolid, "network-solid")
            });

            foreach (CaptainIcons icon in Enum.GetValues(typeof(CaptainIcons)))
            {
                _iconArchive.Add(new IconRecord(icon, GetCaptainIconName(icon)));
            }

            _iconArchive.AddRange(new IIconRecord[] {
                new IconRecord(DevIcons.Git, "git", "602"),
                new IconRecord(DevIcons.GitCompare, "git_compare", "628"),
                new IconRecord(DevIcons.GitBranch, "git_branch", "625"),
                new IconRecord(DevIcons.GitCommit, "git_commit", "629"),
                new IconRecord(DevIcons.GitPullRequest, "git_pull_request", "626"),
                new IconRecord(DevIcons.GitMerge, "git_merge", "627"),
                new IconRecord(DevIcons.Bitbucket, "bitbucket", "603"),
                new IconRecord(DevIcons.GithubAlt, "github_alt", "608"),
                new IconRecord(DevIcons.GithubBadge, "github_badge", "609"),
                new IconRecord(DevIcons.Github, "github", "60a"),
                new IconRecord(DevIcons.GithubFull, "github_full", "617"),
                new IconRecord(DevIcons.Java, "java", "630"),
                new IconRecord(DevIcons.Ruby, "ruby", "639"),
                new IconRecord(DevIcons.Scala, "scala", "637"),
                new IconRecord(DevIcons.Python, "python", "63c"),
                new IconRecord(DevIcons.Go, "go", "624"),
                new IconRecord(DevIcons.RubyOnRails, "ruby_on_rails", "63b"),
                new IconRecord(DevIcons.Django, "django", "61d"),
                new IconRecord(DevIcons.Markdown, "markdown", "63e"),
                new IconRecord(DevIcons.PHP, "php", "63d"),
                new IconRecord(DevIcons.mySQL, "mysql", "604"),
                new IconRecord(DevIcons.Streamline, "streamline", "605"),
                new IconRecord(DevIcons.Database, "database", "606"),
                new IconRecord(DevIcons.Laravel, "laravel", "63f"),
                new IconRecord(DevIcons.JavaScript, "javascript", "64e"),
                new IconRecord(DevIcons.Angular, "angular", "653"),
                new IconRecord(DevIcons.Backbone, "backbone", "652"),
                new IconRecord(DevIcons.Coffeescript, "cofffeescript", "651"),
                new IconRecord(DevIcons.jQuery, "jquery", "650"),
                new IconRecord(DevIcons.Modernizr, "modernizr", "620"),
                new IconRecord(DevIcons.jQueryUI, "jquery_ui", "654"),
                new IconRecord(DevIcons.Ember, "ember", "61b"),
                new IconRecord(DevIcons.Dojo, "dojo", "61c"),
                new IconRecord(DevIcons.NodeJs, "nodejs", "619"),
                new IconRecord(DevIcons.NodeJsSmall, "nodejs_small", "618"),
                new IconRecord(DevIcons.JavaScriptShield, "javascript_shield", "64f"),
                new IconRecord(DevIcons.Bootstrap, "bootstrap", "647"),
                new IconRecord(DevIcons.SASS, "sass", "64b"),
                new IconRecord(DevIcons.CSS3full, "css3_full", "64a"),
                new IconRecord(DevIcons.CSS3, "css3", "649"),
                new IconRecord(DevIcons.HTML5, "html5", "636"),
                new IconRecord(DevIcons.HTML5Multimedia, "html5_multimedia", "632"),
                new IconRecord(DevIcons.HTML5DeviceAccess, "html5_device_access", "633"),
                new IconRecord(DevIcons.HTML53dEffects, "html5_3d_effects", "635"),
                new IconRecord(DevIcons.HTML5Connectivity, "html5_connectivity", "634"),
                new IconRecord(DevIcons.GhostSmall, "ghost_small", "614"),
                new IconRecord(DevIcons.Ghost, "ghost", "61f"),
                new IconRecord(DevIcons.Magento, "magento", "640"),
                new IconRecord(DevIcons.Joomla, "joomla", "641"),
                new IconRecord(DevIcons.JekyllSmall, "jekyll_small", "60d"),
                new IconRecord(DevIcons.Drupal, "drupal", "642"),
                new IconRecord(DevIcons.Wordpress, "wordpress", "60b"),
                new IconRecord(DevIcons.Grunt, "grunt", "64c"),
                new IconRecord(DevIcons.Bower, "bower", "64d"),
                new IconRecord(DevIcons.Npm, "npm", "61e"),
                new IconRecord(DevIcons.YahooSmall, "yahoo_small", "62d"),
                new IconRecord(DevIcons.Yahoo, "yahoo", "615"),
                new IconRecord(DevIcons.BingSmall, "bing_small", "600"),
                new IconRecord(DevIcons.Windows, "windows", "60f"),
                new IconRecord(DevIcons.Linux, "linux", "612"),
                new IconRecord(DevIcons.Ubuntu, "ubuntu", "63a"),
                new IconRecord(DevIcons.Android, "android", "60e"),
                new IconRecord(DevIcons.Apple, "apple", "611"),
                new IconRecord(DevIcons.Appstore, "appstore", "613"),
                new IconRecord(DevIcons.Phonegap, "phonegap", "630"),
                new IconRecord(DevIcons.Blackberry, "blackberry", "623"),
                new IconRecord(DevIcons.Stackoverflow, "stackoverflow", "610"),
                new IconRecord(DevIcons.Techcrunch, "techcrunch", "62c"),
                new IconRecord(DevIcons.Codrops, "coddrops", "62f"),
                new IconRecord(DevIcons.CssTricks, "css_tricks", "601"),
                new IconRecord(DevIcons.SmashingMagazine, "smashing_magazine", "62d"),
                new IconRecord(DevIcons.Netmagazine, "netmagazine", "62e"),
                new IconRecord(DevIcons.Codepen, "codepen", "616"),
                new IconRecord(DevIcons.Cssdeck, "cssdeck", "62a"),
                new IconRecord(DevIcons.Hackernews, "hackernews", "61a"),
                new IconRecord(DevIcons.Dropbox, "dropbox", "607"),
                new IconRecord(DevIcons.GoogleDrive, "google_drive", "631"),
                new IconRecord(DevIcons.VisualStudio, "visualstudio", "60c"),
                new IconRecord(DevIcons.UnitySmall, "unity_small", "621"),
                new IconRecord(DevIcons.RaspberryPi, "raspberry_pi", "622"),
                new IconRecord(DevIcons.Chrome, "chrome", "643"),
                new IconRecord(DevIcons.IE, "ie", "644"),
                new IconRecord(DevIcons.Firefox, "firefox", "645"),
                new IconRecord(DevIcons.Opera, "opera", "646"),
                new IconRecord(DevIcons.Safari, "safari", "648"),
                new IconRecord(DevIcons.Swift, "swift", "655"),
                new IconRecord(DevIcons.Symfony, "symfony", "656"),
                new IconRecord(DevIcons.SymfonyBadge, "symfony_badge", "657"),
                new IconRecord(DevIcons.Less, "less", "658"),
                new IconRecord(DevIcons.Stylus, "stylus", "659"),
                new IconRecord(DevIcons.Trello, "trello", "65a"),
                new IconRecord(DevIcons.Atlassian, "atlassian", "65b"),
                new IconRecord(DevIcons.Jira, "jira", "65c"),
                new IconRecord(DevIcons.Envato, "envato", "65d"),
                new IconRecord(DevIcons.SnapSvg, "snap_svg", "65e"),
                new IconRecord(DevIcons.Raphael, "raphael", "65f"),
                new IconRecord(DevIcons.GoogleAnalytics, "google_analytics", "660"),
                new IconRecord(DevIcons.Compass, "compass", "661"),
                new IconRecord(DevIcons.OneDrive, "onedrive", "662"),
                new IconRecord(DevIcons.Gulp, "gulp", "663"),
                new IconRecord(DevIcons.Atom, "atom", "664"),
                new IconRecord(DevIcons.Cisco, "cisco", "665"),
                new IconRecord(DevIcons.Nancy, "nancy", "666"),
                new IconRecord(DevIcons.Clojure, "clojuro", "668"),
                new IconRecord(DevIcons.ClojureAlt, "clojuro_alt", "66a"),
                new IconRecord(DevIcons.Perl, "perl", "669"),
                new IconRecord(DevIcons.Celluloid, "celluloid", "66b"),
                new IconRecord(DevIcons.W3C, "w3c", "66c"),
                new IconRecord(DevIcons.Redis, "redis", "66d"),
                new IconRecord(DevIcons.Postgresql, "postgresql", "66e"),
                new IconRecord(DevIcons.Webplatform, "webplatform", "66f"),
                new IconRecord(DevIcons.Jenkins, "jenkins", "667"),
                new IconRecord(DevIcons.RequireJS, "requirejs", "670"),
                new IconRecord(DevIcons.Opensource, "opensource", "671"),
                new IconRecord(DevIcons.Typo3, "typo3", "672"),
                new IconRecord(DevIcons.UIkit, "uikit", "673"),
                new IconRecord(DevIcons.Doctrine, "doctrine", "674"),
                new IconRecord(DevIcons.Groovy, "groovy", "675"),
                new IconRecord(DevIcons.Nginx, "nginx", "676"),
                new IconRecord(DevIcons.Haskell, "haskell", "677"),
                new IconRecord(DevIcons.Zend, "zend", "678"),
                new IconRecord(DevIcons.Gnu, "gnu", "679"),
                new IconRecord(DevIcons.Yeoman, "yeoman", "67a"),
                new IconRecord(DevIcons.Heroku, "heroku", "67b"),
                new IconRecord(DevIcons.Debian, "debian", "67d"),
                new IconRecord(DevIcons.Travis, "travis", "67e"),
                new IconRecord(DevIcons.Dotnet, "dotnet", "67f"),
                new IconRecord(DevIcons.Codeigniter, "codeigniter", "680"),
                new IconRecord(DevIcons.JavaScriptBadge, "javascript_badge", "681"),
                new IconRecord(DevIcons.Yii, "yii", "682"),
                new IconRecord(DevIcons.MsSqlServer, "mssql_server", "67c"),
                new IconRecord(DevIcons.Composer, "composer", "683"),
                new IconRecord(DevIcons.KrakenJsBadge, "krakenjs_badge", "684"),
                new IconRecord(DevIcons.KrakenJS, "krakenjs", "685"),
                new IconRecord(DevIcons.Mozilla, "mozilla", "686"),
                new IconRecord(DevIcons.Firebase, "firebase", "687"),
                new IconRecord(DevIcons.SizzleJS, "sizzlejs", "688"),
                new IconRecord(DevIcons.CreativeCommons, "creativecommons", "689"),
                new IconRecord(DevIcons.CreativeCommonsBadge, "creativecommons_badge", "68a"),
                new IconRecord(DevIcons.Mitlicence, "mitlicence", "68b"),
                new IconRecord(DevIcons.Senchatouch, "senchatouch", "68c"),
                new IconRecord(DevIcons.Bugsense, "bugsense", "68d"),
                new IconRecord(DevIcons.ExtJS, "extjs", "68e"),
                new IconRecord(DevIcons.MootoolsBadge, "mootools_badge", "68f"),
                new IconRecord(DevIcons.Mootools, "mootools", "690"),
                new IconRecord(DevIcons.RubyRough, "ruby_rough", "691"),
                new IconRecord(DevIcons.Komodo, "komodo", "692"),
                new IconRecord(DevIcons.Coda, "coda", "693"),
                new IconRecord(DevIcons.Bintray, "bintray", "694"),
                new IconRecord(DevIcons.Terminal, "terminal", "695"),
                new IconRecord(DevIcons.Code, "code", "696"),
                new IconRecord(DevIcons.Responsive, "responsive", "697"),
                new IconRecord(DevIcons.Dart, "dart", "698"),
                new IconRecord(DevIcons.Aptana, "aptana", "699"),
                new IconRecord(DevIcons.Mailchimp, "mailchimp", "69a"),
                new IconRecord(DevIcons.Netbeans, "netbeans", "69b"),
                new IconRecord(DevIcons.Dreamweaver, "dreamweaver", "69c"),
                new IconRecord(DevIcons.Brackets, "brackets", "69d"),
                new IconRecord(DevIcons.Eclipse, "eclipse", "69e"),
                new IconRecord(DevIcons.Cloud9, "cloud9", "69f"),
                new IconRecord(DevIcons.Scrum, "scrum", "6a0"),
                new IconRecord(DevIcons.Prolog, "prolog", "6a1"),
                new IconRecord(DevIcons.TerminalBadge, "terminal_badge", "6a2"),
                new IconRecord(DevIcons.CodeBadge, "code_badge", "6a3"),
                new IconRecord(DevIcons.Mongodb, "mongodb", "6a4"),
                new IconRecord(DevIcons.Meteor, "meteor", "6a5"),
                new IconRecord(DevIcons.Meteorfull, "meteorfull", "6a6"),
                new IconRecord(DevIcons.Fsharp, "fsharp", "6a7"),
                new IconRecord(DevIcons.Rust, "rust", "6a8"),
                new IconRecord(DevIcons.Ionic, "ionic", "6a9"),
                new IconRecord(DevIcons.Sublime, "sublime", "6aa"),
                new IconRecord(DevIcons.Appcelerator, "appcelerator", "6ab"),
                new IconRecord(DevIcons.Asterisk, "asterisk", "6ac"),
                new IconRecord(DevIcons.AWS, "aws", "6ad"),
                new IconRecord(DevIcons.DigitalOcean, "digital-ocean", "6ae"),
                new IconRecord(DevIcons.Dlang, "dlang", "6af"),
                new IconRecord(DevIcons.Docker, "docker", "6b0"),
                new IconRecord(DevIcons.Erlang, "erlang", "6b1"),
                new IconRecord(DevIcons.GoogleCloudPlatform, "google-cloud-platform", "6b2"),
                new IconRecord(DevIcons.Grails, "grails", "6b3"),
                new IconRecord(DevIcons.Illustrator, "illustrator", "6b4"),
                new IconRecord(DevIcons.Intellij, "intellij", "6b5"),
                new IconRecord(DevIcons.Materializecss, "materializecss", "6b6"),
                new IconRecord(DevIcons.Openshift, "openshift", "6b7"),
                new IconRecord(DevIcons.Photoshop, "photoshop", "6b8"),
                new IconRecord(DevIcons.Rackspace, "rackspace", "6b9"),
                new IconRecord(DevIcons.React, "react", "6ba"),
                new IconRecord(DevIcons.Redhat, "readhat", "6bb"),
                new IconRecord(DevIcons.Scriptcs, "scriptcs", "6bc"),
                new IconRecord(DevIcons.SQLlite, "sqllite", "6c4"),
                new IconRecord(DevIcons.VIM, "vim", "6c5")
            });
        }

        public IIconSizeRecord GetSize(IconSizes size) => _sizeArchive.Single(rc => rc.Size == size);

        public string GetFormatTagName(IconOutputFormats format)
        {
            string result = string.Empty;

            switch (format)
            {
                case IconOutputFormats.Anchor:
                    result = "a";
                    break;

                case IconOutputFormats.Button:
                    result = "button";
                    break;

                case IconOutputFormats.Div:
                    result = "div";
                    break;

                case IconOutputFormats.Span:
                    result = "span";
                    break;

                case IconOutputFormats.Italic:
                    result = "i";
                    break;

                case IconOutputFormats.Strong:
                    result = "strong";
                    break;
            }

            return result;
        }

        public ITagBuilderCustom ProcessAnchorTag(ITagBuilderCustom tag, string href, TagAnchorTargets target = TagAnchorTargets.None)
        {
            tag.AddAttribute("href", href, false);

            if (!target.Equal(TagAnchorTargets.None))
            {
                tag.AddAttribute("target", $"_{target.ToString().ToLower()}", false);
            }

            return tag;
        }

        public IIconRecord GetIcon(Enum icon)
        {
            IIconRecord result = default;

            switch (icon)
            {
                case FontAwesomeIcons fa:
                    result = _iconArchive.Single(icn => icn.FontAwesomeIcon == (FontAwesomeIcons)icon);
                    break;

                case FriconixIcons fi:
                    result = _iconArchive.Single(icn => icn.FriconixIcon == (FriconixIcons)icon);
                    break;

                case CaptainIcons ci:
                    result = _iconArchive.Single(icn => icn.CaptainIcon == (CaptainIcons)icon);
                    break;

                case DevIcons di:
                    result = _iconArchive.Single(icn => icn.DevIcon == (DevIcons)icon);
                    break;
            }

            return result;
        }

        public string GetCaptainIconName(CaptainIcons icon)
        {
            string result = ((Int32)icon).ToString();

            if (result.Length == 1)
            {
                result = $"00{result}";
            }
            else if (result.Length == 2)
            {
                result = $"0{result}";
            }

            return result;
        }
    }

    #region Enums
    /// <summary>
    /// Supported output formats when rendering Font Awesome icon
    /// </summary>
    public enum IconOutputFormats
    {
        Span,
        Anchor,
        Button,
        Div,
        Italic,
        Strong
    }

    /// <summary>
    /// Supported icon sets
    /// </summary>
    public enum IconSets
    {
        FontAwesome,
        Friconix,
        CaptainIcon,
        DevIcon
    }

    /// <summary>
    /// Supported icon sizes
    /// </summary>
    public enum IconSizes
    {
        XXS,
        XS,
        SM,
        MD,
        Normal,
        LG,
        XL,
        XXL,
        XXXL,
        X7,
        X8,
        X9
    }

    /// <summary>
    /// List of all icons in the Captain Icon set
    /// </summary>
    public enum CaptainIcons
    {
        None = 000,
        House = 001,
        ExclamationMark = 002,
        TalkingBubbleEmpty = 003,
        TalkingBubble = 004,
        Cloud = 005,
        Cog = 006,
        Cogs = 007,
        Heart = 008,
        Lock = 009,
        Unlock = 010,
        GoBack = 011,
        Refresh = 012,
        Clock = 013,
        ArtPallete = 014,
        Compass = 015,
        StopWatch = 016,
        Inbox = 017,
        Outbox = 018,
        Email = 019,
        Tag = 020,
        Eye = 021,
        Star = 022,
        MobilePhone = 023,
        TV = 024,
        IPod = 025,
        PhotoCamera = 026,
        Briefcase = 027,
        Archive = 028,
        Umbrella = 029,
        Thrashcan = 030,
        ShoppingCart = 031,
        ShoppingCartEmpty = 032,
        CalendarDay = 033,
        UnknownOne = 034,
        UnknownTwo = 035,
        PuzzlePiece = 036,
        SunShining = 037,
        Map = 038,
        ArtPalleteHollow = 039,
        LightBulb = 040,
        Phone = 041,
        UknownThree = 042,
        BulletinList = 043,
        NumberedList = 044,
        QuestionMark = 045,
        Document = 046,
        DocumentWithContent = 047,
        DoorLock = 048,
        UknownFour = 049,
        Peoples = 050,
        PeoplesTwo = 051,
        ComputerMouse = 052,
        ComputerKeyboard = 053,
        Pen = 054,
        Checkmark = 055,
        CheckboxChecked = 056,
        Xmark = 057,
        CheckboxXmarked = 058,
        PlusSign = 059,
        LightningAndCloud = 060,
        UknownFive = 061,
        Laptop = 062,
        Cup = 063,
        Lightning = 064,
        UknownSix = 065,
        UnknownSeven = 066,
        VinylRecord = 067,
        TalkingBubbleContentTwo = 068,
        Speaker = 069,
        UknownEight = 070,
        PenTwo = 071,
        UknownNine = 072,
        RecordPlayer = 073,
        WireAndSocket = 074,
        ReadingLamp = 075,
        ConsoleController = 076,
        PicturePersonAndSun = 078,
        Books = 079,
        Handpurse = 080,
        Glasses = 081,
        MoneyStack = 082,
        EditDocument = 083,
        Note = 084,
        NoteSingle = 085,
        OnSymbol = 086,
        Zoom = 087,
        ZoomOut = 088,
        GlassAndFork = 089,
        Fax = 090,
        FacebookSimple = 091,
        FacebookIcon = 092,
        FacebookIconCircle = 093,
        Bird = 094,
        BirdSquare = 095,
        BirdCircle = 096,
        BasketballSimple = 100,
        BasketballSquare = 101,
        Basketball = 102,
        GooglePlus = 103,
        GooglePlusSquare = 104,
        GooglePlusCircle = 105,
        Wireless = 106,
        WirelessSquare = 107,
        WirelessCircle = 108,
        LinkedIn = 109,
        LinkedInSquare = 110,
        LinkedInCircle = 111,
        YouTube = 112,
        TalkingBubbleWithPhone = 121,
        TalkingBubbleWithPhoneSquare = 122,
        TalkingBubbleWithPhoneCircle = 123,
        AtSymbol = 127,
        Fire = 132,
        Mustache = 133,
        BoxOpen = 134,
        DocumentFolder = 135,
        Network = 136,
        UsbSymbol = 137,
        EmailIn = 139,
        EmailOut = 140,
        TalkingBubbles = 141,
        Pin = 143,
        ProfileCard = 144,
        Baloon = 148,
        Rocketship = 151,
        RainyCloud = 152,
        BowlingBall = 159,
        ProfileCardTwo = 163,
        PieChart = 166,
        Ruler = 167,
        Bicycle = 169,
        TennisBall = 170,
        AmericanFootball = 173,
        ClothHanger = 182,
        Key = 183,
        Pacman = 185,
        BatteryIndicatorNoBars = 188,
        BatteryIndicatorNoBattery = 189,
        BatteryIndicatorOneBar = 190,
        BatteryIndicatorTwoBars = 191,
        BatteryIndicatorThreeBars = 192,
        BatteryIndicatorCharging = 193,
        TargetCircle = 199,
        TargetCircleWithArrow = 200,
        EightBall = 201,
        FishBones = 204,
        ThumbsUp = 211,
        ThumbsDown = 212,
        Feather = 218,
        HtmlFive = 223,
        SmartPhone = 225,
        Diskette = 226,
        Earphones = 227,
        DocumentBoxContent = 228,
        DocumentBoxEmpty = 229,
        TagWithContent = 230,
        CloudCheckmark = 232,
        CloudReload = 233,
        CloudZoom = 234,
        CloudUpArrow = 235,
        CloudDownArrow = 236,
        CloudConfiguration = 237,
        Sunglasses = 242,
        Quotes = 243,
        QuotesEnd = 244,
        Link = 245,
        DocumentLayers = 248,
        MoveCross = 249,
        MagicWand = 255,
        Text = 261,
        ArrowLeftUp = 262,
        DocumentWithContentTwo = 266,
        Stars = 269,
        ToilettPaper = 271,
        Controls = 272,
        Binder = 274,
        Tools = 275,
        ArrowRight = 276,
        ArrowLeft = 277,
        ArrowDown = 278,
        ArrowUp = 279,
        Pause = 280,
        ForwardSkip = 281,
        BackwardSkip = 282,
        ArrowRightCircle = 286,
        ArrowLeftCircle = 287,
        ArrowDownCircle = 288,
        ArrowUpCircle = 289,
        PauseCircle = 290,
        ForwardSkipCircle = 291,
        BackwardSkipCircle = 291,
        StopCircle = 295,
        PictureInFrame = 300,
        CardOfSpades = 302,
        Volume = 306,
        WirelessSignals = 309,
        Calendar = 313,
        BarsMenu = 316,
        Anchor = 232,
        Flower = 335,
        Flowers = 336,
        DataLayers = 338,
        Info = 341,
        InfoCircle = 342,
        TalkingBubbleWithContent = 353,
        TalkingBubbleWithContentThick = 354,
        EnvelopeWithLetter = 357
    }

    /// <summary>
    /// Supported icons for Friconix
    /// </summary>
    public enum FriconixIcons
    {
        None,
        Facebook,
        GoogleLogo,
        AmazonLogo,
        ShoppingCart,
        ShoppingCartSolid,
        ShoppingCartWide,
        Shopify,
        UserThin,
        UserCircle,
        UserSolid,
        User,
        UserCircleSolid,
        UserCircleThin,
        UserLockSolid,
        UserLockWide,
        UserLock,
        UserUnlockSolid,
        UserLockThin,
        UserUnlockWide,
        UserUnlock,
        UserUnlockThin,
        UserCircleWide,
        JavaScriptSquare,
        PlusSolid,
        WarningSolid,
        Update,
        TeamSolid,
        Check,
        NetworkSolid
    }

    /// <summary>
    /// Supported DevIcons
    /// </summary>
    public enum DevIcons
    {
        None,
        Git,
        GitCompare,
        GitBranch,
        GitCommit,
        GitPullRequest,
        GitMerge,
        Bitbucket,
        GithubAlt,
        GithubBadge,
        Github,
        GithubFull,
        Java,
        Ruby,
        Scala,
        Python,
        Go,
        RubyOnRails,
        Django,
        Markdown,
        PHP,
        mySQL,
        Streamline,
        Database,
        Laravel,
        JavaScript,
        Angular,
        Backbone,
        Coffeescript,
        jQuery,
        Modernizr,
        jQueryUI,
        Ember,
        Dojo,
        NodeJs,
        NodeJsSmall,
        JavaScriptShield,
        Bootstrap,
        SASS,
        CSS3full,
        CSS3,
        HTML5,
        HTML5Multimedia,
        HTML5DeviceAccess,
        HTML53dEffects,
        HTML5Connectivity,
        GhostSmall,
        Ghost,
        Magento,
        Joomla,
        JekyllSmall,
        Drupal,
        Wordpress,
        Grunt,
        Bower,
        Npm,
        YahooSmall,
        Yahoo,
        BingSmall,
        Windows,
        Linux,
        Ubuntu,
        Android,
        Apple,
        Appstore,
        Phonegap,
        Blackberry,
        Stackoverflow,
        Techcrunch,
        Codrops,
        CssTricks,
        SmashingMagazine,
        Netmagazine,
        Codepen,
        Cssdeck,
        Hackernews,
        Dropbox,
        GoogleDrive,
        VisualStudio,
        UnitySmall,
        RaspberryPi,
        Chrome,
        IE,
        Firefox,
        Opera,
        Safari,
        Swift,
        Symfony,
        SymfonyBadge,
        Less,
        Stylus,
        Trello,
        Atlassian,
        Jira,
        Envato,
        SnapSvg,
        Raphael,
        GoogleAnalytics,
        Compass,
        OneDrive,
        Gulp,
        Atom,
        Cisco,
        Nancy,
        Clojure,
        ClojureAlt,
        Perl,
        Celluloid,
        W3C,
        Redis,
        Postgresql,
        Webplatform,
        Jenkins,
        RequireJS,
        Opensource,
        Typo3,
        UIkit,
        Doctrine,
        Groovy,
        Nginx,
        Haskell,
        Zend,
        Gnu,
        Yeoman,
        Heroku,
        Debian,
        Travis,
        Dotnet,
        Codeigniter,
        JavaScriptBadge,
        Yii,
        MsSqlServer,
        Composer,
        KrakenJsBadge,
        KrakenJS,
        Mozilla,
        Firebase,
        SizzleJS,
        CreativeCommons,
        CreativeCommonsBadge,
        Mitlicence,
        Senchatouch,
        Bugsense,
        ExtJS,
        MootoolsBadge,
        Mootools,
        RubyRough,
        Komodo,
        Coda,
        Bintray,
        Terminal,
        Code,
        Responsive,
        Dart,
        Aptana,
        Mailchimp,
        Netbeans,
        Dreamweaver,
        Brackets,
        Eclipse,
        Cloud9,
        Scrum,
        Prolog,
        TerminalBadge,
        CodeBadge,
        Mongodb,
        Meteor,
        Meteorfull,
        Fsharp,
        Rust,
        Ionic,
        Sublime,
        Appcelerator,
        Asterisk,
        AWS,
        DigitalOcean,
        Dlang,
        Docker,
        Erlang,
        GoogleCloudPlatform,
        Grails,
        Illustrator,
        Intellij,
        Materializecss,
        Openshift,
        Photoshop,
        Rackspace,
        React,
        Redhat,
        Scriptcs,
        SQLlite,
        VIM
    }

    /// <summary>
    /// Supported Friconix shapes
    /// </summary>
    public enum FriconixShapes
    {
        None,
        Triangle,
        Equilateral,
        Circle,
        Square,
        Hexagon,
        Octagon
    }

    /// <summary>
    /// Supported thickness types of Friconix
    /// </summary>
    public enum FriconixThickness
    {
        None,
        Thin,
        Normal,
        Wide
    }

    /// <summary>
    /// Supported Friconix styles
    /// </summary>
    public enum FriconixStyle
    {
        None,
        Line,
        Solid,
        Prohibited
    }

    /// <summary>
    /// Supported Friconix directions
    /// </summary>
    public enum FriconixDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// Supported effects of Friconix
    /// </summary>
    public enum FriconixEffect
    {
        None,
        Horizontal,
        Vertical,
        Spin,
        Pulse
    }

    /// <summary>
    /// Supported FontAwesome icons
    /// </summary>
    public enum FontAwesomeIcons
    {
        None,
        Add,
        AddCircle,
        AddCircleFull,
        AddFull,
        AddressBook,
        AddressBookFull,
        AddressCard,
        AddressCardFull,
        AmazonPay,
        AmazonPayIcon,
        AmericanExpress,
        AngleDown,
        AngleLeft,
        AngleRight,
        AngleUp,
        ApplePay,
        ApplePayIcon,
        ArrowDown,
        ArrowLeft,
        ArrowRight,
        ArrowUp,
        Bars,
        Building,
        BuildingFull,
        Bullhorn,
        Calendar,
        CalendarFull,
        Check,
        CheckCircle,
        CheckCircleFull,
        CheckSquare,
        CheckSquareFull,
        Close,
        CloseCircle,
        CloseCircleFull,
        CloseFull,
        Code,
        CodeLaptop,
        Comment,
        CommentFull,
        Comments,
        CommentsFull,
        Copy,
        CopyFull,
        Database,
        DeleteMinus,
        DeleteMinusCircleFull,
        DeleteMinusSquare,
        DeleteMinusSquareFull,
        DeleteTrash,
        DeleteTrashBold,
        DeleteTrashFull,
        DeleteUser,
        DeleteUserMinus,
        Diagram,
        Doc,
        DocCode,
        DocImage,
        DocPDF,
        Download,
        DownloadCloud,
        Edit,
        EditFull,
        Envelope,
        EnvelopeFull,
        EnvelopeOpen,
        EnvelopeOpenFull,
        FacebookLogo,
        FacebookLogoCircle,
        FacebookLogoSquare,
        FileArchive,
        FileArchiveFull,
        FileDownload,
        FileExport,
        FileUpload,
        Folder,
        FolderFull,
        FolderMinusFull,
        FolderOpen,
        FolderOpenFull,
        FolderPlusFull,
        GitHub,
        Globe,
        GoogleDrive,
        GoogleIcon,
        GooglePlay,
        GoogleWallet,
        Handshake,
        HandshakeFull,
        Hashtag,
        Home,
        HomeDamage,
        Info,
        InfoCircle,
        InstagramIcon,
        InstagramSquare,
        Key,
        LaptopCode,
        Like,
        LikeFull,
        LinkedInLogo,
        LinkedInLogoSquare,
        Mastercard,
        Messenger,
        Mobile,
        MobileFull,
        MoneyCheckout,
        Pager,
        PayPal,
        PayPalIcon,
        Percentage,
        Phone,
        PhoneFull,
        PhotoVideo,
        Save,
        SaveFull,
        Search,
        SearchDollar,
        SearchLocation,
        SearchMinus,
        SearchPlus,
        Shield,
        ShoppingBag,
        ShoppingBasket,
        ShoppingCart,
        ShoppingCartPlus,
        SignIn,
        SignOut,
        Smile,
        SmileFull,
        SmileLaugh,
        SmileLaughFull,
        SmileWink,
        SmileWinkFull,
        Stripe,
        Swatchbook,
        Tag,
        Tags,
        ThumbsDown,
        ThumbsDownFull,
        Thumbstick,
        ThumbsUp,
        ThumbsUpFull,
        Times,
        Truck,
        Upload,
        UploadCloud,
        User,
        UserCircle,
        UserCircleFull,
        UserFriends,
        UserFull,
        UserLock,
        UserNinja,
        UserPlusFull,
        Users,
        UserShield,
        UserSpy,
        UserTie,
        Visa,
        X,
        PaintBrush,
        PaintBrushWide,
        PaintRoller,
        Palette
    }
    #endregion
}
