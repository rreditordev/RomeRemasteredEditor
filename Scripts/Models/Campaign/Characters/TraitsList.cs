using System.Collections.Generic;

namespace Model
{

public class Trait
{
	public string ID => _traitID;
	public int Level => _traitLevel;
	
	string _traitID;
	int _traitLevel;

	public Trait (string traitID, int traitLevel)
	{
		_traitID = traitID;
		_traitLevel = traitLevel;
	}
}

public class TraitList
{
	List<Trait> _traits = new();
	
	public TraitList (Dictionary<string, int> traitsDictionary)
	{
		SetTraits(traitsDictionary);
	}

	public List<Trait> GetTraits ()
	{
		return new List<Trait>(_traits);
	}

	void SetTraits (Dictionary<string, int> traitsDictionary)
	{
		foreach ((string traitID, int level) in traitsDictionary)
		{
			_traits.Add(new Trait(traitID, level));
		}
	}
}

}