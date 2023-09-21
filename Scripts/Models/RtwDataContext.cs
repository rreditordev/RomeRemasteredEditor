using Newtonsoft.Json;
using RtwFileIO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

namespace Model
{
	public static class RtwDataContext
	{
		public static Campaign Campaign => s_campaign;
		public static BuildingDefinitions BuildingDefinitions => s_buildingDefs;
		public static DescrSmFactions FactionDefinitions => s_descrFactions;
	
		static Campaign s_campaign;
		static UnitDefinitions s_unitDefs;
		static BuildingDefinitions s_buildingDefs;
		static DescrSmFactions s_descrFactions;
		static CampaignMap s_campaignMap;

		public static bool Load (string path)
		{
			if ( ! DataFilesPaths.SetRoot(path))
			{
				return false;
			}

			ExportDescrUnitReader eduReader = new(DataFilesPaths.ExportDescrUnitPath);
			ExportDescrUnit edu = eduReader.Read();
			s_unitDefs = new UnitDefinitions(edu);

			ExportDescrBuildingsReader edbReader = new(DataFilesPaths.ExportDescrBuildingsPath);
			ExportDescrBuildings edb = edbReader.Read();
			s_buildingDefs = new BuildingDefinitions(edb);

			//DescrRegionsReader regionsReader = new(DataFilesPaths.DescrRegionsPath);
			//DescrRegions descrRegions = regionsReader.Read();
			//s_campaignMap = new (descrRegions);

			StratReader stratReader = new (DataFilesPaths.DescrStratPath);
			Strat strat = stratReader.Read();
			s_campaign = new Campaign(strat);		

			DescrSmFactionsReader reader = new(DataFilesPaths.DescrSmFactionsPath);
			s_descrFactions = reader.Read();
		
			return true;
		}

		public static void Save ()
		{
			string stratPath = "";
			//string edbPath = "";
			bool checkDir = false;
			if (GlobalUIVariables.safeSave) {
				stratPath = Path.Combine(Directory.GetCurrentDirectory(), "output");
				//edbPath = Path.Combine(Directory.GetCurrentDirectory(), "output");
				checkDir = true;
			} else {
				stratPath = Path.Combine(GlobalUIVariables.CurrentDataPath(), DataFilesPaths.DescrStratFolder);
				//edbPath = GlobalUIVariables.CurrentDataPath();
			}
			if (checkDir && !Directory.Exists(stratPath)) {
				Directory.CreateDirectory(stratPath);
			}

			//ExportDescrBuildingsWriter edbWriter = new(s_buildingDefs, edbPath);
			//edbWriter.Write();
			StratWriter stratWriter = new(s_campaign, stratPath);
			stratWriter.Write();
		}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//------------------------------------------RESOURCE COMMANDS------------------------------------------------------//

		//Both Update and Create Command
		public static void UpsertResource(Resource resource) {
			string path;
			if (!GlobalUIVariables.safeSave)
				path = Path.Combine(GlobalUIVariables.CurrentDataPath(), DataFilesPaths.DescrStratPath);
			else if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "output/descr_strat.txt"))){
				path = Path.Combine(Directory.GetCurrentDirectory(), "output/descr_strat.txt");
			} else {
                path = Path.Combine(GlobalUIVariables.CurrentDataPath(), DataFilesPaths.DescrStratPath);
            }
            string[] arrLines = File.ReadAllLines(path);

			bool isUpdate = false;

			//Update Resource
			for(int i = 0; i < arrLines.Length; i++) {
				if (arrLines[i].Contains("resource") && arrLines[i].Contains(resource.MapPosition.X.ToString()) && arrLines[i].Contains(resource.MapPosition.Y.ToString())) {
					string replacementLine = $"resource\t\t{resource.ResourceID},              \t\t{resource.AbundanceLevel},\t\t\t {resource.MapPosition.X},  {resource.MapPosition.Y}\t\t; {resource.RegionTag}";
                    arrLines[i] = replacementLine;
					isUpdate = true;
                }
			}
            GD.Print(isUpdate);
            //Create Resource
            if (!isUpdate) {
				List<string> listLines = arrLines.ToList();
                for (int i = 0; i < arrLines.Length; i++) {
                    if (!string.IsNullOrEmpty(arrLines[i]) && !arrLines[i].Substring(0,1).Contains(";") && arrLines[i].Contains("resource")) {
                        string insertLine = $"resource\t\t{resource.ResourceID},              \t\t{resource.AbundanceLevel},\t\t\t {resource.MapPosition.X},  {resource.MapPosition.Y}\t\t; {resource.RegionTag}";
						listLines.Insert(i, insertLine);
						break;
                    }
                }
                arrLines = listLines.ToArray();
            }

			if(!GlobalUIVariables.safeSave)
				File.WriteAllLines(path, arrLines);
			else {
				if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "output"))) {
					Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "output"));
				}
				File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), "output/descr_strat.txt"), arrLines);
            }
        }
	}

}