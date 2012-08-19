using System;
using System.Collections.Generic;

namespace Bruttissimo.Domain.Logic
{
    /// <summary>
    /// <para>Metadata sources</para>
    /// <para>- "og:*" meta properties come from the Open Graph Protocol (http://ogp.me/).</para>
    /// <para>- "itemprop" microdata attributes come from schema.org (http://schema.org/).</para>
    /// </summary>
    public static class XPath
    {
        static XPath()
        {
            _titleXPaths = new Lazy<IEnumerable<string>>(GetTitleXPaths);
            _descriptionXPaths = new Lazy<IEnumerable<string>>(GetXPathForDescriptions);
            _pictureXPaths = new Lazy<IEnumerable<string>>(GeXPathForPictures);
        }

        private static readonly Lazy<IEnumerable<string>> _titleXPaths;

        public static IEnumerable<string> ResourceTitle
        {
            get { return _titleXPaths.Value; }
        }

        private static IEnumerable<string> GetTitleXPaths()
        {
            yield return "/html/head/meta[@property='og:title']";
            yield return "//*[@itemprop='name']";
            yield return "/html/head/meta[@name='title']";
            yield return "/html/head/title";
        }

        private static readonly Lazy<IEnumerable<string>> _descriptionXPaths;

        public static IEnumerable<string> ResourceDescription
        {
            get { return _descriptionXPaths.Value; }
        }

        /// <summary>
        /// Predicts HTML resource description positions pretty reliably.
        /// </summary>
        public const string ResourceDescriptionNodes = "//*[not(self::html or self::head or self::body or self::h1 or self::ul or self::ol)][string-length() > 150]";

        public const string UndesirableHtmlTags = "//script|//style|//object";

        private static IEnumerable<string> GetXPathForDescriptions()
        {
            yield return "/html/head/meta[@property='og:description']";
            yield return "//*[@itemprop='description']";
            yield return ResourceDescriptionNodes;
            yield return "/html/head/meta[@name='description']";
        }

        private static readonly Lazy<IEnumerable<string>> _pictureXPaths;

        /// <summary>
        /// Document Root XPath.
        /// </summary>
        public const string DocumentRoot = "/#document";

        /// <summary>
        /// Matches relevante image nodes.
        /// </summary>
        public const string ImageNode = "//img[@src][not(@width) or @width>50][not(@height) or @height>50]";

        public static IEnumerable<string> ResourcePicture
        {
            get { return _pictureXPaths.Value; }
        }

        private static IEnumerable<string> GeXPathForPictures()
        {
            yield return "/html/head/meta[@property='og:image']";
            yield return "/html/head/link[@rel='image_src']";
            yield return "//*[@itemprop='image']";
            yield return "/html/head/meta[@name='thumbnail']";
        }
    }
}
