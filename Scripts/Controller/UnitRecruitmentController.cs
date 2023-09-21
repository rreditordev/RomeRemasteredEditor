using Model;
using RtwFileIO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controller
{

public enum RecruitmentType
{
	Common,
	PlayerOnly,
	AIOnly
}

public class UnitRecruitmentController
{
	public UnitRecruitmentController ()
	{
		
	}

	public List<UnitRecruitmentDto> GetRecruitableUnits (string factionID, RecruitmentType recruitmentType)
	{
		var factionCulture = "";
		List<FactionDescription> factionDescriptions = RtwDataContext.FactionDefinitions.Factions;
		for (var i = 0; i < factionDescriptions.Count; i++)
		{
			if (factionDescriptions[i].FactionID == factionID)
			{
				factionCulture = factionDescriptions[i].Culture;
				break;
			}
		}
		if (string.IsNullOrEmpty(factionCulture)) throw new ArgumentException($"Invalid faction {factionID}!");

		List<UnitRecruitmentDto> units = new();

		List<BuildingTree> trees = RtwDataContext.BuildingDefinitions.GetBuildingTrees();
		for (var i = 0; i < trees.Count; i++)
		{
			BuildingTree tree = trees[i];
			List<BuildingLevel> levels = tree.GetLevels();
			for (var j = 0; j < levels.Count; j++)
			{
				BuildingLevel level = levels[j];
				if ( ! level.Requirements.AcceptsFaction(factionID, factionCulture)) continue;
				if (recruitmentType is not RecruitmentType.Common)
				{
					if ( ! level.Requirements.AcceptsPlayer(recruitmentType is RecruitmentType.PlayerOnly)) continue;
				}
				else
				{
					if (level.Requirements.HasPlayerRequirement()) continue;
				}

				List<Recruitment> recruitments = level.GetCapabilities().OfType<Recruitment>().ToList();
				for (var p = 0; p < recruitments.Count; p++)
				{
					Recruitment recruitment = recruitments[p];
					if ( ! recruitment.Requirements.AcceptsFaction(factionID, factionCulture)) continue;
					if (recruitmentType is not RecruitmentType.Common)
					{
						if ( ! recruitment.Requirements.AcceptsPlayer(recruitmentType is RecruitmentType.PlayerOnly)) continue;
						if ( ! recruitment.Requirements.HasPlayerRequirement()) continue;
					}
					else
					{
						if (recruitment.Requirements.HasPlayerRequirement()) continue;
					}
					if (units.Any(recruitmentDto => recruitmentDto.UnitID == recruitment.UnitID)) continue;
					
					units.Add(new UnitRecruitmentDto()
					{
						UnitID = recruitment.UnitID,
						BuildingTreeID = tree.BuildingTreeID,
						MinimumLevelID = level.BuildingLevelID,
						Recruitment = recruitment,
						Tier = (int)level.MinSettlementLevel + 1
					});
				}
			}
		}
		
		return units;
	}
}

public struct UnitRecruitmentDto
{
	public string UnitID;
	public string BuildingTreeID;
	public string MinimumLevelID;
	public Recruitment Recruitment;
	public int Tier;
}

}