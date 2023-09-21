using RtwFileIO;
using System.Collections.Generic;
using Godot;

namespace Model
{

	public enum Season
	{
		Summer, Winter
	}

	public class Campaign
	{
		public CampaignHeader Header => _header;
		public Scripts Scripts => _scripts;
		public Diplomacy Diplomacy => _diplomacy;

		CampaignHeader _header;
		public List<Landmark> _landmarks = new();
		public List<Resource> _resources = new();
		public List<Faction> _factions = new();
		public List<RegionLocation> _regionLocs = new();
		Diplomacy _diplomacy;
		Scripts _scripts;

		public Campaign (Strat stratData)
		{
			SetCampaignHeader(ref stratData);
			SetLandmarks(stratData.Landmarks);
			SetResources(stratData.Resources);
			SetFactions(stratData.FactionInfos);
			SetRegionLocations(stratData.RegionLocations);
            SetDiplomacy(stratData.CoreAttitudes, stratData.FactionRelationships, stratData.FactionAggressions);
			SetScripts(stratData.Scripts, stratData.SpawnScripts);
		}
	
		public List<Landmark> GetLandmarks ()
		{
			return new List<Landmark>(_landmarks);
		}
	
		public List<Resource> GetResources ()
		{
			return new List<Resource>(_resources);
		}
	
		public List<Faction> GetFactions ()
		{
			return new List<Faction>(_factions);
		}
	
		public List<string> GetAllFactionIDs ()
		{
			List<string> factionIDs = new();
			for (var i = 0; i < _factions.Count; i++)
			{
				factionIDs.Add(_factions[i].FactionID);
			}
			return factionIDs;
		}

		public Faction GetFactionByID (string factionID)
		{
			for (var i = 0; i < _factions.Count; i++)
			{
				if (_factions[i].FactionID == factionID)
				{
					return _factions[i];
				}
			}

			GD.PushWarning($"Faction with ID \"{factionID}\" was not found!");
			return null;
		}

		public List<RegionLocation> GetRegionLocations() {
			return new List<RegionLocation>(_regionLocs);
		}
	
		public void ChangeSettlementOwner (string originalOwnerID, string newOwnerID, string regionID)
		{
			Faction ownerFaction = GetFactionByID(originalOwnerID);
			Settlement settlement = ownerFaction.RemoveSettlement(regionID);
			Faction newOwnerFaction = GetFactionByID(newOwnerID);
			newOwnerFaction.AddSettlement(settlement);
		}
	
		public void SetCampaignHeader (ref Strat stratData)
		{
			_header = new CampaignHeader(stratData);
		}

		public void SetLandmarks (List<LandmarkInfo> landmarks)
		{
			for (var i = 0; i < landmarks.Count; i++)
			{
				_landmarks.Add(new Landmark(landmarks[i]));	
			}
		}

		public void SetResources (List<ResourceInfo> resources)
		{
			for (var i = 0; i < resources.Count; i++)
			{
				_resources.Add(new Resource(resources[i]));
			}
		}

        public void SetRegionLocations(List<RegionLocationsInfo> regionLocs) {
            for (var i = 0; i < regionLocs.Count; i++) {
                _regionLocs.Add(new RegionLocation(regionLocs[i]));
            }
        }

        public void SetFactions (Dictionary<string, FactionInfo> factionInfos)
		{
			foreach (FactionInfo factionInfo in factionInfos.Values)
			{
				_factions.Add(new Faction(factionInfo));
			}
		}


		public void SetDiplomacy (List<DiplomacyInfo> coreAttitudes, List<DiplomacyInfo> factionRelationships, List<DiplomacyInfo> factionAggressions)
		{
			_diplomacy = new Diplomacy(coreAttitudes, factionRelationships, factionAggressions);
		}
	
		public void SetScripts (List<ScriptInfo> scripts, List<SpawnScriptInfo> spawnScripts)
		{
			_scripts = new Scripts(scripts, spawnScripts);
		}
	}

}