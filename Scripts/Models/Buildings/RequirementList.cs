using RtwFileIO;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Model
{

public enum BinaryOperator
{
	None,
	And,
	Or
}

public class RequirementList
{
	List<Requirement> _requirements = new();
	List<BinaryOperator> _operators = new();
	
	public RequirementList (List<RequirementDefinition> definitions)
	{
		BuildRequirements(definitions);
	}

	public int NumRequirements ()
	{
		return _requirements.Count;
	}
	
	public List<Requirement> GetRequirements ()
	{
		return new List<Requirement>(_requirements);
	}

	public List<BinaryOperator> GetOperators ()
	{
		return new List<BinaryOperator>(_operators);
	}
	
	void BuildRequirements (IReadOnlyList<RequirementDefinition> definitions)
	{
		if (definitions.Count == 0) return;
		
		for (var i = 0; i < definitions.Count; i++)
		{
			_requirements.Add(RequirementFactory.CreateRequirement(definitions[i]));
			if (definitions[i].BinaryOperator is not BinaryOperator.None)
			{
				_operators.Add(definitions[i].BinaryOperator);
			}
		}
		
		if (_operators.Count != _requirements.Count - 1)
		{
			GD.PrintErr("RequirementList: Number of operators does not match amount needed based on requirements.");
		}
	}

	public bool AcceptsFaction (string factionID, string factionCulture)
	{
		List<FactionsRequirement> factionsRequirements = _requirements.OfType<FactionsRequirement>().ToList();
		if (factionsRequirements.Count == 0) return true;
		
		FactionsRequirement requirement = factionsRequirements[0]; // only ONE faction requirement is considered
		List<string> factions = requirement.GetFactions();
		for (var j = 0; j < factions.Count; j++)
		{
			if (factions[j] == factionID || factions[j] == factionCulture) return ! requirement.IsNegated;
		}
		
		return requirement.IsNegated;
	}

	public bool HasPlayerRequirement ()
	{
		return _requirements.OfType<IsPlayerRequirement>().ToList().Count != 0;
	}
	
	public bool AcceptsPlayer (bool isPlayer)
	{
		List<IsPlayerRequirement> playerRequirements = _requirements.OfType<IsPlayerRequirement>().ToList();
		if (playerRequirements.Count == 0) return true;

		IsPlayerRequirement requirement = playerRequirements[0]; // only ONE is player requirement is considered
		return isPlayer ? ! requirement.IsNegated : requirement.IsNegated;
	}
}

}