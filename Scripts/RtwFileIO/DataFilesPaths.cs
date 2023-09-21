using System.IO;

namespace RtwFileIO
{

public static class DataFilesPaths
{
	public static string DescrSmFactionsPath => Path.Combine(s_root, s_descrSmFactions);
	public static string DescrRegionsPath => Path.Combine(s_root, s_descrRegions);
	public static string DescrStratPath => Path.Combine(s_root, s_descrStrat);
    public static string DescrStratFolder => Path.Combine(s_root, s_descrStratFolder);
    public static string ExportDescrUnitPath => Path.Combine(s_root, s_edu);
	public static string ExportDescrBuildingsPath => Path.Combine(s_root, s_edb);
	public static string MapRegionsTgaPath => Path.Combine(s_root, s_mapRegionsTga);

	static string s_root;
	static readonly string s_descrSmFactions = "descr_sm_factions.txt";
	static readonly string s_descrStrat = Path.Combine("world", "maps", "campaign", "imperial_campaign", "descr_strat.txt");
    static readonly string s_descrStratFolder = Path.Combine("world", "maps", "campaign", "imperial_campaign");
    static readonly string s_edu = "export_descr_unit.txt";
	static readonly string s_edb = "export_descr_buildings.txt";
	static readonly string s_descrRegions = Path.Combine("world", "maps", "base", "descr_regions.txt");
	static readonly string s_mapRegionsTga = Path.Combine("world", "maps", "base", "map_regions.tga");

	public static bool SetRoot (string path)
	{
		if ( ! IsValidRootDirectory(path)) return false;
		s_root = path;
		return true;
	}

	static bool IsValidRootDirectory (string path)
	{
		var valid = true;
		valid &= File.Exists(Path.Combine(path, s_descrSmFactions));
		valid &= File.Exists(Path.Combine(path, s_edu));
		valid &= File.Exists(Path.Combine(path, s_edb));
		valid &= File.Exists(Path.Combine(path, s_descrRegions));
		valid &= File.Exists(Path.Combine(path, s_mapRegionsTga));
		valid &= File.Exists(Path.Combine(path, s_descrStrat));
		return valid;
	}
}

}