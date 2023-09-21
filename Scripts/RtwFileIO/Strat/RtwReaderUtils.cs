using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Godot;
using UIUtilities;

namespace RtwFileIO
{

public static partial class RtwReaderUtils
{
	public static readonly char[] Whitespace = {' ', '\t'};
	
	static readonly Regex s_matchNonDigits = new(@"[^\d^-]"); // Match non-digits but don't match minus symbol
	static readonly Regex s_matchNonLetters = new(@"[^A-Za-z_]+");

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int IntParse (string text)
	{
		return int.Parse(s_matchNonDigits.Replace(text, string.Empty));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string NonNumericParse (string text)
	{
		return s_matchNonLetters.Replace(text, string.Empty);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Color32Equals(this Color32 color, Color32 other)
	{
		return color.r == other.r && color.g == other.g && color.b == other.b && color.a == other.a;
	}
}

}