using RtwFileIO;
using System;

namespace Model
{

public static class EnumExtensions
{
	public static string ToRtwString (this Season season)
	{
		return season switch
		{
			Season.Summer => Keywords.SeasonSummer,
			Season.Winter => Keywords.SeasonWinter,
			_ => throw new ArgumentOutOfRangeException(nameof(season), season, null)
		};
	}
	
	public static string ToRtwString (this CharacterType type)
	{
		return type switch
		{
			CharacterType.NamedCharacter => Keywords.NamedCharacterSpace,
			CharacterType.General => Keywords.General,
			CharacterType.Admiral => Keywords.Admiral,
			CharacterType.Spy => Keywords.Spy,
			CharacterType.Diplomat => Keywords.Diplomat,
			CharacterType.Assassin => Keywords.Assassin,
			CharacterType.Merchant => Keywords.Merchant,
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
	
	public static string ToRtwString (this Noble.Rank rank)
	{
		return rank switch
		{
			Noble.Rank.Leader => Keywords.Leader,
			Noble.Rank.Heir => Keywords.Heir,
			Noble.Rank.None => "None",
			_ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
		};
	}
	
	public static string ToRtwString (this Gender gender)
	{
		return gender switch
		{
			Gender.male => Keywords.GenderMale,
			Gender.female => Keywords.GenderFemale,
			_ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
		};
	}

	public static string ToRtwString (this SettlementLevel level)
	{
		return level switch
		{
			SettlementLevel.Village => Keywords.Village,
			SettlementLevel.Town => Keywords.Town,
			SettlementLevel.LargeTown => Keywords.LargeTown,
			SettlementLevel.City => Keywords.City,
			SettlementLevel.LargeCity => Keywords.LargeCity,
			SettlementLevel.HugeCity => Keywords.HugeCity,
			_ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
		};
	}

	public static string ToRtwString (this BinaryOperator op)
	{
		return op switch
		{
			BinaryOperator.And => Keywords.And,
			BinaryOperator.Or => Keywords.Or,
			BinaryOperator.None => "",
			_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
		};
	}
	
	public static string ToRtwString (this DiplomaticStance stance)
	{
		return stance switch
		{
			DiplomaticStance.Allied => Keywords.Allied,
			DiplomaticStance.Protector => Keywords.Protector,
			DiplomaticStance.Protectorate => Keywords.Protectorate,
			DiplomaticStance.SameSuperFaction => Keywords.SameSuperFaction,
			DiplomaticStance.AtWar => Keywords.AtWar,
			_ => throw new ArgumentOutOfRangeException(nameof(stance), stance, null)
		};
	}
	
	public static string ToRtwString (this ComparisonOperator op)
	{
		return op switch
		{
			ComparisonOperator.GreaterThan => Keywords.GreaterThan,
			ComparisonOperator.LessThan => Keywords.LessThan,
			ComparisonOperator.GreaterOrEqualTo => Keywords.GreaterOrEqualTo,
			ComparisonOperator.LessOrEqualTo => Keywords.LessOrEqualTo,
			_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
		};
	}
}

}