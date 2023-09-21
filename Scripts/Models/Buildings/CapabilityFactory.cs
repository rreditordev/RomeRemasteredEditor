using RtwFileIO;
using System;

namespace Model
{

public static class CapabilityFactory
{
	public static Capability CreateCapability (BuildingCapability definition)
	{
		string[] split = definition.Capability.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		string capabilityKeyword = split[0].Trim();
		return capabilityKeyword switch
		{
			Keywords.TaxBonus => ParseTaxBonus(ref definition),
			Keywords.TradeBaseBonus => ParseTradeBaseBonus(ref definition),
			Keywords.TradeLandBonus => ParseTradeLandBonus(ref definition),
			Keywords.TradeFleet => ParseTradeFleet(ref definition),
			Keywords.MineResource => ParseMineResource(ref definition),
			Keywords.FarmingLevel => ParseFarmingLevel(ref definition),
			Keywords.RoadLevel => ParseRoadLevel(ref definition),
			Keywords.ConstructionCostBonusMilitary => ParseConstructionCostBonusMilitary(ref definition),
			Keywords.ConstructionCostBonusReligious => ParseConstructionCostBonusReligious(ref definition),
			Keywords.ConstructionCostBonusDefensive => ParseConstructionCostBonusDefensive(ref definition),
			Keywords.ConstructionCostBonusOther => ParseConstructionCostBonusOther(ref definition),
			Keywords.ConstructionTimeBonusMilitary => ParseConstructionTimeBonusMilitary(ref definition),
			Keywords.ConstructionTimeBonusReligious => ParseConstructionTimeBonusReligious(ref definition),
			Keywords.ConstructionTimeBonusDefensive => ParseConstructionTimeBonusDefensive(ref definition),
			Keywords.ConstructionTimeBonusOther => ParseConstructionTimeBonusOther(ref definition),
			Keywords.ExtraRecruitmentPoints => ParseExtraRecruitmentPoints(ref definition),
			Keywords.ExtraConstructionPoints => ParseExtraConstructionPoints(ref definition),
			Keywords.SettlementAgentLimit => ParseSettlementAgentLimit(ref definition),
			Keywords.ReligiousOrder => ParseReligiousOrder(ref definition),
			Keywords.HappinessBonus => ParseHappinessBonus(ref definition),
			Keywords.LawBonus => ParseLawBonus(ref definition),
			Keywords.PopGrowthBonus => ParsePopGrowthBonus(ref definition),
			Keywords.PopHealthBonus => ParsePopHealthBonus(ref definition),
			Keywords.StageGames => ParseStageGames(ref definition),
			Keywords.StageRaces => ParseStageRaces(ref definition),
			Keywords.ReligiousConversion => ParseReligiousConversion(ref definition),
			Keywords.Recruitment => ParseRecruitment(ref definition),
			Keywords.Agent => ParseAgent(ref definition),
			Keywords.ArmourUpgrade => ParseArmourUpgrade(ref definition),
			Keywords.WeaponLightUpgrade => ParseWeaponLightUpgrade(ref definition),
			Keywords.WeaponHeavyUpgrade => ParseWeaponHeavyUpgrade(ref definition),
			Keywords.WeaponMissileUpgrade => ParseWeaponMissileUpgrade(ref definition),
			Keywords.WeaponOtherUpgrade => ParseWeaponOtherUpgrade(ref definition),
			Keywords.WallLevel => ParseWallLevel(ref definition),
			Keywords.GateStrength => ParseGateStrength(ref definition),
			Keywords.GateDefenses => ParseGateDefenses(ref definition),
			Keywords.TowerLevel => ParseTowerLevel(ref definition),
			Keywords.RecruitsMoraleBonus => ParseRecruitsMoraleBonus(ref definition),
			Keywords.RecruitsExperienceBonus => ParseRecruitsExperienceBonus(ref definition),
			_ => throw new ArgumentException($"Capability keyword \"{capabilityKeyword}\" not recognized")
		};
	}

	static Capability ParseTaxBonus (ref BuildingCapability definition)
	{
		int taxBonus = ParseBonus(Keywords.TaxBonus, definition.Capability, out bool isBonus);
		return new TaxBonusCapability(definition.Requirement, isBonus, taxBonus);
	}
	
	static Capability ParseTradeBaseBonus (ref BuildingCapability definition)
	{
		int tradeBonus = ParseBonus(Keywords.TradeBaseBonus, definition.Capability, out bool isBonus);
		return new TradeBaseBonusCapability(definition.Requirement, isBonus, tradeBonus);
	}
	
	static Capability ParseTradeLandBonus (ref BuildingCapability definition)
	{
		int tradeBonus = ParseBonus(Keywords.TradeLandBonus, definition.Capability, out bool isBonus);
		return new TradeLandBonusCapability(definition.Requirement, isBonus, tradeBonus);
	}

