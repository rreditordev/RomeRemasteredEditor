using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RtwFileIO
{

public class ExportDescrBuildingsReader
{
	ExportDescrBuildings _data;
	readonly string _filepath;
	string[] _lines;
	int _curLine = 0;

	public ExportDescrBuildingsReader (string filepath)
	{
		_filepath = filepath;
	}

	public ExportDescrBuildings Read ()
	{
		_data = new ExportDescrBuildings
		{
			Tags = new List<string>(),
			Aliases = new List<AliasDefinition>(),
			BuildingTreeDefinitions = new List<BuildingTreeDefinition>()
		};
		_lines = File.ReadAllLines(_filepath);

		ReadTags();
		ReadAliases();
		ReadBuildings();
		
		return _data;
	}

	void ReadTags ()
	{
		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment)) continue;
			
			if (line.StartsWith(Keywords.Tags))
			{
				_curLine += 2; // Skip "{" to tag IDs
				string[] split = _lines[_curLine].Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
				for (var i = 0; i < split.Length; i++)
				{
					_data.Tags.Add(split[i].Trim());
				}
			}
			else if (line.StartsWith(Keywords.Alias) || line.StartsWith(Keywords.Building))
			{
				return;
			}
		}
	}
	
	void ReadAliases ()
	{
		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment)) continue;
			
			if (line.StartsWith(Keywords.Alias))
			{
				AliasDefinition aliasDefinition = new();
				aliasDefinition.AliasID = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
				for (; _curLine < _lines.Length; _curLine++)
				{
					string aliasLine = _lines[_curLine].Trim();
					if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment)) continue;
					if (aliasLine.StartsWith(Keywords.StructEnd)) break;

					if (aliasLine.StartsWith(Keywords.Requires))
					{
						aliasDefinition.Requirements = ReadRequirements(aliasLine.Trim().Remove(0, Keywords.Requires.Length).Trim());
					}
					else if (aliasLine.StartsWith(Keywords.DisplayString))
					{
						aliasDefinition.DisplayStringID = aliasLine.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
					}
				}
				_data.Aliases.Add(aliasDefinition);
			}
			else if (line.StartsWith(Keywords.Building))
			{
				return;
			}
		}
	}
	
	void ReadBuildings ()
	{
		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment)) continue;
			
			if (line.StartsWith(Keywords.Building))
			{
				BuildingTreeDefinition tree = new();
				tree.BuildingTreeID = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1];
				tree.IsIndestructible = tree.BuildingTreeID.StartsWith(Keywords.Hinterland); // Hinterland buildings cannot be destroyed
				tree.LevelIDs = new List<string>();
				tree.Plugins = new List<string>();
				tree.BuildingLevelDefinitions = new List<BuildingLevelDefinition>();
				ReadBuildingTreeDefinition(ref tree);
				_data.BuildingTreeDefinitions.Add(tree);
			}
		}
	}

	void ReadBuildingTreeDefinition (ref BuildingTreeDefinition tree)
	{
		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment)) continue;

			if (line.StartsWith(Keywords.StructEnd)) // Building tree ends, bail out
			{
				return;
			}

			if (line.StartsWith(Keywords.Classification))
			{
				tree.Classification = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1];
			}
			else if (line.StartsWith(Keywords.BuildingTag))
			{
				tree.Tag = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1];
			}
			else if (line.StartsWith(Keywords.Icon))
			{
				tree.IconID = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1];
			}
			else if (line.StartsWith(Keywords.Levels))
			{
				tree.LevelIDs = line.Remove(0, Keywords.Levels.Length).Trim()
					.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries).ToList();
			}
			else if (tree.LevelIDs.Any(id => line.StartsWith(id))) // If a building level definitions starts here
			{
				tree.BuildingLevelDefinitions.Add(ReadBuildingLevel());
			}
			else if (line.StartsWith(Keywords.Plugins))
			{
				tree.Plugins = ReadPlugins();
			}
		}
	}

	BuildingLevelDefinition ReadBuildingLevel ()
	{
		BuildingLevelDefinition level = new()
		{
			AiDestructionRequirements = new List<RequirementDefinition>(),
			Requirements = new List<RequirementDefinition>(),
			FactionCapabilities = new List<BuildingCapability>(),
			Capabilities = new List<BuildingCapability>()
		};

		string line = _lines[_curLine].Trim();
		level.BuildingLevelID = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[0].Trim();

		string lineWithoutLevelID = line.Remove(0, level.BuildingLevelID.Length).Trim();
		if (lineWithoutLevelID.Length > 0)
		{
			level.Requirements = ReadRequirements(lineWithoutLevelID.Remove(0, Keywords.Requires.Length).Trim());
		}
		

		for (; _curLine < _lines.Length; _curLine++)
		{
			line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment)) continue;
			if (line.StartsWith(Keywords.StructEnd))
			{
				return level; // End of building tree level structure, bail out
			}

			if (line.StartsWith(Keywords.AiDestructionHint))
			{
				line = line.Remove(0, Keywords.AiDestructionHint.Length).Trim();
				level.AiDestructionRequirements = ReadRequirements(line.Remove(0, Keywords.Requires.Length).Trim());
			}
			else if (line.StartsWith(Keywords.Capability))
			{
				_curLine += 2; // Skip "capability" and "{" lines
				level.Capabilities = ReadCapabilities();
			}
			else if (line.StartsWith(Keywords.FactionCapability))
			{
				_curLine += 2; // Skip "faction_capability" and "{" lines
				level.FactionCapabilities = ReadCapabilities();
			}
			else if (line.StartsWith(Keywords.Construction))
			{
				level.ConstructionTime = int.Parse(line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1].Trim());
			}
			else if (line.StartsWith(Keywords.Cost))
			{
				level.ConstructionCost = int.Parse(line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1].Trim());
			}
			else if (line.StartsWith(Keywords.SettlementMin))
			{
				level.MinSettlementLevel =
					RtwReaderUtils.SettlementLevelParse(line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries)[1].Trim());
			}
			else if (line.StartsWith(Keywords.Upgrades))
			{
				_curLine += 2; // Skip "upgrade" and "{" lines
				level.UpgradeLevelIDs = ReadUpgrades();
			}
		}
		
		return level;
	}

	List<BuildingCapability> ReadCapabilities ()
	{
		List<BuildingCapability> capabilities = new();
		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment) || line.StartsWith(Keywords.StructStart)) continue;
			
			if (line.StartsWith(Keywords.StructEnd))
			{
				return capabilities;
			}

			int index = line.IndexOf(Keywords.Requires, StringComparison.Ordinal); // Find "require" in capability line
			if (index == -1) // No "require" found in capabilities line
			{
				capabilities.Add(new BuildingCapability()
				{
					Capability = line,
					Requirement = new List<RequirementDefinition>()
				});
			}
			else // Found requirements for capability
			{
				string requirementsLine = line.Substring(index + Keywords.Requires.Length, line.Length - (index + Keywords.Requires.Length)).Trim();
				capabilities.Add(new BuildingCapability()
				{
					Capability = line[..index].Trim(),
					Requirement = ReadRequirements(requirementsLine)
				});
			}
		}
		return capabilities;
	}
	
	List<string> ReadUpgrades ()
	{
		List<string> upgrades = new();

		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment) || line.StartsWith(Keywords.StructStart)) continue;
			
			if (line.StartsWith(Keywords.StructEnd))
			{
				return upgrades;
			}
			
			upgrades.Add(line);
		}

		return upgrades;
	}
	
	List<string> ReadPlugins ()
	{
		List<string> plugins = new();

		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment) || line.StartsWith(Keywords.StructStart)) continue;
			
			if (line.StartsWith(Keywords.StructEnd))
			{
				return plugins;
			}
			
			// Do nothing for now
		}

		return plugins;
	}

	List<RequirementDefinition> ReadRequirements (string line)
	{
		if (string.IsNullOrEmpty(line)) return new List<RequirementDefinition>();
		List<RequirementDefinition> requirements = new();

		var split = line.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
		List<int> operatorIndices = new();
		for (var i = 0; i < split.Length; i++)
		{
			if (split[i] == Keywords.And || split[i] == Keywords.Or)
			{
				operatorIndices.Add(i);
			}
		}

		if (operatorIndices.Count == 0) // Single requirement
		{
			RequirementDefinition soloRequirementDefinition = new()
			{
				Negated = split[0] == Keywords.Not,
				Condition = ""
			};

			int indexStart = soloRequirementDefinition.Negated ? 1 : 0;
			for (int i = indexStart; i < split.Length; i++)
			{
				soloRequirementDefinition.Condition += " " + split[i];
			}
			soloRequirementDefinition.Condition = soloRequirementDefinition.Condition.Trim();
			requirements.Add(soloRequirementDefinition);
		}
		else // Multiple requirements combined
		{
			var start = 0;
			for (var i = 0; i < operatorIndices.Count; i++)
			{
				BinaryOperator binaryOperator = RtwReaderUtils.OperatorParse(split[operatorIndices[i]]);
				int endIndex = operatorIndices[i] - 1;
				requirements.Add(ReadRequirement(start, endIndex, ref split, binaryOperator));
				start = operatorIndices[i] + 1;
			}
			requirements.Add(ReadRequirement(operatorIndices[^1] + 1, split.Length - 1, ref split));
		}

		return requirements;
	}

	RequirementDefinition ReadRequirement (int startIndex, int endIndex, ref string[] line, BinaryOperator binaryOp = BinaryOperator.None)
	{
		RequirementDefinition requirementDefinition = new()
		{
			Negated = false,
			Condition = ""
		};
		if (line[startIndex] == Keywords.Not)
		{
			requirementDefinition.Negated = true;
			startIndex++;
		}
		for (int i = startIndex; i <= endIndex; i++)
		{
			requirementDefinition.Condition += " " + line[i].Trim();
		}
		requirementDefinition.Condition = requirementDefinition.Condition.Trim();
		requirementDefinition.BinaryOperator = binaryOp;
		return requirementDefinition;
	}
}

}