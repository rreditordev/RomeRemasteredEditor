
using System;
using System.Collections.Generic;
using RtwFileIO;
using Godot;
using UIUtilities;

namespace Model {

    public class Region
{
    public Color32 RegionColor => _regionColor;
    public string RegionName => _regionName;
    public string RegionCapitalName => _regionCapitalName;
    public string IndigenousCultureOrFactionName => _indigenousCultureOrFactionName;
    public string RebelFactionName => _rebelFactionName;
    public int BaseFarmLevel => _baseFarmLevel;
    public List<Tuple<string, int>> ReligiousMakeup => _religiousMakeup;
    public bool IsCoastal => _isCoastal;

    Color32 _regionColor;
    string _regionName;
    string _regionCapitalName;
    Vector2I _capitalPosition;
    Vector2I _portPosition;
    string _indigenousCultureOrFactionName;
    string _rebelFactionName;
    List<string> _resourcesList;
    int _baseFarmLevel;
    List<Tuple<string, int>> _religiousMakeup;
    bool _isCoastal;

    public Region(DescrRegion region, Vector2I capitalPosition, Vector2I portPosition)
    {
        _regionColor = region.RegionColor;
        _regionName = region.RegionName;
        _regionCapitalName = region.RegionCapitalName;
        _capitalPosition = capitalPosition;
        _portPosition = portPosition;
        _indigenousCultureOrFactionName = region.IndigenousCultureOrFactionName;
        _rebelFactionName = region.RebelFactionName;
        _resourcesList = region.ResourceList;
        _baseFarmLevel = region.BaseFarmLevel;
        _religiousMakeup = region.ReligiousMakeup;
        _isCoastal = portPosition != default;
    }
}
    
}