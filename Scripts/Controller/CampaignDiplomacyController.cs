using Model;
using RtwFileIO;
using System.Collections.Generic;

namespace Controller
{

public class CampaignDiplomacyController
{
	readonly Campaign _campaign;

	public CampaignDiplomacyController ()
	{
		_campaign = RtwDataContext.Campaign;
	}

	public DiplomaticInfoDto GetDiplomaticInfo ()
	{
		return new DiplomaticInfoDto
		{
			CoreAttitudes = _campaign.Diplomacy.GetCoreAttitudes(),
			Relations = _campaign.Diplomacy.GetRelationships(),
			Aggressions = _campaign.Diplomacy.GetAggressions()
		};
	}
}

public struct DiplomaticInfoDto
{
	public List<DiplomacyInfo> CoreAttitudes;
	public List<DiplomacyInfo> Relations;
	public List<DiplomacyInfo> Aggressions;
}

}