using Model;
using System.Collections.Generic;

namespace Controller
{

public class CampaignFactionsController
{
	readonly Campaign _campaign;

	public CampaignFactionsController ()
	{
		_campaign = RtwDataContext.Campaign;
	}

	public FactionDto GetFaction (string factionID)
	{
		Faction faction = _campaign.GetFactionByID(factionID);
		if (faction == null) return new FactionDto();
		return new FactionDto
		{
			AIPersonality = faction.AIPersonality,
			StartingMoney = faction.StartingMoney,
			NumberOfSettlements = faction.GetNumberOfSettlements(),
			NumberOfNobles = faction.GetNumberOfNobles(),
			TotalPopulation = faction.GetTotalPopulation(),
			TotalUnits = faction.GetTotalUnits()
		};
	}

	public List<string> GetAiDoNotAttackFactions (string factionID)
	{
		Faction faction = _campaign.GetFactionByID(factionID);
		return faction.GetAiDoNotAttackFactions();
	}

	public void AddAiDoNotAttackFaction (string factionID, string aiDoNotAttackFactionID)
	{
		Faction faction = _campaign.GetFactionByID(factionID);
		faction.AddAiDoNotAttackFaction(aiDoNotAttackFactionID);
	}
	
	public void RemoveAiDoNotAttackFaction (string factionID, string aiDoNotAttackFactionID)
	{
		Faction faction = _campaign.GetFactionByID(factionID);
		faction.RemoveAiDoNotAttackFaction(aiDoNotAttackFactionID);
	}
}

public struct FactionDto
{
	public string AIPersonality;
	public int StartingMoney;
	public int NumberOfSettlements;
	public int NumberOfNobles;
	public int TotalPopulation;
	public int TotalUnits;
}

}