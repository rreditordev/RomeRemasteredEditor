using System.Collections.Generic;

namespace Model
{

public abstract class Requirement
{
	public bool IsNegated => _isNegated;
	bool _isNegated;

	protected Requirement (bool isNegated)
	{
		_isNegated = isNegated;
	}
}

/**
 * This is a requirement that is a reference to an Alias. The Alias
 * contains the requirements themselves and is shared by all who reference it.
 */
public class AliasedRequirement : Requirement
{
	public string AliasID => _aliasID;
	string _aliasID;
	
	public AliasedRequirement (bool isNegated, string aliasID) : base(isNegated)
	{
		_aliasID = aliasID;
	}
}

/*
 * resource <resource name> [factionwide] - checks if the settlement has that resource
 * 2.0.4 Feature: 
 * Can also use factionwide as a check to check if the resource is found withing the empire.
 * Same for hidden resource but starts with "hidden_resource" instead of "resource"
 */
public class ResourceRequirement : Requirement
{
	public string ResourceID => _resourceID;
	public bool IsFactionWide => _isFactionWide;
	public bool IsHiddenResource => _isHiddenResource;
	
	string _resourceID;
	bool _isFactionWide;
	bool _isHiddenResource;

	public ResourceRequirement (bool isNegated, string resourceID, bool isFactionWide, bool isHiddenResource = false) : base(isNegated)
	{
		_resourceID = resourceID;
		_isFactionWide = isFactionWide;
		_isHiddenResource = isHiddenResource;
	}
}

/**
 *  building_present <building name> [queued] [factionwide] checks if a building exists at any level.
 *  2.0.4 Feature: The game now supports:
 *  -> queued - will also check the building queue not just constructed buildings
 *  -> factionwide - will check for the existence of a building across all settlements controlled by the faction allowing for buildings that can only be built once per faction.
 */
public class BuildingPresentRequirement : Requirement
{
	public string BuildingTreeID => _buildingTreeID;
	public bool IsFactionWide => _isFactionWide;
	public bool Queued => _queued;

	string _buildingTreeID;
	bool _isFactionWide;
	bool _queued;

	public BuildingPresentRequirement (bool isNegated, string buildingTreeID, bool isFactionWide, bool queued) : base(isNegated)
	{
		_buildingTreeID = buildingTreeID;
		_isFactionWide = isFactionWide;
		_queued = queued;
	}
}

/**
 * building_present_min_level <building name> <level> [queued] [factionwide] checks if the building exists at at least the specified level.
 * 2.0.4 Feature: The game now supports:
 * -> queued - will also check the building queue not just constructed buildings
 * -> factionwide - will check for the existence of a building across all settlements controlled by the faction allowing for buildings that can only be built once per faction.
 */
public class BuildingPresentLevelRequirement : Requirement
{
	public string BuildingTreeID => _buildingTreeID;
	public string BuildingLevelID => _buildingLevelID;
	public bool IsFactionWide => _isFactionWide;
	public bool Queued => _queued;

	string _buildingTreeID;
	string _buildingLevelID;
	bool _isFactionWide;
	bool _queued;

	public BuildingPresentLevelRequirement (bool isNegated, string buildingTreeID, string buildingLevelID, bool isFactionWide, bool queued) : base(isNegated)
	{
		_buildingTreeID = buildingTreeID;
		_buildingLevelID = buildingLevelID;
		_isFactionWide = isFactionWide;
		_queued = queued;
	}
}

/**
 * major_event "<event name>" - checks if the given major event has triggered for this faction.
 * As major events are now moddable and you can have multiple events this command replaces the hard coded marian_reforms conditions
 */
public class MajorEventRequirement : Requirement
{
	public string MajorEventID => _majorEventID;
	string _majorEventID;

	public MajorEventRequirement (bool isNegated, string majorEventID) : base(isNegated) 
	{
		_majorEventID = majorEventID;
	}
}


/**
 * factions { <all/culture/faction name>, } - checks if the settlement is controlled by a given faction.
 * 2.0.4 Feature: The game now supports:
 * culture - you can use any custom culture for the checks
 * all - you can use all if you just want it to be true for all factions
 */
public class FactionsRequirement : Requirement
{
	List<string> _factionIDs;
	
	public FactionsRequirement (bool isNegated, List<string> factionIDs) : base(isNegated) 
	{
		_factionIDs = factionIDs;
	}

	public List<string> GetFactions ()
	{
		return new List<string>(_factionIDs);
	}
}

/**
 * This returns true in coastal areas with ports assigned (i.e. in map_regions.tga).
 * It can be used as a condition for buildings and capabilities, including units.
 *
 * NOTE: This is basically a markup class since there is no data to store related to this requirement
 */
public class PortRequirement : Requirement
{
	public PortRequirement (bool isNegated) : base(isNegated) {}
}

/**
 * is_player 2.0.2 Feature: Allows you to use the requires function to state if a building is availble for only the player or only the AI factions.
 * This will allow for the creation of special buildings that mods can use to assist the AI factions, or provide depth to the human player experience
 * without overly complicating the AI build trees.
 */
public class IsPlayerRequirement : Requirement
{
	public IsPlayerRequirement (bool isNegated) : base(isNegated) {}
}

/**
 * is_toggled "<toggle name>" 2.0.2 Feature: checks if the given gameplay toggle is turned on.
 * This allows you to unlock items based on any of the classic/remastered toggle settings.
 */
public class ToggleRequirement : Requirement
{
	public string ToggleID => _toggleID;
	string _toggleID;
	
