using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public abstract class Capability
{
	public RequirementList Requirements => _requirements;
	RequirementList _requirements;

	protected Capability (List<RequirementDefinition> requirements)
	{
		_requirements = new RequirementList(requirements);
	}
}

public abstract class BonusCapability : Capability
{
	public bool IsBonus => _isBonus;
	bool _isBonus;
	
	protected BonusCapability (List<RequirementDefinition> requirements, bool isBonus) : base(requirements)
	{
		_isBonus = isBonus;
	}
}

/**
 * population_growth_bonus: Pop. growth 1-25 (0.5-12.5%)
 */
public class TaxBonusCapability : BonusCapability
{
	public int TaxBonus => _taxBonus;
	int _taxBonus;
	
	public TaxBonusCapability (List<RequirementDefinition> requirements, bool isBonus, int taxBonus) : base(requirements, isBonus) 
	{
		_taxBonus = taxBonus;
	}
}

/**
 * trade_base_income_bonus: Increases trade goods) 1-5 (1-5) - adds 10% to base value of land trade & sea exports
 */
public class TradeBaseBonusCapability : BonusCapability
{
	public int TradeBonus => _tradeBonus;
	int _tradeBonus;
	
	public TradeBaseBonusCapability (List<RequirementDefinition> requirements, bool isBonus, int tradeBonus) : base(requirements, isBonus) 
	{
		_tradeBonus = tradeBonus;
	}
}

/**
 * trade_level_bonus 2.0.4 Feature: Affects land trade only (previously non-functional in original game)
 */
public class TradeLandBonusCapability : BonusCapability
{
	public int TradeBonus => _tradeBonus;
	int _tradeBonus;
	
	public TradeLandBonusCapability (List<RequirementDefinition> requirements, bool isBonus, int tradeBonus) : base(requirements, isBonus) 
	{
		_tradeBonus = tradeBonus;
	}
}

/**
 * adds additional overseas trade routes (trade fleets) 1-3 (1-3)
 */
public class TradeFleetCapability : Capability
{
	public int TradeFleets => _tradeFleets;
	int _tradeFleets;

	public TradeFleetCapability (List<RequirementDefinition> requirements, int tradeFleets) : base(requirements) 
	{
		_tradeFleets = tradeFleets;
	}
}

/**
 * (income from mining) 1-4 (1-4) values above 4 also work and create greater income
 */
public class MineResourceCapability : Capability
{
	public int MiningIncomeLevel => _miningIncomeLevel;
	int _miningIncomeLevel;

	public MineResourceCapability (List<RequirementDefinition> requirements, int miningIncomeLevel) : base(requirements) 
	{
		_miningIncomeLevel = miningIncomeLevel;
	}
}

/**
 * Plus 0.5% pop. growth and plus 80 income (average harvest) per point - equivalent to base farm level in descr_regions.txt) 1-5 (1-5)
 */
public class FarmingLevelCapability : Capability
{
	public int FarmingLevel => _farmingLevel;
	int _farmingLevel;

	public FarmingLevelCapability (List<RequirementDefinition> requirements, int farmingLevel) : base(requirements) 
	{
		_farmingLevel = farmingLevel;
	}
}

/**
 * (improved roads and trade) 0-2
 */
public class RoadLevelCapability : Capability
{
	public int RoadLevel => _roadLevel;
	int _roadLevel;

	public RoadLevelCapability (List<RequirementDefinition> requirements, int roadLevel) : base(requirements) 
	{
		_roadLevel = roadLevel;
	}
}

/**
 * (percentile cost reduction for recruitment buildings) 1-100 (1-100%)
 */
