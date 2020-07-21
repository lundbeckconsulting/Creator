/*
    @Date			              : 24.06.2020
    @Author                       : Stein Lundbeck
    @Description                  : Represents the different size strings for FontAwesome and Friconix
*/

namespace Creator.TagHelpers.Icon
{
    internal interface IIconSizeRecord
    {
        /// <summary>
        /// The icon size
        /// </summary>
        IconSizes Size { get; }

        /// <summary>
        /// FontAwesome size string
        /// </summary>
        string FontAwesomeSize { get; }

        /// <summary>
        /// Froconix size string
        /// </summary>
        string FriconixSize { get; }

        /// <summary>
        /// Captain Icon size string
        /// </summary>
        string CaptainIconSize { get; }
    }

    internal sealed class IconSizeRecord : IIconSizeRecord
    {
        /// <summary>
        /// Creates a new size record with values for FontAwesome and Friconix
        /// </summary>
        /// <param name="size">The size</param>
        /// <param name="fontAwesomeSize">FontAwesome size string</param>
        /// <param name="friconixSize">Friconix size string</param>
        /// <param name="captainIconSize">Captain Icon size string</param>
        public IconSizeRecord(IconSizes size, string fontAwesomeSize, string friconixSize, string captainIconSize)
        {
            this.Size = size;
            this.FontAwesomeSize = fontAwesomeSize;
            this.FriconixSize = friconixSize;
            this.CaptainIconSize = captainIconSize;
        }

        public IconSizes Size { get; }
        public string FontAwesomeSize { get; }
        public string FriconixSize { get; }
        public string CaptainIconSize { get; }
    }
}
