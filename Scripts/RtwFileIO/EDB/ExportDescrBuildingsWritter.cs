﻿using Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace RtwFileIO
{

public class ExportDescrBuildingsWriter
{
	readonly string _filepath;
	readonly BuildingDefinitions _definitions;
	readonly StringWriter _writer;
	
	public ExportDescrBuildingsWriter (BuildingDefinitions definitions, string filepath)
	{
		_definitions = definitions;
		_filepath = filepath;
		_writer = new StringWriter();
	}
	
	public bool Write ()
	{
		WriteHeader();
		WriteTags();
		WriteAliases();
		WriteBuildingDefinitions();
		File.WriteAllText(Path.Combine(_filepath, "export_descr_buildings.txt"), _writer.ToString());
		return true;
	}

	void WriteHeader ()
	{
		_writer.WriteLine($"{Keywords.Comment} This file was generated by RREditor.");
		_writer.WriteLine();
	}
	
	void WriteTags ()
	{
		List<string> tags = _definitions.GetTags();
		_writer.WriteLine(Keywords.Tags);
		_writer.WriteLine(Keywords.StructStart);
		_writer.Write("\t");
		for (var i = 0; i < tags.Count; i++)
		{
			_writer.Write($"{tags[i]} ");
		}
		_writer.WriteLine();
		_writer.WriteLine(Keywords.StructEnd);
		_writer.WriteLine();
	}
	
	void WriteAliases ()
	{
		List<RequirementAlias> aliases = _definitions.GetAliases();
		for (var i = 0; i < aliases.Count; i++)
		{
			RequirementAlias alias = aliases[i];
			_writer.WriteLine($"{Keywords.Alias} {alias.AliasID}");
			_writer.WriteLine(Keywords.StructStart);
			_writer.Write("\t");
			WriteRequirements(alias.Requirements);
			_writer.WriteLine();
			_writer.WriteLine($"\t{Keywords.DisplayString} {alias.DisplayStringID}");
			_writer.WriteLine(Keywords.StructEnd);
		}
		_writer.WriteLine();
	}
	
	void WriteBuildingDefinitions ()
	{
		List<BuildingTree> trees = _definitions.GetBuildingTrees();
		for (var i = 0; i < trees.Count; i++)
		{
			BuildingTree tree = trees[i];
			_writer.WriteLine($"{Keywords.Building} {tree.BuildingTreeID}");
			_writer.WriteLine(Keywords.StructStart);
			if ( ! string.IsNullOrEmpty(tree.Classification)) _writer.WriteLine($"\t{Keywords.Classification} {tree.Classification}");
			if ( ! string.IsNullOrEmpty(tree.Tag)) _writer.WriteLine($"\t{Keywords.BuildingTag} {tree.Tag}");
			if ( ! string.IsNullOrEmpty(tree.IconID)) _writer.WriteLine($"\t{Keywords.Icon} {tree.IconID}");
			
			List<BuildingLevel> levels = tree.GetLevels();
			_writer.Write($"\t{Keywords.Levels} ");
			var levelsLine = "";
			levels.ForEach(level => levelsLine += level.BuildingLevelID + " ");
			_writer.WriteLine(levelsLine.Trim());
			_writer.WriteLine($"\t{Keywords.StructStart}");
			for (var levelIndex = 0; levelIndex < levels.Count; levelIndex++)
			{
				WriteBuildingLevel(levels[levelIndex]);
			}
			_writer.WriteLine($"\t{Keywords.StructEnd}"); // Close Levels
			_writer.WriteLine($"\t{Keywords.Plugins}");
			_writer.WriteLine($"\t{Keywords.StructStart}");
			_writer.WriteLine($"\t{Keywords.StructEnd}");
			if (tree.GetPlugins().Count != 0)
			{
				throw new ArgumentException("EdbWriter: Cannot handle writing plugins!");
			}
			_writer.WriteLine($"{Keywords.StructEnd}");  // Close building tree
		}
	}

	void WriteBuildingLevel (BuildingLevel level)
	{
		_writer.Write($"\t\t{level.BuildingLevelID}");
		if (level.Requirements.NumRequirements() != 0)
		{
			_writer.Write(" ");
			WriteRequirements(level.Requirements);
		}
		_writer.WriteLine();
		_writer.WriteLine($"\t\t{Keywords.StructStart}");

		if (level.AiDestructionRequirements.NumRequirements() != 0)
		{
			_writer.Write($"\t\t\t{Keywords.AiDestructionHint} ");
			WriteRequirements(level.AiDestructionRequirements);
			_writer.WriteLine();
		}
		
		WriteCapabilities(level.GetFactionCapabilities(), level.GetCapabilities());
		_writer.WriteLine($"\t\t\t{Keywords.Construction} {level.ConstructionTime}");
		_writer.WriteLine($"\t\t\t{Keywords.Cost} {level.ConstructionCost}");
		_writer.WriteLine($"\t\t\t{Keywords.SettlementMin} {level.MinSettlementLevel.ToRtwString()}");
		_writer.WriteLine($"\t\t\t{Keywords.Upgrades}");
		_writer.WriteLine($"\t\t\t{Keywords.StructStart}");
		List<string> upgrades = level.GetUpgradeIDs();
		for (var i = 0; i < upgrades.Count; i++)
		{
			_writer.WriteLine($"\t\t\t\t{upgrades[i]}");	
		}
		_writer.WriteLine($"\t\t\t{Keywords.StructEnd}");
		
		_writer.WriteLine($"\t\t{Keywords.StructEnd}");
	}

	void WriteCapabilities (List<Capability> factionCapabilities, List<Capability> capabilities)
	{
		_writer.WriteLine($"\t\t\t{Keywords.Capability}");
		_writer.WriteLine($"\t\t\t{Keywords.StructStart}");
		
		for (var i = 0; i < capabilities.Count; i++)
		{
			WriteCapability(capabilities[i]);
		}
		
		_writer.WriteLine($"\t\t\t{Keywords.StructEnd}");
		
		_writer.WriteLine($"\t\t\t{Keywords.FactionCapability}");
		_writer.WriteLine($"\t\t\t{Keywords.StructStart}");
		
		for (var i = 0; i < factionCapabilities.Count; i++)
		{
			WriteCapability(factionCapabilities[i]);
		}
		
		_writer.WriteLine($"\t\t\t{Keywords.StructEnd}");
	}

	void WriteCapability (Capability capability)
	{
		_writer.Write("\t\t\t\t");
		switch (capability)
		{
			case TaxBonusCapability taxBonus:
				_writer.Write($"{Keywords.TaxBonus} ");
				if (taxBonus.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{taxBonus.TaxBonus}");
				break;
			
			case TradeBaseBonusCapability tradeBaseBonus:
				_writer.Write($"{Keywords.TradeBaseBonus} ");
				if (tradeBaseBonus.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{tradeBaseBonus.TradeBonus}");
				break;
			
			case TradeLandBonusCapability tradeLandBonus:
				_writer.Write($"{Keywords.TradeLandBonus} ");
				if (tradeLandBonus.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{tradeLandBonus.TradeBonus}");
				break;
			
			case TradeFleetCapability fleet:
				_writer.Write($"{Keywords.TradeFleet} {fleet.TradeFleets}");
				break;
			
			case MineResourceCapability mine:
				_writer.Write($"{Keywords.MineResource} {mine.MiningIncomeLevel}");
				break;
			
			case FarmingLevelCapability farming:
				_writer.Write($"{Keywords.FarmingLevel} {farming.FarmingLevel}");
				break;
			
			case RoadLevelCapability road:
				_writer.Write($"{Keywords.RoadLevel} {road.RoadLevel}");
				break;
			
			case ConstructionCostBonusMilitary ccbm:
				_writer.Write($"{Keywords.ConstructionCostBonusMilitary} {ccbm.CostReduction}");
				break;
			
			case ConstructionCostBonusReligious ccbr:
				_writer.Write($"{Keywords.ConstructionCostBonusReligious} {ccbr.CostReduction}");
				break;
			
			case ConstructionCostBonusDefensive ccbd:
				_writer.Write($"{Keywords.ConstructionCostBonusDefensive} {ccbd.CostReduction}");
				break;
			
			case ConstructionCostBonusOther ccbo:
				_writer.Write($"{Keywords.ConstructionCostBonusOther} {ccbo.CostReduction}");
				break;
			
			case ConstructionTimeBonusMilitary ctbm:
				_writer.Write($"{Keywords.ConstructionTimeBonusMilitary} {ctbm.CostReduction}");
				break;
			
			case ConstructionTimeBonusReligious ctbr:
				_writer.Write($"{Keywords.ConstructionTimeBonusReligious} {ctbr.CostReduction}");
				break;
			
			case ConstructionTimeBonusDefensive ctbd:
				_writer.Write($"{Keywords.ConstructionTimeBonusDefensive} {ctbd.CostReduction}");
				break;
			
			case ConstructionTimeBonusOther ctbo:
				_writer.Write($"{Keywords.ConstructionTimeBonusOther} {ctbo.CostReduction}");
				break;
			
			case ExtraRecruitmentPoints extraRecruitment:
				_writer.Write($"{Keywords.ExtraRecruitmentPoints} {extraRecruitment.ExtraPoints}");
				break;
			
			case ExtraConstructionPoints extraConstruction:
				_writer.Write($"{Keywords.ExtraConstructionPoints} {extraConstruction.ExtraPoints}");
				break;
			
			case SettlementAgentLimit agentLimit:
				_writer.Write($"{Keywords.SettlementAgentLimit} {agentLimit.AgentType.ToRtwString()} {agentLimit.Limit}");
				break;
			
			case ReligiousOrder religiousOrder:
				_writer.Write($"{Keywords.ReligiousOrder} {religiousOrder.Order}");
				break;
			
			case HappinessBonus happiness:
				_writer.Write($"{Keywords.HappinessBonus} ");
				if (happiness.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{happiness.Bonus}");
				break;
			
			case LawBonus law:
				_writer.Write($"{Keywords.LawBonus} ");
				if (law.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{law.Bonus}");
				break;
			
			case PopGrowthBonus popGrowth:
				_writer.Write($"{Keywords.PopGrowthBonus} ");
				if (popGrowth.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{popGrowth.Bonus}");
				break;
			
			case PopHealthBonus popHealth:
				_writer.Write($"{Keywords.PopHealthBonus} ");
				if (popHealth.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{popHealth.Bonus}");
				break;
			
			case StageGames games:
				_writer.Write($"{Keywords.StageGames} {games.GamesLevel}");
				break;
			
			case StageRaces races:
				_writer.Write($"{Keywords.StageRaces} {races.RacesLevel}");
				break;
			
			case ReligiousConversion conversion:
				_writer.Write($"{Keywords.ReligiousConversion} {conversion.ReligionID} {conversion.ConversionStrength}");
				break;
			
			case Recruitment recruitment:
				_writer.Write($"{Keywords.Recruitment} \"{recruitment.UnitID}\" {recruitment.Experience}");
				break;
			
			case Agent agent:
				_writer.Write($"{Keywords.Agent} {agent.AgentType.ToRtwString()} {agent.Experience}");
				break;
			
			case ArmourUpgrade armour:
				_writer.Write($"{Keywords.ArmourUpgrade} ");
				if (armour.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{armour.ArmourValue}");
				break;
			
			case WeaponLightUpgrade weaponLight:
				_writer.Write($"{Keywords.WeaponLightUpgrade} {weaponLight.UpgradeLevel}");
				break;
			
			case WeaponHeavyUpgrade weaponHeavy:
				_writer.Write($"{Keywords.WeaponHeavyUpgrade} {weaponHeavy.UpgradeLevel}");
				break;
			
			case WeaponMissileUpgrade weaponMissile:
				_writer.Write($"{Keywords.WeaponMissileUpgrade} {weaponMissile.UpgradeLevel}");
				break;
			
			case WeaponOtherUpgrade weaponOther:
				_writer.Write($"{Keywords.WeaponOtherUpgrade} {weaponOther.UpgradeLevel}");
				break;
			
			case WallLevel wallLevel:
				_writer.Write($"{Keywords.WallLevel} {wallLevel.Level}");
				break;
			
			case GateStrength gateStrength:
				_writer.Write($"{Keywords.GateStrength} {gateStrength.Level}");
				break;
			
			case GateDefenses gateDefenses:
				_writer.Write($"{Keywords.GateDefenses} {gateDefenses.Level}");
				break;
			
			case TowerLevel towerLevel:
				_writer.Write($"{Keywords.TowerLevel} {towerLevel.Level}");
				break;
			
			case RecruitsMoraleBonus moraleBonus:
				_writer.Write($"{Keywords.RecruitsMoraleBonus} ");
				if (moraleBonus.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{moraleBonus.Bonus}");
				break;
			
			case RecruitsExperienceBonus experienceBonus:
				_writer.Write($"{Keywords.RecruitsExperienceBonus} ");
				if (experienceBonus.IsBonus) _writer.Write($"{Keywords.Bonus} ");
				_writer.Write($"{experienceBonus.Bonus}");
				break;
		}

		if (capability.Requirements.NumRequirements() != 0)
		{
			_writer.Write(" ");
			WriteRequirements(capability.Requirements);
		}
		_writer.WriteLine();
	}

	void WriteRequirements (RequirementList requirementsList)
	{
		List<Requirement> requirements = requirementsList.GetRequirements();
		if (requirements.Count == 0) return;
		List<BinaryOperator> operators = requirementsList.GetOperators();
		
		_writer.Write($"{Keywords.Requires} ");
		WriteRequirement(requirements[0]);
		for (var i = 1; i < requirements.Count; i++)
		{
			_writer.Write($" {operators[i - 1].ToRtwString()} ");
			WriteRequirement(requirements[i]);
		}
	}

	void WriteRequirement (Requirement requirement)
	{
		if (requirement.IsNegated)
		{
			_writer.Write($"{Keywords.Not} ");
		}
		switch (requirement)
		{
			case AliasedRequirement alias:
				_writer.Write(alias.AliasID);
				break;
			
			case ResourceRequirement res:
				_writer.Write($"{(res.IsHiddenResource ? Keywords.HiddenResourcePresent : Keywords.ResourcePresent)} {res.ResourceID}");
				if (res.IsFactionWide) _writer.Write($" {Keywords.FactionWide}");
				break;
			
			case BuildingPresentRequirement buildPres:
				_writer.Write($"{Keywords.BuildingPresent} {buildPres.BuildingTreeID}");
				if (buildPres.Queued) _writer.Write($" {Keywords.Queued}");
				if (buildPres.IsFactionWide) _writer.Write($" {Keywords.FactionWide}");
				break;
			
			case BuildingPresentLevelRequirement buildPressLevel:
				_writer.Write($"{Keywords.BuildingPresentLevel} {buildPressLevel.BuildingTreeID} {buildPressLevel.BuildingLevelID}");
				if (buildPressLevel.Queued) _writer.Write($" {Keywords.Queued}");
				if (buildPressLevel.IsFactionWide) _writer.Write($" {Keywords.FactionWide}");
				break;

			case MajorEventRequirement majorEvent:
				_writer.Write($"{Keywords.MajorEvent} \"{majorEvent.MajorEventID}\"");
				break;
			
			case FactionsRequirement factions:
				_writer.Write($"{Keywords.Factions} ");
				WriteFactionsStruct(factions.GetFactions());
				break;
			
			case PortRequirement:
				_writer.Write($"{Keywords.Port}");
				break;
			
			case IsPlayerRequirement:
				_writer.Write($"{Keywords.IsPlayer}");
				break;
			
			case ToggleRequirement toggle:
				_writer.Write($"{Keywords.IsToggled} \"{toggle.ToggleID}\"");
				break;
			
			case DiplomacyRequirement diplomacy:
				_writer.Write($"{Keywords.Diplomacy} {diplomacy.DiplomaticStance.ToRtwString()} {diplomacy.TargetFaction}");
				break;
			
			case BuildingFactionsRequirement bfr:
				_writer.Write($"{Keywords.BuildingFactions} ");
				WriteFactionsStruct(bfr.GetFactions());
				break;
			
			case SettlementCapabilityRequirement:
				throw new ArgumentException("EdbWriter: SettlementCapabilityRequirement is not supported");
			
			case NoBuildingTaggedRequirement nbt:
				_writer.Write($"{Keywords.NoBuildingTagged} {nbt.Tag}");
				if (nbt.Queued) _writer.Write($" {Keywords.Queued}");
				if (nbt.IsFactionWide) _writer.Write($" {Keywords.FactionWide}");
				break;
			
			case SettlementReligionRequirement srr:
				_writer.Write($"{Keywords.SettlementReligion} {srr.ReligionID} {srr.ComparisonOperator.ToRtwString()} {srr.TargetReligionInfluence}");
				break;
			
			case SettlementMajorityReligionRequirement smrr:
				_writer.Write($"{Keywords.SettlementMajorityReligion} {smrr.ReligionID}");
				break;
			
			case SettlementOfficialReligionRequirement sorr:
				_writer.Write($"{Keywords.OfficialReligion} {sorr.ReligionID}");
				break;
			
			default:
				throw new ArgumentException($"Requirement of type \"{requirement.GetType().Name}\" cannot be written!");
		}
	}

	void WriteFactionsStruct (IReadOnlyList<string> factions)
	{
		_writer.Write($"{Keywords.StructStart} ");
		for (var i = 0; i < factions.Count; i++)
		{
			_writer.Write($"{factions[i]}, ");
		}
		_writer.Write($"{Keywords.StructEnd}");
	}

}

}