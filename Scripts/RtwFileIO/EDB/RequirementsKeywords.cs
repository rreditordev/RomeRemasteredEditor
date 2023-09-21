namespace RtwFileIO
{

public static partial class Keywords
{
// See https://github.com/FeralInteractive/romeremastered/blob/main/documentation/data_file_guides/EDB.md
	public const string ResourcePresent = "resource"; // checks if the settlement has that resource can also use factionwide as a check to check if the resource is found withing the empire.
	public const string HiddenResourcePresent = "hidden_resource"; // checks if the settlement has that hidden resource can also use factionwide as a check to check if the resource is found withing the empire.
	public const string BuildingPresent = "building_present"; // checks if a building exists at any level.
	public const string Queued = "queued"; // will also check the building queue not just constructed buildings
	public const string FactionWide = "factionwide"; // will check for the existence of a building across all settlements controlled by the faction allowing for buildings that can only be built once per faction.
	public const string BuildingPresentLevel = "building_present_min_level"; // checks if the building exists at at least the specified level.
	public const string MajorEvent = "major_event"; // checks if the given major event has triggered for this faction.
	public const string Factions = "factions"; // checks if the settlement is controlled by a given faction.
	public const string All = "all"; // you can use all if you just want it to be true for all factions
	public const string Port = "port"; // This returns true in coastal areas with ports assigned (i.e. in map_regions.tga).
	public const string IsPlayer = "is_player"; // Allows you to use the requires function to state if a building is available for only the player or only the AI factions.
	public const string IsToggled = "is_toggled"; // checks if the given gameplay toggle is turned on. This allows you to unlock items based on any of the classic/remastered toggle settings.
	public const string Diplomacy = "diplomacy"; // checks the relationship of the settlement's owner with the given faction
	public const string Allied = "allied";
	public const string Protector = "protector";
	public const string Protectorate = "protectorate";
	public const string SameSuperFaction = "same_superfaction";
	public const string AtWar = "at_war";
	public const string BuildingFactions = "building_factions"; // checks if the building was built by a given faction.
	public const string SettlementCapability = "capability"; // checks if the settlement has a capability of at least that amount. This allows you to conditionalise items being available based on settlement capabilities like public order etc
	public const string NoBuildingTagged = "no_building_tagged"; // checks that no building with this tag exists, (lower levels of this building within the same settlement are not counted).
	public const string SettlementReligion = "religion"; // checks how much influence a religion has in this settlement.
	public const string SettlementMajorityReligion = "majority_religion"; // checks if the religion is the majority (highest influence) religion in the settlement
	public const string OfficialReligion = "official_religion"; // checks if the religion is the official religion in the settlement
}

}