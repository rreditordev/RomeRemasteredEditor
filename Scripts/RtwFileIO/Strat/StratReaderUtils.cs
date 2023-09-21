using Model;
using System;
using System.Runtime.CompilerServices;

namespace RtwFileIO
{

public static partial class RtwReaderUtils
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Season SeasonParse (string season)
	{
		return season switch
		{
			Keywords.SeasonSummer => Season.Summer,
			Keywords.SeasonWinter => Season.Winter,
			_ => throw new ArgumentException($"Failed to convert text \"{season}\" to enum Season.")
		};
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CharacterType CharacterTypeParse (string characterType)
	{
		return characterType switch
		{
			Keywords.NamedCharacterSpace => CharacterType.NamedCharacter,
			Keywords.NamedCharacterUnderscore => CharacterType.NamedCharacter,
			Keywords.General => CharacterType.General,
			Keywords.Admiral => CharacterType.Admiral,
			Keywords.Spy => CharacterType.Spy,
			Keywords.Diplomat => CharacterType.Diplomat,
			Keywords.Assassin => CharacterType.Assassin,
			Keywords.Merchant => CharacterType.Merchant,
			_ => throw new ArgumentException($"Failed to convert text \"{characterType}\" to enum CharacterType.")
		};
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Noble.Rank NobleRankParse (string rank)
	{
		return rank switch
		{
			Keywords.Heir => Noble.Rank.Heir,
			Keywords.Leader => Noble.Rank.Leader,
			_ => throw new ArgumentException($"Failed to convert text \"{rank}\" to enum Noble.Rank.")
		};
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static SettlementLevel SettlementLevelParse (string settlementLevel)
	{
		return settlementLevel switch
		{
			Keywords.Village => SettlementLevel.Village,
			Keywords.Town => SettlementLevel.Town,
			Keywords.LargeTown => SettlementLevel.LargeTown,
			Keywords.City => SettlementLevel.City,
			Keywords.LargeCity => SettlementLevel.LargeCity,
			Keywords.HugeCity => SettlementLevel.HugeCity,
			_ => throw new ArgumentException($"Failed to convert text \"{settlementLevel}\" to enum SettlementLevel.")
		};
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Gender GenderParse (string gender)
	{
		gender = gender.ToLower();
		return gender switch
		{
			Keywords.GenderMale => Gender.male,
			Keywords.GenderFemale => Gender.female,
			_ => throw new ArgumentException($"Failed to convert text \"{gender}\" to enum Gender.")
		};
	}
}

}