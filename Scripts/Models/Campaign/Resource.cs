using RtwFileIO;
using Godot;

namespace Model
{

public class Resource
{
	public string ResourceID => _resourceID;
	public int AbundanceLevel => _abundanceLevel;
	public Vector2I MapPosition => _mapPosition;
	public string RegionTag => _regionTag;
	
	string _resourceID;
	int _abundanceLevel;
	public Vector2I _mapPosition;
	string _regionTag;

	public Resource (ResourceInfo stratResourceInfo)
	{
		SetResourceID(stratResourceInfo.ID);
		SetAbundanceLevel(stratResourceInfo.AbundanceLevel);
		SetMapPosition(stratResourceInfo.MapPosition);
		SetRegionTag(stratResourceInfo.RegionTag);
	}

    public Resource(string resId, int abundanceLevel, string regionTag) {
		SetResourceID(resId);
		SetAbundanceLevel(abundanceLevel);
		SetRegionTag(regionTag);
    }

        void SetResourceID (string resourceID)
	{
		_resourceID = resourceID;
	}

	void SetAbundanceLevel (int abundanceLevel)
	{
		_abundanceLevel = abundanceLevel;
	}

	void SetMapPosition (Vector2I mapPosition)
	{
		_mapPosition = mapPosition;
	}

	void SetRegionTag (string regionTag)
	{
		_regionTag = regionTag;
	}
}

}