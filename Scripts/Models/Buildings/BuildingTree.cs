using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class BuildingTree
{
	public string BuildingTreeID => _buildingTreeID;
	public bool IsIndestructible => _isIndestructible;
	public string Classification => _classification;
	public string Tag => _tag;
	public string IconID => _iconID;
	
	string _buildingTreeID;
	bool _isIndestructible;
	string _classification;
	string _tag;
	string _iconID;
	List<BuildingLevel> _buildingLevels = new();
	List<string> _plugins = new();
	
	public BuildingTree (BuildingTreeDefinition definition)
	{
		_buildingTreeID = definition.BuildingTreeID;
		_isIndestructible = definition.IsIndestructible;
		_classification = definition.Classification;
		_tag = definition.Tag;
		_iconID = definition.IconID;
		SetBuildingLevels(definition.BuildingLevelDefinitions);
		_plugins = new List<string>(definition.Plugins);
	}

	public List<BuildingLevel> GetLevels ()
	{
		return new List<BuildingLevel>(_buildingLevels);
	}
	
	public List<string> GetPlugins ()
	{
		return new List<string>(_plugins);
	}
	
	void SetBuildingLevels (List<BuildingLevelDefinition> buildingLevelDefinitions)
	{
		for (var i = 0; i < buildingLevelDefinitions.Count; i++)
		{
			_buildingLevels.Add(new BuildingLevel(buildingLevelDefinitions[i]));
		}
	}
}

}