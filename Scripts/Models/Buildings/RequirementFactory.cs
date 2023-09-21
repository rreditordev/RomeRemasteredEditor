using RtwFileIO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{

public static class RequirementFactory
{

	public static Requirement CreateRequirement (RequirementDefinition definition)
	{
		string[] split = definition.Condition.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		string requirementKeyword = split[0].Trim();
		return requirementKeyword switch
		{
			Keywords.ResourcePresent => ParseResourceRequirement(ref definition, false),
			Keywords.HiddenResourcePresent => ParseResourceRequirement(ref definition, true),
			Keywords.BuildingPresentLevel => ParseBuildingPresentLevelRequirement(ref definition),
			Keywords.BuildingPresent => ParseBuildingPresentRequirement(ref definition),
			Keywords.MajorEvent => ParseMajorEventRequirement(ref definition),
			Keywords.Factions => ParseFactionsRequirement(ref definition),
			Keywords.Port => new PortRequirement(definition.Negated),
			Keywords.IsPlayer => new IsPlayerRequirement(definition.Negated),
			Keywords.IsToggled => ParseToggleRequirement(ref definition),
			Keywords.Diplomacy => ParseDiplomacyRequirement(ref definition),
			Keywords.BuildingFactions => ParseBuildingFactionsRequirement(ref definition),
			Keywords.SettlementCapability => ParseSettlementCapabilityRequirement(ref definition),
			Keywords.NoBuildingTagged => ParseNoBuildingTaggedRequirement(ref definition),
			Keywords.SettlementReligion => ParseSettlementReligionRequirement(ref definition),
			Keywords.SettlementMajorityReligion => ParseSettlementMajorityReligionRequirement(ref definition),
			Keywords.OfficialReligion => ParseSettlementOfficialReligionRequirement(ref definition),
			_ => new AliasedRequirement(definition.Negated, requirementKeyword)
		};
	}

	static Requirement ParseResourceRequirement (ref RequirementDefinition definition, bool isHiddenResource)
	{
		int removeLength = isHiddenResource ? Keywords.HiddenResourcePresent.Length : Keywords.ResourcePresent.Length;
		string line = definition.Condition.Remove(0, removeLength).Trim();
		string[] split = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		var factionWide = false;
		if (split.Length > 1)
		{
			if (split[1] is Keywords.FactionWide)
			{
				factionWide = true;
			}
		}
		
		return new ResourceRequirement(definition.Negated, split[0], factionWide, isHiddenResource);
	}
	
	static Requirement ParseBuildingPresentRequirement (ref RequirementDefinition definition)
	{
		string[] line = definition.Condition.Remove(0, Keywords.BuildingPresent.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);

		bool isFactionWide = line.Any(entry => entry.Trim() is Keywords.FactionWide);
		bool isQueued = line.Any(entry => entry.Trim() is Keywords.Queued);
		return new BuildingPresentRequirement(definition.Negated, line[0].Trim(), isFactionWide, isQueued);
	}
	
	static Requirement ParseBuildingPresentLevelRequirement (ref RequirementDefinition definition)
	{
		string[] line = definition.Condition.Remove(0, Keywords.BuildingPresentLevel.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);

		bool isFactionWide = line.Any(entry => entry.Trim() is Keywords.FactionWide);
		bool isQueued = line.Any(entry => entry.Trim() is Keywords.Queued);
		return new BuildingPresentLevelRequirement(definition.Negated, line[0].Trim(), line[1].Trim(), isFactionWide, isQueued);
	}
	
	static Requirement ParseMajorEventRequirement (ref RequirementDefinition definition)
	{
		string majorEventID = definition.Condition.Replace("\"", "").Trim().Remove(0, Keywords.MajorEvent.Length).Trim();
		return new MajorEventRequirement(definition.Negated, majorEventID);
	}
	
	static Requirement ParseFactionsRequirement (ref RequirementDefinition definition)
	{
		List<string> factionOrCultureIDs = ParseFactionList(definition.Condition.Remove(0, Keywords.Factions.Length).Trim());
		return new FactionsRequirement(definition.Negated, factionOrCultureIDs);
	}
	
	static Requirement ParseToggleRequirement (ref RequirementDefinition definition)
	{
		string toggleID = definition.Condition.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1]
			.Replace("\"", "").Trim();
		return new ToggleRequirement(definition.Negated, toggleID);
	}
	
	static Requirement ParseDiplomacyRequirement (ref RequirementDefinition definition)
	{
		string[] split = definition.Condition.Remove(0, Keywords.Diplomacy.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);

		if (split.Length < 2)
		{
			throw new ArgumentException("Invalid diplomatic requirement");
		}
		
		DiplomaticStance stance = RtwReaderUtils.DiplomaticStanceParse(split[0].Trim());
		return new DiplomacyRequirement(definition.Negated, stance, split[1].Trim());
	}
	
	static Requirement ParseBuildingFactionsRequirement (ref RequirementDefinition definition)
	{
		List<string> factionOrCultureIDs = ParseFactionList(definition.Condition.Remove(0, Keywords.BuildingFactions.Length).Trim());
		return new BuildingFactionsRequirement(definition.Negated, factionOrCultureIDs);
	}
	
	static Requirement ParseSettlementCapabilityRequirement (ref RequirementDefinition definition)
	{
		string[] split = definition.Condition.Remove(0, Keywords.SettlementCapability.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		if (split.Length < 2)
		{
			throw new ArgumentException("Invalid SettlementCapability requirement");
		}
		return new SettlementCapabilityRequirement(definition.Negated, split[0].Trim(), RtwReaderUtils.IntParse(split[1].Trim()));
	}
	
	static Requirement ParseNoBuildingTaggedRequirement (ref RequirementDefinition definition)
	{
		string[] line = definition.Condition.Remove(0, Keywords.NoBuildingTagged.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		string tag = line[0].Trim();
		bool isFactionWide = line.Any(entry => entry.Trim() is Keywords.FactionWide);
		bool isQueued = line.Any(entry => entry.Trim() is Keywords.Queued);
		return new NoBuildingTaggedRequirement(definition.Negated, tag, isFactionWide, isQueued);
	}
	
	static Requirement ParseSettlementReligionRequirement (ref RequirementDefinition definition)
	{
		string[] line = definition.Condition.Remove(0, Keywords.SettlementReligion.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		string religionID = line[0].Trim();
		ComparisonOperator comparisonOperator = RtwReaderUtils.ComparisonOperatorParse(line[1].Trim());
		int targetReligionInfluence = RtwReaderUtils.IntParse(line[2].Trim());
		return new SettlementReligionRequirement(definition.Negated, religionID, comparisonOperator, targetReligionInfluence);
	}
	
	static Requirement ParseSettlementMajorityReligionRequirement (ref RequirementDefinition definition)
	{
		string religionID = definition.Condition.Remove(0, Keywords.SettlementMajorityReligion.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1];
		return new SettlementMajorityReligionRequirement(definition.Negated, religionID);
	}
	
	static Requirement ParseSettlementOfficialReligionRequirement (ref RequirementDefinition definition)
	{
		string religionID = definition.Condition.Remove(0, Keywords.OfficialReligion.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1];
		return new SettlementOfficialReligionRequirement(definition.Negated, religionID);
	}
	
	static List<string> ParseFactionList (string list)
	{
		List<string> factionOrCultureIDs = new();
		string[] split = list.Replace(Keywords.StructStart, "").Replace(Keywords.StructEnd, "").Trim()
			.Split(",", StringSplitOptions.RemoveEmptyEntries);
		for (var i = 0; i < split.Length; i++)
		{
			factionOrCultureIDs.Add(split[i].Trim());
		}
		return factionOrCultureIDs;
	}
}

}