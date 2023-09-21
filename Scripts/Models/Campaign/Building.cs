using RtwFileIO;

namespace Model
{

public class Building
{
	public string BuildingTreeID => _buildingTreeID;
	public string BuildingLevelID => _buildingLevelID;
	
	string _buildingTreeID;
	string _buildingLevelID;

	public Building (BuildingInfo stratBuildingInfo)
	{
		SetBuildingTreeID(stratBuildingInfo.BuildingTreeName);
		SetBuildingLevelID(stratBuildingInfo.BuildingLevelName);
	}
	
	void SetBuildingTreeID (string buildingTreeID)
	{
		_buildingTreeID = buildingTreeID;
	}

	void SetBuildingLevelID (string buildingLevelID)
	{
		_buildingLevelID = buildingLevelID;
	}
}

}