	public ToggleRequirement (bool isNegated, string toggleID) : base(isNegated)
	{
		_toggleID = toggleID;
	}
}

public enum DiplomaticStance
{
	Allied,
	Protector,
	Protectorate,
	SameSuperFaction,
	AtWar
}

/**
 * diplomacy <allied/protector/protectorate/same_superfaction/at_war> - checks the relationship of the settlement's owner with the given faction
 */
public class DiplomacyRequirement : Requirement
{
	public DiplomaticStance DiplomaticStance => _diplomaticStance;
	public string TargetFaction => _targetFaction;
	
	DiplomaticStance _diplomaticStance;
	string _targetFaction;
	  
	public DiplomacyRequirement (bool isNegated, DiplomaticStance stance, string targetFaction) : base(isNegated)
	{
		_diplomaticStance = stance;
		_targetFaction = targetFaction;
	}
}

/**
 * building_factions { <all/culture/faction name>, } 2.0.4 Feature: checks if the building was built by a given faction.
 * This allows modders to restrict units or building recruitment based on the faction that built the building instead of the faction that controls the building.
 * This will allow factions to have the ability to recruit certain units via conquest only and not via construction.
 */
public class BuildingFactionsRequirement : Requirement
{
	List<string> _factionIDs;
	
	public BuildingFactionsRequirement (bool isNegated, List<string> factionIDs) : base(isNegated) 
	{
		_factionIDs = factionIDs;
	}
	
	public List<string> GetFactions ()
	{
		return new List<string>(_factionIDs);
	}
}

/**
 * 2.0.4 Feature: checks if the settlement has a capability of at least that amount.
 * This allows you to conditionalise items being available based on settlement capabilities like public order etc
 */
public class SettlementCapabilityRequirement : Requirement
{
	string _capability;
	int _number;

	public SettlementCapabilityRequirement (bool isNegated, string capability, int number) : base(isNegated)
	{
		_capability = capability;
		_number = number;
	}
}

/**
 * no_building_tagged <tag name> [queued] [factionwide]
 * 2.0.4 Feature: As explained in more detail in the prior section
 * this checks that no building with this tag exists (lower levels of this building within the same settlement are not counted).
 * This is used in the base game to restrict temples to only one type. This feature also supports the following optional items:
 * queued - will also check the building queue not just constructed buildings
 * faction wide - will check for the existence of a building across all settlements
 */
public class NoBuildingTaggedRequirement : Requirement
{
	public string Tag => _tag;
	public bool IsFactionWide => _isFactionWide;
	public bool Queued => _queued;

	string _tag;
	bool _isFactionWide;
	bool _queued;

	public NoBuildingTaggedRequirement (bool isNegated, string tag, bool isFactionWide, bool queued) : base(isNegated)
	{
		_tag = tag;
		_isFactionWide = isFactionWide;
		_queued = queued;
	}
}

public enum ComparisonOperator
{
	GreaterThan,
	LessThan,
	GreaterOrEqualTo,
	LessOrEqualTo
}

/**
 * religion <religion name> <comparison operator> <number> (i.e. "religion christianity >= 60")
 * 2.0.4 Feature: checks how much influence a religion has in this settlement.
 */
public class SettlementReligionRequirement : Requirement
{
	public string ReligionID => _religionID;
	public ComparisonOperator ComparisonOperator => _comparisonOperator;
	public int TargetReligionInfluence => _targetReligionInfluence;

	string _religionID;
	ComparisonOperator _comparisonOperator;
	int _targetReligionInfluence;

	public SettlementReligionRequirement (bool isNegated, string religionID, ComparisonOperator comparisonOperator, int targetReligionInfluence) : base(isNegated)
	{
		_religionID = religionID;
		_comparisonOperator = comparisonOperator;
		_targetReligionInfluence = targetReligionInfluence;
	}
}

/**
 * majority_religion <religion name>
 * 2.0.4 Feature: checks if the religion is the majority (highest influence) religion in the settlement
 */
public class SettlementMajorityReligionRequirement : Requirement
{
	public string ReligionID => _religionID;
	
	string _religionID;

	public SettlementMajorityReligionRequirement (bool isNegated, string religionID) : base(isNegated) 
	{
		_religionID = religionID;
	}
}

/**
 * official_religion 2.0.4 Feature: checks if the religion is the official religion in the settlement (set by owning faction's leader, local governor or
 * local buildings like a temple)
 */
public class SettlementOfficialReligionRequirement : Requirement
{
	public string ReligionID => _religionID;

	string _religionID;

	public SettlementOfficialReligionRequirement (bool isNegated, string religionID) : base(isNegated) 
	{
		_religionID = religionID;
	}
}

}