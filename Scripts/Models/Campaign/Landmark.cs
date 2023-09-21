using RtwFileIO;
using Godot;

namespace Model
{

public class Landmark
{
	public string ID => _landmarkID;
	public Vector2I MapPosition => _mapPosition;
	
	string _landmarkID;
	public Vector2I _mapPosition;

	public Landmark (LandmarkInfo stratLandmarkInfo)
	{
		SetLandmarkID(stratLandmarkInfo.ID);
		SetMapPosition(stratLandmarkInfo.MapPosition);
	}

	public Landmark(string landmarkID) {
			_landmarkID = landmarkID;
	}
	
	void SetLandmarkID (string landmarkID)
	{
		_landmarkID = landmarkID;
	}
	
	void SetMapPosition (Vector2I mapPosition)
	{
		_mapPosition = mapPosition;
	}
}

}