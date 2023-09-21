using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class CampaignHeader
{
	public string Name => _campaignName;
	public string Option => _campaignOption;
	public int StartYear => _startYear;
	public int EndYear => _endYear;
	public Season StartSeason => _startSeason;
	public Season EndSeason => _endSeason;
	public int BrigandSpawnValue => _brigandSpawnValue;
	public int PirateSpawnValue => _pirateSpawnValue;
	
	string _campaignName;
	string _campaignOption;
	List<string> _playableFactions = new();
	List<string> _unlockableFactions = new();
	List<string> _nonPlayableFactions = new();
	int _startYear;
	int _endYear;
	Season _startSeason;
	Season _endSeason;
	int _brigandSpawnValue;
	int _pirateSpawnValue;
	
	public CampaignHeader (Strat stratData)
	{
		SetCampaignName(stratData.CampaignType);
		SetCampaignOption(stratData.CampaignOption);
		SetPlayableFactions(stratData.FactionsDeclaration[Keywords.Playable]);
		SetUnlockableFactions(stratData.FactionsDeclaration[Keywords.Unlockable]);
		SetNonPlayableFactions(stratData.FactionsDeclaration[Keywords.NonPlayable]);
		SetStartYear(stratData.StartYear);
		SetStartSeason(stratData.StartSeason);
		SetEndYear(stratData.EndYear);
		SetEndSeason(stratData.EndSeason);
		SetBrigandSpawnValue(stratData.BrigandSpawnValue);
		SetPirateSpawnValue(stratData.PirateSpawnValue);
	}
	
	public List<string> GetPlayableFactions ()
	{
		return new List<string>(_playableFactions);
	}
	
	public List<string> GetUnlockableFactions ()
	{
		return new List<string>(_unlockableFactions);
	}
	
	public List<string> GetNonPlayableFactions ()
	{
		return new List<string>(_nonPlayableFactions);
	}
	
	public void SetCampaignName (string campaignName)
	{
		_campaignName = campaignName;
	}

	public void SetCampaignOption (string campaignOption)
	{
		_campaignOption = campaignOption;
	}

	public void SetStartYear (int startYear)
	{
		_startYear = startYear;
	}

	public void SetStartSeason (Season startSeason)
	{
		_startSeason = startSeason;
	}
	
	public void SetEndYear (int endYear)
	{
		_endYear = endYear;
	}

	public void SetEndSeason (Season endSeason)
	{
		_endSeason = endSeason;
	}
	
	public void SetBrigandSpawnValue (int brigandSpawnValue)
	{
		_brigandSpawnValue = brigandSpawnValue;
	}
	
	public void SetPirateSpawnValue (int pirateSpawnValue)
	{
		_pirateSpawnValue = pirateSpawnValue;
	}
	
	void SetPlayableFactions (List<string> playableFactions)
	{
		_playableFactions = playableFactions;
	}
	
	void SetUnlockableFactions (List<string> unlockableFactions)
	{
		_unlockableFactions = unlockableFactions;
	}
	
	void SetNonPlayableFactions (List<string> nonPlayableFactions)
	{
		_nonPlayableFactions = nonPlayableFactions;
	}
}

}