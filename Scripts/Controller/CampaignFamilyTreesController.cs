using Model;
using System.Collections.Generic;

namespace Controller
{

public class CampaignFamilyTreesController
{
	readonly Campaign _campaign;

	public CampaignFamilyTreesController ()
	{
		_campaign = RtwDataContext.Campaign;
	}

	public Dictionary<string, FamilyTreesDto> GetFamilyTrees ()
	{
		Dictionary<string, FamilyTreesDto> familyTrees = new();
		List<Faction> factions = _campaign.GetFactions();
		for (var i = 0; i < factions.Count; i++)
		{
			Faction faction = factions[i];
			if ( ! faction.FamilyTrees.HasFamilyTree()) continue;
			FamilyTreesDto treeDto = new();
			List<FamilyTrees.Couple> founders = faction.FamilyTrees.GetFoundingCouples();
			List<CoupleDto> foundersDto = new();
			for (var j = 0; j < founders.Count; j++)
			{
				FamilyTrees.Couple founder = founders[j];
				CoupleDto coupleDto = new()
				{
					Husband = founder.Husband,
					Wife = founder.Wife,
					IsHusbandDescendant = founder.IsHusbandDescendant,
					Descendants = new List<CoupleDto>()
				};
				BuildDescendants(founder, ref coupleDto);
				foundersDto.Add(coupleDto);
			}
			treeDto.FoundingCouples = foundersDto;
			familyTrees.Add(faction.FactionID, treeDto);
		}
		return familyTrees;
	}

	public List<string> GetFactionIDsWithoutFamilyTree ()
	{
		List<string> factionIDs = new();
		List<Faction> factions = _campaign.GetFactions();
		for (var i = 0; i < factions.Count; i++)
		{
			if ( ! factions[i].FamilyTrees.HasFamilyTree())
			{
				factionIDs.Add(factions[i].FactionID);
			}
		}
		return factionIDs;
	}

	void BuildDescendants (FamilyTrees.Couple parent, ref CoupleDto parentDto)
	{
		for (var i = 0; i < parent.Descendants.Count; i++)
		{
			FamilyTrees.Couple child = parent.Descendants[i];
			CoupleDto childDto = new()
			{
				Husband = child.Husband,
				Wife = child.Wife,
				IsHusbandDescendant = child.IsHusbandDescendant,
				Descendants = new List<CoupleDto>()
			};
			BuildDescendants(child, ref childDto);
			parentDto.Descendants.Add(childDto);
		}
	}
}

public struct FamilyTreesDto
{
	public List<CoupleDto> FoundingCouples;
}

public struct CoupleDto
{
	public FamilyTrees.CharInfo Husband;
	public FamilyTrees.CharInfo Wife;
	public List<CoupleDto> Descendants;
	public bool IsHusbandDescendant;
}

}