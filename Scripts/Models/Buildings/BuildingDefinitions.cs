using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class BuildingDefinitions
{
	List<string> _tags = new();
	List<RequirementAlias> _aliases = new();
	List<BuildingTree> _buildingTrees = new();

	public BuildingDefinitions (ExportDescrBuildings edb)
	{
		SetTags(edb.Tags);
		SetAliases(edb.Aliases);
		SetBuildingTrees(edb.BuildingTreeDefinitions);
	}

	public List<string> GetTags ()
	{
		return new List<string>(_tags);
	}

	public List<RequirementAlias> GetAliases ()
	{
		return new List<RequirementAlias>(_aliases);
	}
	
	public List<BuildingTree> GetBuildingTrees ()
	{
		return new List<BuildingTree>(_buildingTrees);
	}
	
	void SetTags (List<string> edbTags)
	{
		_tags = new List<string>(edbTags);
	}

	void SetAliases (List<AliasDefinition> edbAliases)
	{
		for (var i = 0; i < edbAliases.Count; i++)
		{
			_aliases.Add(new RequirementAlias(edbAliases[i]));
		}
	}

	void SetBuildingTrees (List<BuildingTreeDefinition> buildingTreeDefinitions)
	{
		for (var i = 0; i < buildingTreeDefinitions.Count; i++)
		{
			_buildingTrees.Add(new BuildingTree(buildingTreeDefinitions[i]));
		}
	}
}

}