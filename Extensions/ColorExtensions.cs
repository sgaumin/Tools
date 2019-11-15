using UnityEngine;

public static class ColorExtensions
{
	/// <summary>
	/// Create a new color from the given color and a new value for its alpha.
	/// </summary>
	/// <param name="c">The source color</param>
	/// <param name="a">The new alpha value</param>
	/// <returns>A new color {c.rgb, a}</returns>
	public static Color WithAlpha(this Color c, float a)
	{
		c.a = a;
		return c;
	}

	/// <summary>
	/// Create a new color from the given color with its alpha multiplied.
	/// </summary>
	/// <param name="c">The source color</param>
	/// <param name="a">A factor to multiply the alpha value with</param>
	/// <returns>A new color {c.rgb, a}</returns>
	public static Color WithAlphaMultiplied(this Color c, float a)
	{
		c.a *= a;
		return c;
	}
}
