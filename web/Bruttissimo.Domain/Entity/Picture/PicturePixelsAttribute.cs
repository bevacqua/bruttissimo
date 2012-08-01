using System;

namespace Bruttissimo.Domain
{
	/// <summary>
	/// Sets the maximum dimention for both width and height the decorated field supports, in pixels.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class PicturePixelsAttribute : Attribute
	{
		public int Pixels { get; private set; }

		public PicturePixelsAttribute(int pixels)
		{
			Pixels = pixels;
		}
	}
}