public class ConstructionCostBonusMilitary : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionCostBonusMilitary (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * (percentile cost reduction for temples) 1-100 (1-100%)
 */
public class ConstructionCostBonusReligious : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionCostBonusReligious (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * (percentile cost reduction for walls) 1-100 (1-100%)
 */
public class ConstructionCostBonusDefensive : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionCostBonusDefensive (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * (percentile cost reduction for civil buildings but applies to all buildings except religious ones) 1-100 (1-100%)
 */
public class ConstructionCostBonusOther : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionCostBonusOther (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * (percentile time reduction for constructing recruitment buildings but does not seem to work) 1-100 (1-100%)
 */
public class ConstructionTimeBonusMilitary : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionTimeBonusMilitary (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * (percentile time reduction for constructing temples) 1-100 (1-100%)
 */
public class ConstructionTimeBonusReligious : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionTimeBonusReligious (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * (percentile time reduction for constructing walls) 1-100 (1-100%)
 */
public class ConstructionTimeBonusDefensive : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionTimeBonusDefensive (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * (percentile time reduction for constructing civil buildings but also applies to all buildings except religious ones) 1-100 (1-100%)
 */
public class ConstructionTimeBonusOther : Capability
{
	public int CostReduction => _costReduction;
	int _costReduction;
	
	public ConstructionTimeBonusOther (List<RequirementDefinition> requirements, int costReduction) : base(requirements) 
	{
		_costReduction = costReduction;
	}
}

/**
 * Allows you to give additional recruitment points to a settlement via a building modifier. You can define the bonus in terms of turns.
 */
public class ExtraRecruitmentPoints : Capability
{
	public int ExtraPoints => _extraRecruitmentPoints;
	int _extraRecruitmentPoints;
	
	public ExtraRecruitmentPoints (List<RequirementDefinition> requirements, int extraRecruitmentPoints) : base(requirements) 
	{
		_extraRecruitmentPoints = extraRecruitmentPoints;
	}
}

/**
 * Allows you to give additional construction points to a settlement via a building modifier. You can define the bonus in terms of turns.
 */
public class ExtraConstructionPoints : Capability
{
	public int ExtraPoints => _extraConstructionPoints;
	int _extraConstructionPoints;
	
	public ExtraConstructionPoints (List<RequirementDefinition> requirements, int extraConstructionPoints) : base(requirements) 
	{
		_extraConstructionPoints = extraConstructionPoints;
	}
}

/**
 * agent_limit_settlement <agentType> <limit> - sets the amount of a given agent type that can be recruited here, agent type and limit number
 */
public class SettlementAgentLimit : Capability
{
	public CharacterType AgentType => _agentType;
	public int Limit => _limit;

	CharacterType _agentType;
	int _limit;
	
	public SettlementAgentLimit (List<RequirementDefinition> requirements, CharacterType agentType, int limit) : base(requirements)
	{
		_agentType = agentType;
		_limit = limit;
	}
}

/**
 * suppresses religious unrest ?? Literally no idea what input comes with it .... Not tested at all
 */
public class ReligiousOrder : Capability
{
	public int Order => _order;
	int _order;
	
	public ReligiousOrder (List<RequirementDefinition> requirements, int order) : base(requirements) 
	{
		_order = order;
	}
}

/**
 * Public order due to happiness 1-x (5-x%)
 */
public class HappinessBonus : BonusCapability
{
	public int Bonus => _happinessBonus;
	int _happinessBonus;
	
	public HappinessBonus (List<RequirementDefinition> requirements, bool isBonus, int happinessBonus) : base(requirements, isBonus) 
	{
		_happinessBonus = happinessBonus;
	}
}

/**
 * Public order bonus due to law 1-x (5-x%)
 */
public class LawBonus : BonusCapability
{
	public int Bonus => _lawBonus;
	int _lawBonus;

	public LawBonus (List<RequirementDefinition> requirements, bool isBonus, int lawBonus) : base(requirements, isBonus) 
	{
		_lawBonus = lawBonus;
	}
}

/**
 * Pop. growth 1-25 (0.5-12.5%)
 */
public class PopGrowthBonus : BonusCapability
{
	public int Bonus => _popGrowthBonus;
	int _popGrowthBonus;

	public PopGrowthBonus (List<RequirementDefinition> requirements, bool isBonus, int popGrowthBonus) : base(requirements, isBonus) 
	{
		_popGrowthBonus = popGrowthBonus;
	}
}

/**
 * public health 1-x (5-x%)
 */
public class PopHealthBonus : BonusCapability
{
	public int Bonus => _healthBonus;
	int _healthBonus;
	
	public PopHealthBonus (List<RequirementDefinition> requirements, bool isBonus, int healthBonus) : base(requirements, isBonus) 
	{
		_healthBonus = healthBonus;
	}
}

/**
 * Allows gladiatorial games 1-3
 */
public class StageGames : Capability
{
	public int GamesLevel => _gamesLevel;
	int _gamesLevel;

	public StageGames (List<RequirementDefinition> requirements, int gamesLevel) : base(requirements) 
	{
		_gamesLevel = gamesLevel;
	}
}

/**
 * allows races 1-2
 */
public class StageRaces : Capability
{
	public int RacesLevel => _racesLevel;
	int _racesLevel;

	public StageRaces (List<RequirementDefinition> requirements, int racesLevel) : base(requirements) 
	{
		_racesLevel = racesLevel;
	}
}

/**
 * increase conversion to a specific religion, religious_belief [religion] [1-x] (5-x%)
 */
public class ReligiousConversion : Capability
{
	public string ReligionID => _religionID;
	public int ConversionStrength => _conversionStrength;
	
	string _religionID;
	int _conversionStrength;

	public ReligiousConversion (List<RequirementDefinition> requirements, string religionID, int conversionStrength) : base(requirements)
	{
		_religionID = religionID;
		_conversionStrength = conversionStrength;
	}
}

/**
 * allows recruiting units, recruit "[unit-name]" [experience, 1-9]
 */
public class Recruitment : Capability
{
	public string UnitID => _unitID;
	public int Experience => _experience;
	
	string _unitID;
	int _experience;

	public Recruitment (List<RequirementDefinition> requirements, string unitID, int experience) : base(requirements)
	{
		_unitID = unitID;
		_experience = experience;
	}
}

/**
 * allows recruiting of agents, agent [agent-type] [skill(?), 1-10]
 */
public class Agent : Capability
{
	public CharacterType AgentType => _agentType;
	public int Experience => _experience;

	CharacterType _agentType;
	int _experience;

	public Agent (List<RequirementDefinition> requirements, CharacterType agentType, int experience) : base(requirements)
	{
		_agentType = agentType;
		_experience = experience;
	}
}

/**
 * Armour upgrade 1 (1)
 */
public class ArmourUpgrade : BonusCapability
{
	public int ArmourValue => _armourValue;
	
	int _armourValue;

	public ArmourUpgrade (List<RequirementDefinition> requirements, bool isBonus, int armourValue) : base(requirements, isBonus) 
	{
		_armourValue = armourValue;
	}
}

/**
 * (upgrades melee (light) weapons) 1 (1)
 */
public class WeaponLightUpgrade : Capability
{
	public int UpgradeLevel => _upgradeLevel;

	int _upgradeLevel;

	public WeaponLightUpgrade (List<RequirementDefinition> requirements, int upgradeLevel) : base(requirements) 
	{
		_upgradeLevel = upgradeLevel;
	}
}

/**
 * (upgrades bladed (heavy) weapons) 1 (1)
 */
public class WeaponHeavyUpgrade : Capability
{
	public int UpgradeLevel => _upgradeLevel;
	
	int _upgradeLevel;

	public WeaponHeavyUpgrade (List<RequirementDefinition> requirements, int upgradeLevel) : base(requirements) 
	{
		_upgradeLevel = upgradeLevel;
	}
}

/**
 * (upgrades missile weapons) 1-5 (1-5)
 */
public class WeaponMissileUpgrade : Capability
{
	public int UpgradeLevel => _upgradeLevel;

	int _upgradeLevel;

	public WeaponMissileUpgrade (List<RequirementDefinition> requirements, int upgradeLevel) : base(requirements) 
	{
		_upgradeLevel = upgradeLevel;
	}
}

public class WeaponOtherUpgrade : Capability
{
	public int UpgradeLevel => _upgradeLevel;
	
	int _upgradeLevel;

	public WeaponOtherUpgrade (List<RequirementDefinition> requirements, int upgradeLevel) : base(requirements) 
	{
		_upgradeLevel = upgradeLevel;
	}
}

/**
 * walls 0-4 (palisade, wooden, stone, large stone, epic stone)
 */
public class WallLevel : Capability
{
	public int Level => _level;
	
	int _level;

	public WallLevel (List<RequirementDefinition> requirements, int level) : base(requirements) 
	{
		_level = level;
	}
}

/**
 * gates 0-2 (wooden, reinforced, iron-bound), overriden by wall_level (?)
 */
public class GateStrength : Capability
{
	public int Level => _level;
	
	int _level;

	public GateStrength (List<RequirementDefinition> requirements, int level) : base(requirements) 
	{
		_level = level;
	}
}

/**
 * gate defence 0-1 (scorched sand/boiling oil), overriden by wall_level (?), does not appear to work since stone walls of any kind come with scorched sand/boiling oil
 */
public class GateDefenses : Capability
{
	public int Level => _level;
	
	int _level;

	public GateDefenses (List<RequirementDefinition> requirements, int level) : base(requirements) 
	{
		_level = level;
	}
}

/**
 * towers 0-2 (watchtower, arrow, ballista)
 */
public class TowerLevel : Capability
{
	public int Level => _level;
	
	int _level;

	public TowerLevel (List<RequirementDefinition> requirements, int level) : base(requirements) 
	{
		_level = level;
	}
}

/**
 * Increases morale of units recruited 1-4 (1-4) (previously non-functional in original game)
 */
public class RecruitsMoraleBonus : BonusCapability
{
	public int Bonus => _bonus;

	int _bonus;

	public RecruitsMoraleBonus (List<RequirementDefinition> requirements, bool isBonus, int bonus) : base(requirements, isBonus) 
	{
		_bonus = bonus;
	}
}

/**
 * Increases morale of units recruited 1-4 (1-4) (previously non-functional in original game)
 */
public class RecruitsExperienceBonus : BonusCapability
{
	public int Bonus => _bonus;
	
	int _bonus;

	public RecruitsExperienceBonus (List<RequirementDefinition> requirements, bool isBonus, int bonus) : base(requirements, isBonus) 
	{
		_bonus = bonus;
	}
}

}