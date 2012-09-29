using System.Text.RegularExpressions;
using Bruttissimo.Common.Resources;
using Regular = Bruttissimo.Common.Resources.Shared.Regular;

namespace Bruttissimo.Common.Static
{
    /// <summary>
    /// Holds a collection of regular expressions compiled for faster execution.
    /// </summary>
    public static class CompiledRegex
    {
        private const RegexOptions compiled = RegexOptions.Compiled | RegexOptions.IgnoreCase;
        private const RegexOptions html = RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled;

        public static readonly Regex WebLink = new Regex(Regular.WebLink, compiled);
        public static readonly Regex WwwSubdomain = new Regex(Regular.WwwSubdomain, compiled);

        public static readonly Regex HtmlTag = new Regex(Html.Tag, html);
        public static readonly Regex HtmlSafeTag = new Regex(Html.WhitelistedTag, html);
        public static readonly Regex HtmlSafeAnchorTag = new Regex(Html.WhitelistedAnchor, html);
        public static readonly Regex HtmlSafeImageTag = new Regex(Html.WhitelistedImage, html);

        public static readonly Regex JavaScriptViewNamingConvention = new Regex(Resources.Regular.JavaScriptViewNamingConvention, compiled);

        public static readonly Regex DistinctLineBreaks = new Regex(Resources.Regular.DistinctLineBreaks, compiled);
    }
}
