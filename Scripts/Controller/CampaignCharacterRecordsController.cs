using Model;
using RtwFileIO;
using System.Collections.Generic;

namespace Controller
{

public class CampaignCharacterRecordsController
{
	readonly Campaign _campaign;

	public CampaignCharacterRecordsController ()
	{
		_campaign = RtwDataContext.Campaign;
	}

	public List<CharacterRecordDto> GetCharacterRecords ()
	{
		List<Faction> factions = _campaign.GetFactions();
		List<CharacterRecordDto> dto = new();
		
		for (var i = 0; i < factions.Count; i++)
		{
			Faction faction = factions[i];
			List<CharacterRecord> records = faction.FamilyTrees.GetStratCharacterRecords();
			for (var j = 0; j < records.Count; j++)
			{
				CharacterRecord record = records[j];
				dto.Add(new CharacterRecordDto()
				{
					FactionID = faction.FactionID,
					Name = record.Name,
					Gender = record.Gender,
					Age = record.Age,
					IsAlive = record.IsAlive,
					IsPastLeader = record.IsPastLeader
				});
			}
		}
		
		return dto;
	}
}

public class CharacterRecordDto
{
	public string FactionID;
	public string Name;
	public Gender Gender;
	public int Age;
	public string IsAlive;
	public string IsPastLeader;
}

}