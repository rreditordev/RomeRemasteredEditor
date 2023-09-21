using Model;
using System;
using System.Runtime.CompilerServices;

namespace RtwFileIO
{

public static partial class RtwReaderUtils
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BinaryOperator OperatorParse (string binaryOperator)
	{
		if (string.IsNullOrEmpty(binaryOperator))
		{
			return BinaryOperator.None;
		}
		
		return binaryOperator switch
		{
			Keywords.And => BinaryOperator.And,
			Keywords.Or => BinaryOperator.Or,
			_ => throw new ArgumentException($"Failed to convert text \"{binaryOperator}\" to enum BinaryOperator.")
		};
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DiplomaticStance DiplomaticStanceParse (string diplomaticStance)
	{
		return diplomaticStance switch
		{
			Keywords.Allied => DiplomaticStance.Allied,
			Keywords.Protector => DiplomaticStance.Protector,
			Keywords.Protectorate => DiplomaticStance.Protectorate,
			Keywords.SameSuperFaction => DiplomaticStance.SameSuperFaction,
			Keywords.AtWar => DiplomaticStance.AtWar,
			_ => throw new ArgumentException($"Failed to convert text \"{diplomaticStance}\" to enum DiplomaticStance.")
		};
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ComparisonOperator ComparisonOperatorParse (string comparisonOperator)
	{
		return comparisonOperator switch
		{
			">" => ComparisonOperator.GreaterThan,
			"<" => ComparisonOperator.LessThan,
			">=" => ComparisonOperator.GreaterOrEqualTo,
			"<=" => ComparisonOperator.LessOrEqualTo,
			_ => throw new ArgumentException($"Failed to convert text \"{comparisonOperator}\" to enum ComparisonOperator.")
		};
	}
}

}