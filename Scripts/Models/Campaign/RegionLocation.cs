using RtwFileIO;
using System.Collections;
using System.Collections.Generic;
using Godot;

public class RegionLocation 
{
    public string Comments => _comments;
    public Vector2I MapPosition => _mapPosition;
    public string ObjectType => _objectType;
    public string RegionName => _regionName;

    public Vector2I _mapPosition;
    private string _comments;
    private string _regionName;
    private string _objectType;

    public RegionLocation(RegionLocationsInfo stratRLInfo) {
        SetComments(stratRLInfo.comments);
        SetMapPosition(stratRLInfo.MapPosition);
        SetRegionName(stratRLInfo.regionName);
        SetObjectType(stratRLInfo.objectType);
    }

    void SetComments(string comments) {
        this._comments = comments;
    }

    void SetMapPosition(Vector2I mapPosition) {
        _mapPosition = mapPosition;
    }

    void SetRegionName(string regionName) {
        this._regionName = regionName;
    }

    void SetObjectType(string objectType) {
        this._objectType = objectType;
    }
}
