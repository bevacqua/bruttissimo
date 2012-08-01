namespace Bruttissimo.Domain
{
	public enum LinkParseResult
	{
		/// <summary>
		/// The link may or may not exist, but it's not posted yet.
		/// </summary>
		Valid,
		/// <summary>
		/// The link already exists, and it's posted.
		/// </summary>
		Used,
		/// <summary>
		/// No link was provided or the provided link produced no result.
		/// </summary>
		Invalid
	}
}