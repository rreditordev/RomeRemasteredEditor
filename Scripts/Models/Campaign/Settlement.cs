using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public enum SettlementLevel
{
	Village,
	Town,
	LargeTown,
	City,
	LargeCity,
	HugeCity
}

public class Settlement
{
	public SettlementLevel Level => _level;
	public string RegionID => _regionID;
	public int StartingPopulation => _startingPopulation;
	public string PlanSetName => _planSetName;
	public string FactionCreatorID => _factionCreatorID;
	
	SettlementLevel _level;
	string _regionID;
	int _startingPopulation;
	string _planSetName;
	string _factionCreatorID;
	List<Building> _buildings = new();

	public Settlement (SettlementInfo stratSettlementInfo)
	{
		SetLevel(stratSettlementInfo.Level);
		SetRegionID(stratSettlementInfo.Region);
		SetStartingPopulation(stratSettlementInfo.StartingPopulation);
		SetPlanSetName(stratSettlementInfo.PlanSetName);
		SetFactionCreatorID(stratSettlementInfo.FactionCreator);
		SetBuildingInstances(stratSettlementInfo.BuildingInfos);
	}

	public Settlement (string regionID, SettlementLevel level, int startingPopulation, string planSetName, string factionCreatorID)
	{
		SetRegionID(regionID);
		SetLevel(level);
		SetStartingPopulation(startingPopulation);
		SetPlanSetName(planSetName);
		SetFactionCreatorID(factionCreatorID);
	}

	public List<Building> GetBuildings ()
	{
		return new List<Building>(_buildings);
	}

	public void SetLevel (SettlementLevel level)
	{
		_level = level;
	}
	
	public void SetRegionID (string regionID)
	{
		_regionID = regionID;
	}
	
	public void SetStartingPopulation (int startingPopulation)
	{
		_startingPopulation = startingPopulation;
	}
	
	public void SetPlanSetName (string planSetName)
	{
		_planSetName = planSetName;
	}
	
	public void SetFactionCreatorID (string factionCreator)
	{
		_factionCreatorID = factionCreator;
	}
	
	void SetBuildingInstances (List<BuildingInfo> buildingInfos)
	{
		for (var i = 0; i < buildingInfos.Count; i++)
		{
			Building building = new(buildingInfos[i]);
			_buildings.Add(building);
		}
	}
}

}