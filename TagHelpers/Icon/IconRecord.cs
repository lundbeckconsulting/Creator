/*
    @Date			              : 23.06.2020
    @Author                       : Stein Lundbeck
*/

namespace Creator.TagHelpers.Icon
{
    internal interface IIconRecord
    {
        /// <summary>
        /// Icon name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Prefix for FontAwesome icons
        /// </summary>
        string FontAwesomePrefix { get; set; }

        /// <summary>
        /// Code for DevIcon icons
        /// </summary>
        string DevIconCode { get; set; }

        /// <summary>
        /// Friconix icon
        /// </summary>
        FriconixIcons FriconixIcon { get; set; }

        /// <summary>
        /// FontAwesome icon
        /// </summary>
        FontAwesomeIcons FontAwesomeIcon { get; set; }

        /// <summary>
        /// Captain Icon icon
        /// </summary>
        CaptainIcons CaptainIcon { get; set; }

        /// <summary>
        /// DevIcon icon
        /// </summary>
        DevIcons DevIcon { get; set; }
    }

    /// <summary>
    /// Contains information about either a FontAwesome or Friconix icon
    /// </summary>
    internal sealed class IconRecord : IIconRecord
    {
        /// <summary>
        /// Creates a new FontAwesome record
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <param name="name">Name of icon</param>
        /// <param name="fontAwesomePrefix">Icon prefix</param>
        public IconRecord(FontAwesomeIcons icon, string name, string fontAwesomePrefix = "fas")
        {
            this.Name = name;
            this.FontAwesomePrefix = fontAwesomePrefix;
            this.FontAwesomeIcon = icon;
        }

        /// <summary>
        /// Creates a new Friconix record
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <param name="name">Name of icon</param>
        public IconRecord(FriconixIcons icon, string name)
        {
            this.Name = name;
            this.FriconixIcon = icon;
        }

        /// <summary>
        /// Createa a new Captain Icon record
        /// </summary>
        /// <param name="icon">Type of record</param>
        /// <param name="code">Icon code</param>
        public IconRecord(CaptainIcons icon, string code)
        {
            this.Name = code;
            this.CaptainIcon = icon;
        }

        /// <summary>
        /// Creates a new DevIcon record
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <param name="name">Name of the icon</param>
        /// <param name="code">Icon code</param>
        public IconRecord(DevIcons icon, string name, string code)
        {
            this.DevIcon = icon;
            this.Name = name;
            this.DevIconCode = code;
        }

        public string Name { get; set; }
        public string FontAwesomePrefix { get; set; } = "fas";
        public string DevIconCode { get; set; }
        public FriconixIcons FriconixIcon { get; set; } = FriconixIcons.None;
        public FontAwesomeIcons FontAwesomeIcon { get; set; } = FontAwesomeIcons.None;
        public CaptainIcons CaptainIcon { get; set; } = CaptainIcons.None;
        public DevIcons DevIcon { get; set; } = DevIcons.None;
    }
}
