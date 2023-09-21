using Model;
using System.Collections.Generic;

namespace Controller
{

public class CampaignSettlementsController
{
	readonly Campaign _campaign;

	public CampaignSettlementsController ()
	{
		_campaign = RtwDataContext.Campaign;
	}

	public List<SettlementDto> GetSettlements ()
	{
		List<SettlementDto> dto = new();
		List<Faction> factions = _campaign.GetFactions();
		for (var i = 0; i < factions.Count; i++)
		{
			Faction faction = factions[i];
			List<Settlement> settlements = faction.GetSettlements();
			for (var j = 0; j < settlements.Count; j++)
			{
				Settlement settlement = settlements[j];
				dto.Add(new SettlementDto
				{
					Region = settlement.RegionID,
					Level = settlement.Level,
					StartingPopulation = settlement.StartingPopulation,
					FactionCreatorID = settlement.FactionCreatorID,
					OwnerFactionID = faction.FactionID
				});
			}
		}
		return dto;
	}

	public void EditSettlement (string originalOwnerID, string newOwnerID, SettlementDto dto)
	{
		Faction faction = _campaign.GetFactionByID(originalOwnerID);
		Settlement settlement = faction.GetSettlement(dto.Region);
		settlement.SetLevel(dto.Level);
		settlement.SetStartingPopulation(dto.StartingPopulation);
		settlement.SetFactionCreatorID(dto.FactionCreatorID);
		if (originalOwnerID != newOwnerID)
		{
			_campaign.ChangeSettlementOwner(originalOwnerID, newOwnerID, dto.Region);
		}
	}
}

public struct SettlementDto
{
	public string Region;
	public SettlementLevel Level;
	public int StartingPopulation;
	public string FactionCreatorID;
	public string OwnerFactionID;
}

}