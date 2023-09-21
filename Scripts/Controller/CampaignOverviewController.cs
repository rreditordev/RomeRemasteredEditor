using Model;
using System.Collections.Generic;
using RtwFileIO;
using System;
using Godot;

namespace Controller
{

public class CampaignOverviewController
{
	readonly Campaign _campaign;

	public CampaignOverviewController ()
	{
		_campaign = RtwDataContext.Campaign;
	}
	
	public CampaignHeaderDto GetHeaderDTO ()
	{
		CampaignHeader header = _campaign.Header;
		CampaignHeaderDto dto = new()
		{
			CampaignName = header.Name,
			CampaignOption = header.Option,
			PlayableFactions = header.GetPlayableFactions(),
			UnlockableFactions = header.GetUnlockableFactions(),
			NonPlayableFactions = header.GetNonPlayableFactions(),
			StartYear = header.StartYear,
			StartSeason = header.StartSeason,
			EndYear = header.EndYear,
			EndSeason = header.EndSeason,
			BrigandSpawnValue = header.BrigandSpawnValue,
			PirateSpawnValue = header.PirateSpawnValue
		};
		return dto;
	}

	public ScriptsDto GetScriptsDTO ()
	{
		return new ScriptsDto
		{
			Scripts = _campaign.Scripts.GetScripts(),
			SpawnScripts = _campaign.Scripts.GetSpawnScripts()
		};
	}

	public void EditCampaignName (string name)
	{
		_campaign.Header.SetCampaignName(name);
	}

	public void EditCampaignOption (string option)
	{
		_campaign.Header.SetCampaignOption(option);
	}

	public void EditStartYear (string startYear)
	{
		try
		{
			int year = int.Parse(startYear);
			_campaign.Header.SetStartYear(year);
		}
		catch (Exception e)
		{
			GD.PrintErr("CampaignOverviewController: Failed to parse start year." + e.Message);
		}
	}

	public void EditStartSeason (int startSeason)
	{
		if (IsValidSeason(startSeason))
		{
			_campaign.Header.SetStartSeason((Season)startSeason);	
		}
		else
		{
			GD.PrintErr("CampaignOverviewController: Failed to parse start season.");
		}
	}

	public void EditEndYear (string endYear)
	{
		try
		{
			int year = int.Parse(endYear);
			_campaign.Header.SetEndYear(year);
		}
		catch (Exception e)
		{
			GD.PrintErr("CampaignOverviewController: Failed to parse end year." + e.Message);
		}
	}

	public void EditEndSeason (int endSeason)
	{
		if (IsValidSeason(endSeason))
		{
			_campaign.Header.SetEndSeason((Season)endSeason);	
		}
		else
		{
			GD.PrintErr("CampaignOverviewController: Failed to parse end season.");
		}
	}

	public void EditBrigandSpawn (string brigandSpawnValue)
	{
		try
		{
			int spawnValue = int.Parse(brigandSpawnValue);
			_campaign.Header.SetBrigandSpawnValue(spawnValue);
		}
		catch (Exception e)
		{
			GD.PrintErr("CampaignOverviewController: Failed to parse brigand spawn value." + e.Message);
		}
	}

	public void EditPirateSpawn (string pirateSpawnValue)
	{
		try
		{
			int spawnValue = int.Parse(pirateSpawnValue);
			_campaign.Header.SetPirateSpawnValue(spawnValue);
		}
		catch (Exception e)
		{
			GD.PrintErr("CampaignOverviewController: Failed to parse pirate spawn value." + e.Message);
		}
	}
	
	bool IsValidSeason (int seasonInt)
	{
		switch ((Season) seasonInt)
		{
			case Season.Summer:
			case Season.Winter:
				return true;
			default:
				return false;
		}
	}
}

public struct ScriptsDto
{
	public List<ScriptInfo> Scripts;
	public List<SpawnScriptInfo> SpawnScripts;
}

public struct CampaignHeaderDto
{
	public string CampaignName;
	public string CampaignOption;
	public List<string> PlayableFactions;
	public List<string> UnlockableFactions;
	public List<string> NonPlayableFactions;
	public int StartYear;
	public int EndYear;
	public Season StartSeason;
	public Season EndSeason;
	public int BrigandSpawnValue;
	public int PirateSpawnValue;
}

}