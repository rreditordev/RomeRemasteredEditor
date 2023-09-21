using System.Collections.Generic;
using RtwFileIO;
using Godot;
using UIUtilities;

namespace Model
{
    
public class CampaignMap
{
    int _width;
    int _height;
    Image _mapRegionsTexture;
    List<Region> _regions;
    
    readonly Color32 _waterColor = new(41, 140, 233, 1);
    readonly Color32 _settlementColor = new(0, 0, 0, 1);
    readonly Color32 _portColor = new(255, 255, 255, 1);

    public CampaignMap(DescrRegions descrRegions)
    {
        _mapRegionsTexture = Image.LoadFromFile(DataFilesPaths.MapRegionsTgaPath);
        _width = _mapRegionsTexture.GetWidth();
        _height = _mapRegionsTexture.GetHeight();
        
        Dictionary<Color32, Vector2I> settlements = new();
        Dictionary<Color32, Vector2I> ports = new();
        Dictionary<Color32, List<Vector2I>> regionTiles = new();
        //ParseRegionLocations(settlements, ports, regionTiles);

        // Sanity check if there are too small regions
        foreach ((Color32 regionColor, List<Vector2I> tiles) in regionTiles)
        {
            if (tiles.Count < 2)
            {
                GD.PushWarning($"Region with ID {regionColor} is very small! Is this an error? See region origin [{tiles[0].X}, {tiles[0].Y}].");
            }
        }
        
        _regions = new List<Region>();
        foreach (DescrRegion region in descrRegions.Regions)
        {
            Vector2I capitalPos = default;
            if ( ! settlements.ContainsKey(region.RegionColor))
            {
                Vector2I pos = regionTiles[region.RegionColor][0];
                GD.PushWarning($"Region with ID {region.RegionColor}, named {region.RegionName},  does not have a capital settlement! Region origin at [{pos.X}, {pos.Y}].");
            }
            else
            {
                capitalPos = settlements[region.RegionColor];
            }

            Vector2I portPos = default; // Invalid pos to signal no port
            if (ports.ContainsKey(region.RegionColor)) // For coastal regions
            {
                portPos = ports[region.RegionColor];
            }
            _regions.Add(new Region(region, capitalPos, portPos));
        }
    }
    
    //void ParseRegionLocations(Dictionary<Color32, Vector2I> settlements, Dictionary<Color32, Vector2I> ports, 
    //    Dictionary<Color32, List<Vector2I>> regionTiles)
    //{
    //    Color32[] pixels = _mapRegionsTexture.GetPixels32(0);

    //    for (var y = 0; y < _height; y++)
    //    {
    //        for (var x = 0; x < _width; x++)
    //        {
    //            int index = y * _width + x;
    //            var pixelColor = pixels[index];
    //            if (IsRegionColor(pixelColor)) // Normal tile
    //            {
    //                if (regionTiles.ContainsKey(pixelColor))
    //                {
    //                    regionTiles[pixelColor].Add(new Vector2I(x, y));
    //                }
    //                else
    //                {
    //                    regionTiles.Add(pixelColor, new() { new Vector2I(x, y) });
    //                }
    //            }
    //            else if ( ! pixelColor.Color32Equals(_waterColor)) // If not region color nor water
    //            {
    //                // Settlements and Ports
    //                Color32 regionColor = _settlementColor;
    //                int northIndex = index + _width;
    //                int eastIndex = index + 1;
    //                int southIndex = index - _width;
    //                int westIndex = index - 1;
    //                if (northIndex < pixels.Length && IsRegionColor(pixels[northIndex]))
    //                {
    //                    regionColor = pixels[northIndex];
    //                }
    //                else if (eastIndex < pixels.Length && IsRegionColor(pixels[eastIndex]))
    //                {
    //                    regionColor = pixels[eastIndex];
    //                }
    //                else if (southIndex > 0 && IsRegionColor(pixels[southIndex]))
    //                {
    //                    regionColor = pixels[southIndex];
    //                }
    //                else if (westIndex > 0 && IsRegionColor(pixels[westIndex]))
    //                {
    //                    regionColor = pixels[westIndex];
    //                }
    //                else
    //                {
    //                    GD.PushWarning($"Failed to find port region color ID for port in coordinates [{x}, {y}]!");
    //                }

    //                Vector2I pos = new(x, y);
    //                if (pixelColor.Color32Equals(_settlementColor))
    //                {
    //                    if (settlements.ContainsKey(regionColor))
    //                    {
    //                        GD.PushWarning($"Duplicate settlement for region with color ID {regionColor} found in coordinates [{x}, {y}]!");
    //                    }
    //                    else
    //                    {
    //                        settlements.Add(regionColor, pos);
    //                    }
    //                }
    //                else
    //                {
    //                    if (ports.ContainsKey(regionColor))
    //                    {
    //                        GD.PushWarning($"Duplicate port for region with color ID {regionColor} found in coordinates [{x}, {y}]!");    
    //                    }
    //                    else
    //                    {
    //                        ports.Add(regionColor, pos);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //bool IsRegionColor(Color32 tileColor)
    //{
    //    return ! tileColor.Color32Equals(_waterColor) &&
    //           ! tileColor.Color32Equals(_settlementColor) &&
    //           ! tileColor.Color32Equals(_portColor);
    //}
    
}
    
}