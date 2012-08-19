using System;

namespace Bruttissimo.Domain
{
    /// <summary>
    /// Picture entity size
    /// </summary>
    [Flags]
    public enum PictureSize
    {
        None = 0x0,

        /// <summary>
        /// 720px is the maximum width or height for this image type.
        /// </summary>
        [PicturePixels(720)] Large = 0x1,

        /// <summary>
        /// 480px is the maximum width or height for this image type.
        /// </summary>
        [PicturePixels(480)] Regular = 0x2,

        /// <summary>
        /// 128px is the maximum width or height for this image type.
        /// </summary>
        [PicturePixels(128)] Thumbnail = 0x4,
        All = 0x7
    }
}
