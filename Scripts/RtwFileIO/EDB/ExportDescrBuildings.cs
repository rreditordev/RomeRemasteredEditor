using Model;
using System.Collections.Generic;

namespace RtwFileIO
{

public struct ExportDescrBuildings
{
	public List<string> Tags;
	public List<AliasDefinition> Aliases;
	public List<BuildingTreeDefinition> BuildingTreeDefinitions;
}

public struct AliasDefinition
{
	public string AliasID;
	public List<RequirementDefinition> Requirements;
	public string DisplayStringID;
}

public struct BuildingTreeDefinition
{
	public string BuildingTreeID;
	public bool IsIndestructible;
	public string Classification;
	public string Tag;
	public string IconID;
	public List<string> LevelIDs;
	public List<BuildingLevelDefinition> BuildingLevelDefinitions;
	public List<string> Plugins;
}

public struct BuildingLevelDefinition
{
	public string BuildingLevelID;
	public List<RequirementDefinition> AiDestructionRequirements;
	public List<RequirementDefinition> Requirements;
	public List<BuildingCapability> FactionCapabilities;
	public List<BuildingCapability> Capabilities;
	public int ConstructionTime;
	public int ConstructionCost;
	public SettlementLevel MinSettlementLevel;
	public List<string> UpgradeLevelIDs;
}

public struct BuildingCapability
{
	public string Capability;
	public List<RequirementDefinition> Requirement;
}

public struct RequirementDefinition
{
	public bool Negated;
	public string Condition;
	public BinaryOperator BinaryOperator;
}

}