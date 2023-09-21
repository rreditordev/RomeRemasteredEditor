using Model;
using System.Collections.Generic;
using Godot;

namespace RtwFileIO
{

	public struct Strat
	{
		public string CampaignType;
		public string CampaignOption;
		public Dictionary<string, List<string>> FactionsDeclaration;
		public int StartYear;
		public Season StartSeason;
		public int EndYear;
		public Season EndSeason;
		public int BrigandSpawnValue;
		public int PirateSpawnValue;
		public List<LandmarkInfo> Landmarks;
		public List<ResourceInfo> Resources;
		public Dictionary<string, FactionInfo> FactionInfos;
		public List<DiplomacyInfo> CoreAttitudes;
		public List<DiplomacyInfo> FactionRelationships;
		public List<DiplomacyInfo> FactionAggressions;
		public List<SpawnScriptInfo> SpawnScripts;
		public List<ScriptInfo> Scripts;
		public List<RegionLocationsInfo> RegionLocations;
	};

	public struct ScriptInfo
	{
		public string ScriptPath;
		public string Options; // E.g. "once_only"
	}

	public struct SpawnScriptInfo
	{
		public string Faction;
		public string Event;
		public string ScriptPath;
	}

	public struct RegionLocationsInfo {
		public string regionName;
		public string objectType;
		public Vector2I MapPosition;
		public string comments;
	}

		public struct DiplomacyInfo
	{
		public string FactionSource;
		public List<string> FactionTargets;
		public int DiplomaticValue;
	}

	public struct LandmarkInfo
	{
		public string ID;
		public Vector2I MapPosition;
	};

	public struct ResourceInfo
	{
		public string ID;
		public int AbundanceLevel;
		public Vector2I MapPosition;
		public string RegionTag;
	};


	public struct FactionInfo
	{
		public string ID;
		public string AIPersonality;
		public bool IsEmergent;
		public List<string> AIDoNotAttackFactions;
		public int StartingMoney;
		public List<SettlementInfo> Settlements;
		public List<CharacterInfo> Characters;
		public List<CharacterRecord> CharacterRecords;
		public List<RelativeInfo> RelativesInfo;
	}

	public struct RelativeInfo
	{
		public string Name;
		public string WifeName;
		public List<string> OffspringNames;
	}

	public struct CharacterRecord
	{
		public string Name;
		public Gender Gender;
		public int Age;
		public string IsAlive;
		public int DeathRelativeToStart;
		public string IsPastLeader;
		public Dictionary<string, int> Traits;
	}

	public struct UnitInfo
	{
		public string UnitName;
		public int Experience;
		public int ArmourLevel;
		public int WeaponLevel;
	}

	public struct CharacterInfo
	{
		public string SubFaction;
		public string Name;
		public CharacterType Type;
		public Noble.Rank FactionLeadership;
		public int Age;
		public int command;
		public int influence;
		public int management;
		public int subterfuge;
		public Vector2I MapPosition;
		public Dictionary<string, int> Traits;
		public List<string> Ancillaries;
		public List<UnitInfo> LeadingArmy;
		public string comments;
	}


	public struct SettlementInfo
	{
		public SettlementLevel Level;
		public string Region;
		public int StartingPopulation;
		public string PlanSetName;
		public string FactionCreator;
		public List<BuildingInfo> BuildingInfos;
	}

	public struct BuildingInfo
	{
		public string BuildingTreeName;
		public string BuildingLevelName;
	}

}