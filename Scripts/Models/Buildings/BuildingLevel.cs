using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class BuildingLevel
{
	public string BuildingLevelID => _buildingLevelID;
	public RequirementList AiDestructionRequirements => _aiDestructionRequirements;
	public RequirementList Requirements => _requirements;
	public int ConstructionTime => _constructionTime;
	public int ConstructionCost => _constructionCost;
	public SettlementLevel MinSettlementLevel => _minSettlementLevel;
	
	string _buildingLevelID;
	RequirementList _aiDestructionRequirements;
	RequirementList _requirements;
	List<Capability> _factionCapabilities = new();
	List<Capability> _capabilities = new();
	int _constructionTime;
	int _constructionCost;
	SettlementLevel _minSettlementLevel;
	List<string> _upgradeLevelIDs;
	
	public BuildingLevel (BuildingLevelDefinition definition)
	{
		_buildingLevelID = definition.BuildingLevelID;
		_aiDestructionRequirements = new RequirementList(definition.AiDestructionRequirements);
		_requirements = new RequirementList(definition.Requirements);
		SetCapabilities(definition.FactionCapabilities, definition.Capabilities);
		_constructionTime = definition.ConstructionTime;
		_constructionCost = definition.ConstructionCost;
		_minSettlementLevel = definition.MinSettlementLevel;
		_upgradeLevelIDs = new List<string>(definition.UpgradeLevelIDs);
	}

	public List<Capability> GetFactionCapabilities ()
	{
		return new List<Capability>(_factionCapabilities);
	}
	
	public List<Capability> GetCapabilities ()
	{
		return new List<Capability>(_capabilities);
	}
	
	public List<string> GetUpgradeIDs ()
	{
		return new List<string>(_upgradeLevelIDs);
	}
	
	void SetCapabilities (List<BuildingCapability> factionCapabilities, List<BuildingCapability> capabilities)
	{
		for (var i = 0; i < factionCapabilities.Count; i++)
		{
			_factionCapabilities.Add(CapabilityFactory.CreateCapability(factionCapabilities[i]));
		}
		
		for (var i = 0; i < capabilities.Count; i++)
		{
			_capabilities.Add(CapabilityFactory.CreateCapability(capabilities[i]));
		}
	}
}

}