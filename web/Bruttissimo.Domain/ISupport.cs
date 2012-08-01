namespace Bruttissimo.Domain
{
	/// <summary>
	/// Interface used to decorate Domain Logic support classes, these are scoped to
	/// <para>tasks that are not directly interacting with repositories (even though they may depend on them),</para>
	/// <para>but are not intended to be directly consumed by something other than Domain Logic services.</para>
	/// </summary>
	public interface ISupport
	{
	}
}