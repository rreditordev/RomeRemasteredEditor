using RtwFileIO;

namespace Model
{

public class RequirementAlias
{
	public string AliasID => _aliasID;
	public RequirementList Requirements => _requirements;
	public string DisplayStringID => _displayStringID;

	string _aliasID;
	RequirementList _requirements;
	string _displayStringID;
	
	public RequirementAlias (AliasDefinition definition)
	{
		_aliasID = definition.AliasID;
		_requirements = new RequirementList(definition.Requirements);
		_displayStringID = definition.DisplayStringID;
	}
}

}