	static Capability ParseTradeFleet (ref BuildingCapability definition)
	{
		int tradeFleets = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.TradeFleet.Length).Trim());
		return new TradeFleetCapability(definition.Requirement, tradeFleets);
	}
	
	static Capability ParseMineResource (ref BuildingCapability definition)
	{
		int miningIncomeLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.MineResource.Length).Trim());
		return new MineResourceCapability(definition.Requirement, miningIncomeLevel);
	}
	
	static Capability ParseFarmingLevel (ref BuildingCapability definition)
	{
		int farmingLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.FarmingLevel.Length).Trim());
		return new FarmingLevelCapability(definition.Requirement, farmingLevel);
	}
	
	static Capability ParseRoadLevel (ref BuildingCapability definition)
	{
		int roadLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.RoadLevel.Length).Trim());
		return new RoadLevelCapability(definition.Requirement, roadLevel);
	}
	
	static Capability ParseConstructionCostBonusMilitary (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionCostBonusMilitary.Length).Trim());
		return new ConstructionCostBonusMilitary(definition.Requirement, costReduction);
	}
	
	static Capability ParseConstructionCostBonusReligious (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionCostBonusReligious.Length).Trim());
		return new ConstructionCostBonusReligious(definition.Requirement, costReduction);
	}
	
	static Capability ParseConstructionCostBonusDefensive (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionCostBonusDefensive.Length).Trim());
		return new ConstructionCostBonusDefensive(definition.Requirement, costReduction);
	}
	
	static Capability ParseConstructionCostBonusOther (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionCostBonusOther.Length).Trim());
		return new ConstructionCostBonusOther(definition.Requirement, costReduction);
	}
	
	static Capability ParseConstructionTimeBonusMilitary (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionTimeBonusMilitary.Length).Trim());
		return new ConstructionTimeBonusMilitary(definition.Requirement, costReduction);
	}
	
	static Capability ParseConstructionTimeBonusReligious (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionTimeBonusReligious.Length).Trim());
		return new ConstructionTimeBonusReligious(definition.Requirement, costReduction);
	}
	
	static Capability ParseConstructionTimeBonusDefensive (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionTimeBonusDefensive.Length).Trim());
		return new ConstructionTimeBonusDefensive(definition.Requirement, costReduction);
	}
	
	static Capability ParseConstructionTimeBonusOther (ref BuildingCapability definition)
	{
		int costReduction = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ConstructionTimeBonusOther.Length).Trim());
		return new ConstructionTimeBonusOther(definition.Requirement, costReduction);
	}
	
	static Capability ParseExtraRecruitmentPoints (ref BuildingCapability definition)
	{
		int extraRecruitmentPoints = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ExtraRecruitmentPoints.Length).Trim());
		return new ExtraRecruitmentPoints(definition.Requirement, extraRecruitmentPoints);
	}
	
	static Capability ParseExtraConstructionPoints (ref BuildingCapability definition)
	{
		int extraConstructionPoints = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ExtraConstructionPoints.Length).Trim());
		return new ExtraConstructionPoints(definition.Requirement, extraConstructionPoints);
	}
	
	static Capability ParseSettlementAgentLimit (ref BuildingCapability definition)
	{
		string[] line = definition.Capability.Remove(0, Keywords.SettlementAgentLimit.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		
		CharacterType agentType = RtwReaderUtils.CharacterTypeParse(line[0].Trim());
		int limit = RtwReaderUtils.IntParse(line[1].Trim());
		return new SettlementAgentLimit(definition.Requirement, agentType, limit);
	}
	
	static Capability ParseReligiousOrder (ref BuildingCapability definition)
	{
		int order = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.ReligiousOrder.Length).Trim());
		return new ReligiousOrder(definition.Requirement, order);
	}
	
	static Capability ParseHappinessBonus (ref BuildingCapability definition)
	{
		int happinessBonus = ParseBonus(Keywords.HappinessBonus, definition.Capability, out bool isBonus);
		return new HappinessBonus(definition.Requirement, isBonus, happinessBonus);
	}
	
	static Capability ParseLawBonus (ref BuildingCapability definition)
	{
		int lawBonus = ParseBonus(Keywords.LawBonus, definition.Capability, out bool isBonus);
		return new LawBonus(definition.Requirement, isBonus, lawBonus);
	}
	
	static Capability ParsePopGrowthBonus (ref BuildingCapability definition)
	{
		int popGrowthBonus = ParseBonus(Keywords.PopGrowthBonus, definition.Capability, out bool isBonus);
		return new PopGrowthBonus(definition.Requirement, isBonus, popGrowthBonus);
	}
	
	static Capability ParsePopHealthBonus (ref BuildingCapability definition)
	{
		int popHealthBonus = ParseBonus(Keywords.PopHealthBonus, definition.Capability, out bool isBonus);
		return new PopHealthBonus(definition.Requirement, isBonus, popHealthBonus);
	}
	
	static Capability ParseStageGames (ref BuildingCapability definition)
	{
		int gamesLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.StageGames.Length).Trim());
		return new StageGames(definition.Requirement, gamesLevel);
	}
	
	static Capability ParseStageRaces (ref BuildingCapability definition)
	{
		int racesLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.StageRaces.Length).Trim());
		return new StageRaces(definition.Requirement, racesLevel);
	}
	
	static Capability ParseReligiousConversion (ref BuildingCapability definition)
	{
		string[] split = definition.Capability.Remove(0, Keywords.ReligiousConversion.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		
		string religionID = split[0].Trim();
		int conversionStrength = RtwReaderUtils.IntParse(split[1].Trim());
		return new ReligiousConversion(definition.Requirement, religionID, conversionStrength);
	}
	
	static Capability ParseRecruitment (ref BuildingCapability definition)
	{
		int unitIDStart = definition.Capability.IndexOf("\"", StringComparison.Ordinal);
		int unitIDEnd = definition.Capability.LastIndexOf("\"", StringComparison.Ordinal);
		
		string unitID = definition.Capability.Substring(unitIDStart + 1, unitIDEnd - unitIDStart - 1);
		int experience = RtwReaderUtils.IntParse(definition.Capability.Substring(unitIDEnd + 1).Trim());
		return new Recruitment(definition.Requirement, unitID, experience);
	}
	
	static Capability ParseAgent (ref BuildingCapability definition)
	{
		string[] split = definition.Capability.Remove(0, Keywords.Agent.Length).Trim()
			.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);

		CharacterType agentType = RtwReaderUtils.CharacterTypeParse(split[0].Trim());
		int experience = RtwReaderUtils.IntParse(split[1].Trim());
		return new Agent(definition.Requirement, agentType, experience);
	}
	
	static Capability ParseArmourUpgrade (ref BuildingCapability definition)
	{
		int armourValue = ParseBonus(Keywords.ArmourUpgrade, definition.Capability, out bool isBonus);
		return new ArmourUpgrade(definition.Requirement, isBonus, armourValue);
	}
	
	static Capability ParseWeaponLightUpgrade (ref BuildingCapability definition)
	{
		int upgradeLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.WeaponLightUpgrade.Length).Trim());
		return new WeaponLightUpgrade(definition.Requirement, upgradeLevel);
	}
	
	static Capability ParseWeaponHeavyUpgrade (ref BuildingCapability definition)
	{
		int upgradeLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.WeaponHeavyUpgrade.Length).Trim());
		return new WeaponHeavyUpgrade(definition.Requirement, upgradeLevel);
	}
	
	static Capability ParseWeaponMissileUpgrade (ref BuildingCapability definition)
	{
		int upgradeLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.WeaponMissileUpgrade.Length).Trim());
		return new WeaponMissileUpgrade(definition.Requirement, upgradeLevel);
	}
	
	static Capability ParseWeaponOtherUpgrade (ref BuildingCapability definition)
	{
		int upgradeLevel = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.WeaponOtherUpgrade.Length).Trim());
		return new WeaponOtherUpgrade(definition.Requirement, upgradeLevel);
	}
	
	static Capability ParseWallLevel (ref BuildingCapability definition)
	{
		int level = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.WallLevel.Length).Trim());
		return new WallLevel(definition.Requirement, level);
	}

	static Capability ParseGateStrength (ref BuildingCapability definition)
	{
		int level = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.GateStrength.Length).Trim());
		return new GateStrength(definition.Requirement, level);
	}

	static Capability ParseGateDefenses (ref BuildingCapability definition)
	{
		int level = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.GateDefenses.Length).Trim());
		return new GateDefenses(definition.Requirement, level);
	}

	static Capability ParseTowerLevel (ref BuildingCapability definition)
	{
		int level = RtwReaderUtils.IntParse(definition.Capability.Remove(0, Keywords.TowerLevel.Length).Trim());
		return new TowerLevel(definition.Requirement, level);
	}
	
	static Capability ParseRecruitsMoraleBonus (ref BuildingCapability definition)
	{
		int bonus = ParseBonus(Keywords.RecruitsMoraleBonus, definition.Capability, out bool isBonus);
		return new RecruitsMoraleBonus(definition.Requirement, isBonus, bonus);
	}
	
	static Capability ParseRecruitsExperienceBonus (ref BuildingCapability definition)
	{
		int bonus = ParseBonus(Keywords.RecruitsExperienceBonus, definition.Capability, out bool isBonus);
		return new RecruitsExperienceBonus(definition.Requirement, isBonus, bonus);
	}
	
	static int ParseBonus (string key, string capability, out bool isBonus)
	{
		string[] split = capability.Remove(0, key.Length).Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		if (split.Length > 1)
		{
			isBonus = true;
			return RtwReaderUtils.IntParse(split[1].Trim());
		}
		isBonus = false;
		return RtwReaderUtils.IntParse(split[0].Trim());
	}
}

}