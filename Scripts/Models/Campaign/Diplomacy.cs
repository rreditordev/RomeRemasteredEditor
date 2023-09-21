using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class Diplomacy
{
	List<DiplomacyInfo> _coreAttitudes = new();
	List<DiplomacyInfo> _relationships = new();
	List<DiplomacyInfo> _aggression = new();

	public Diplomacy (List<DiplomacyInfo> coreAttitudes, List<DiplomacyInfo> relationships, List<DiplomacyInfo> aggression)
	{
		_coreAttitudes = coreAttitudes;
		_relationships = relationships;
		_aggression = aggression;
	}

	public List<DiplomacyInfo> GetCoreAttitudes ()
	{
		return new List<DiplomacyInfo>(_coreAttitudes);
	}

	public List<DiplomacyInfo> GetRelationships ()
	{
		return new List<DiplomacyInfo>(_relationships);
	}
	
	public List<DiplomacyInfo> GetAggressions ()
	{
		return new List<DiplomacyInfo>(_aggression);
	}
}

}