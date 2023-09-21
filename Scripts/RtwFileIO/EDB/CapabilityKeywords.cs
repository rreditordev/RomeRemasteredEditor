namespace RtwFileIO
{

public static partial class Keywords
{
// See https://github.com/FeralInteractive/romeremastered/blob/main/documentation/data_file_guides/EDB.md
#region Economy

	public const string TaxBonus = "taxable_income_bonus"; // Boosts tax income by stated percentage; 1-100 (1%-100%)
	public const string TradeBaseBonus = "trade_base_income_bonus"; // Increases trade goods (1-5) - adds 10% to base value of land trade & sea exports
	public const string TradeLandBonus = "trade_level_bonus"; // 2.0.4 Feature: Affects land trade only (previously non-functional in original game)
	public const string TradeFleet = "trade_fleet"; // adds additional overseas trade routes (trade fleets) 1-3 (1-3)
	public const string MineResource = "mine_resource"; // (income from mining) 1-4 (1-4) values above 4 also work and create greater income
	public const string FarmingLevel = "farming_level"; // Plus 0.5% pop. growth and plus 80 income (average harvest) per point - equivalent to base farm level in descr_regions.txt) 1-5 (1-5)
	public const string RoadLevel = "road_level"; // (improved roads and trade) 0-2
	
	public const string ConstructionCostBonusMilitary = "construction_cost_bonus_military"; // (percentile cost reduction for recruitment buildings) 1-100 (1-100%)
	public const string ConstructionCostBonusReligious = "construction_cost_bonus_religious"; // (percentile cost reduction for temples) 1-100 (1-100%)
	public const string ConstructionCostBonusDefensive = "construction_cost_bonus_defensive"; // (percentile cost reduction for walls) 1-100 (1-100%)
	public const string ConstructionCostBonusOther = "construction_cost_bonus_other"; // (percentile cost reduction for civil buildings but applies to all buildings except religious ones) 1-100 (1-100%)
	
	public const string ConstructionTimeBonusMilitary = "construction_time_bonus_military"; // (percentile time reduction for constructing recruitment buildings but does not seem to work) 1-100 (1-100%)
	public const string ConstructionTimeBonusReligious = "construction_time_bonus_religious"; // (percentile time reduction for constructing temples) 1-100 (1-100%)
	public const string ConstructionTimeBonusDefensive = "construction_time_bonus_defensive"; // (percentile time reduction for constructing walls) 1-100 (1-100%)
	public const string ConstructionTimeBonusOther = "construction_time_bonus_other"; // (percentile time reduction for constructing civil buildings but also applies to all buildings except religious ones) 1-100 (1-100%)
	
	public const string ExtraRecruitmentPoints = "extra_recruitment_points"; // Allows you to give additional recruitment points to a settlement via a building modifier. You can define the bonus in terms of turns.
	public const string ExtraConstructionPoints = "extra_construction_points"; // Allows you to give additional construction points to a settlement via a building modifier. You can define the bonus in terms of turns.
	
#endregion
#region Society

	public const string SettlementAgentLimit = "agent_limit_settlement"; // sets the amount of a given agent type that can be recruited here, agent type and limit number
	public const string ReligiousOrder = "religious_order"; // suppresses religious unrest
	public const string HappinessBonus = "happiness_bonus"; // Public order due to happiness 1-x (5-x%)
	public const string LawBonus = "law_bonus"; // Public order bonus due to law 1-x (5-x%)
	public const string PopGrowthBonus = "population_growth_bonus"; // Pop. growth 1-25 (0.5-12.5%)
	public const string PopHealthBonus = "population_health_bonus"; // public health 1-x (5-x%)
	public const string StageGames = "stage_games"; // allows gladiatorial games 1-3
	public const string StageRaces = "stage_races"; // allows races 1-2
	public const string ReligiousConversion = "religious_belief"; // increase conversion to a specific religion, religious_belief [religion] [1-x] (5-x%)
	
#endregion
#region Military
	
	public const string Recruitment = "recruit"; //  allows recruiting units, recruit "[unit-name]" [experience, 1-9]
	public const string Agent = "agent"; //  allows recruiting of agents, agent [agent-type] [skill(?), 1-10]
	public const string ArmourUpgrade = "armour"; // Armour upgrade 1 (1)
	public const string WeaponLightUpgrade = "weapon_simple"; // (upgrades melee (light) weapons) 1 (1)
	public const string WeaponHeavyUpgrade = "weapon_bladed"; // (upgrades bladed (heavy) weapons) 1 (1)
	public const string WeaponMissileUpgrade = "weapon_missile"; // (upgrades missile weapons) 1-5 (1-5)
	public const string WeaponOtherUpgrade = "weapon_other"; // Unknown, needs testing (?)
	
	public const string WallLevel = "wall_level"; //  walls 0-4 (palisade, wooden, stone, large stone, epic stone)
	public const string GateStrength = "gate_strength"; // gates 0-2 (wooden, reinforced, iron-bound), overriden by wall_level (?)
	public const string GateDefenses = "gate_defences"; // gate defence 0-1 (scorched sand/boiling oil), overriden by wall_level (?), does not appear to work since stone walls of any kind come with scorched sand/boiling oil
	public const string TowerLevel = "tower_level"; // towers 0-2 (watchtower, arrow, ballista)

	public const string RecruitsMoraleBonus = "recruits_morale_bonus"; // Increases morale of units recruited 1-4 (1-4) (previously non-functional in original game)
	public const string RecruitsExperienceBonus = "recruits_exp_bonus"; // Upgrades XP of units recruited 1-5 (1-5)
	
#endregion
#region Unused

	public const string WeaponSiegeUpgrade = "weapon_siege"; // Unknown, needs testing (?)
	public const string BodyguardUpgrade = "upgrade_bodyguard"; // (improves general's bodyguard) ? Needs testing ? (comes into effect only after Marian Reforms)
	public const string PopLoyaltyBonus = "population_loyalty_bonus";
	public const string FireRisk = "fire_risk";
	public const string PopFireRisk = "population_fire_risk_bonus";
	public const string Dummy = "dummy"; // does nothing, but allows specifying a string
	
#endregion
}

}