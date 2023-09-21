
using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using UIUtilities;

namespace RtwFileIO
{

public struct DescrRegions
{
    public List<DescrRegion> Regions;
}

public struct DescrRegion
{
    public string RegionName;
    public string RegionCapitalName;
    public string IndigenousCultureOrFactionName;
    public string RebelFactionName;
    public Color32 RegionColor;
    public List<string> ResourceList;
    public int BaseFarmLevel;
    public List<Tuple<string, int>> ReligiousMakeup;
}

public class DescrRegionsReader
{
    readonly string _filepath;
    DescrRegions _data;
    string[] _lines;
    int _curLine = 0;

    public DescrRegionsReader (string filepath)
    {
        _filepath = filepath;
    }

    public DescrRegions Read()
    {
        _lines = File.ReadAllLines(_filepath);
        _data = new DescrRegions
        {
            Regions = new List<DescrRegion>()
        };

        for (_curLine = 0; _curLine < _lines.Length; _curLine++)
        {
            string line = _lines[_curLine].Trim();
            if (line.Length == 0 || line.StartsWith(Keywords.Comment)) continue;
            _data.Regions.Add(ReadRegion());
        }
        
        return _data;
    }

    DescrRegion ReadRegion()
    {
        DescrRegion region = new()
        {
            ResourceList = new List<string>(),
            ReligiousMakeup = new List<Tuple<string, int>>()
        };
        
        region.RegionName = _lines[_curLine].Trim();
        _curLine++;
        
        region.RegionCapitalName = _lines[_curLine].Trim();
        _curLine++;
        
        region.IndigenousCultureOrFactionName = _lines[_curLine].Trim();
        _curLine++;
        
        region.RebelFactionName = _lines[_curLine].Trim();
        _curLine++;
        
        Color32 color = new Color32(0,0,0,1);
        string[] rgb = _lines[_curLine].Trim()
            .Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
        color.r = (byte) RtwReaderUtils.IntParse(rgb[0].Trim());
        color.g = (byte) RtwReaderUtils.IntParse(rgb[1].Trim());
        color.b = (byte) RtwReaderUtils.IntParse(rgb[2].Trim());
        color.a = 1;
        region.RegionColor = color;
        _curLine++;

        string[] list = _lines[_curLine].Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
        foreach (string resource in list)
        {
            region.ResourceList.Add(resource.Trim());
        }
        _curLine++;
        _curLine++; // Double step to also avoid "Triumph Value" which has no use
        
        region.BaseFarmLevel = RtwReaderUtils.IntParse(_lines[_curLine].Trim());
        _curLine++;

        if (_lines[_curLine].Trim().Length == 0) return region; // If no religious composition line exists just exit

        string[] religions = _lines[_curLine].Trim()
            .Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);

        if (religions.Length % 2 != 0)
        {
            GD.PushWarning($"Invalid region religious composition for region named {region.RegionName}, ignoring ...");
            return region;
        }

        for (var i = 0; i < religions.Length; i += 2)
        {
            string religionName = religions[i].Trim();
            int religionAdherence = RtwReaderUtils.IntParse(religions[i + 1].Trim());
            region.ReligiousMakeup.Add(new Tuple<string, int>(religionName, religionAdherence));
        }
        
        return region;
    }
}

}