using Model;
using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace RtwFileIO
{
	public class StratReader
	{
		readonly string _filepath;
		Strat _data;
		string[] _lines;
		int _curLine = 0;

		public StratReader (string filepath)
		{
			_filepath = filepath;
		}

		public Strat Read ()
		{
			_lines = File.ReadAllLines(_filepath);
			ReadCampaignDeclaration();
			ReadCampaignOption();
			ReadFactionsList(Keywords.Playable);
			ReadFactionsList(Keywords.Unlockable);
			ReadFactionsList(Keywords.NonPlayable);
			ReadStartEndDates();
			ReadSpawnValues();
			ReadLandmarks();
			ReadResources();
			ReadFactions();
			ReadDiplomacy();
			ReadRegionLocationInfo();
			ReadScripts();
			return _data;
		}

		void ReadScripts ()
		{
			_data.SpawnScripts = new List<SpawnScriptInfo>();
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine].Trim();
				if (line.StartsWith(Keywords.Script))
				{
					break;
				}
				if ( ! line.StartsWith(Keywords.SpawnScript))
				{
					continue;
				}
			
				string[] temp = line.Remove(0, Keywords.SpawnScript.Length).Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
				SpawnScriptInfo spawnScript = new()
				{
					Faction = temp[0].Trim(),
					Event = temp[1].Trim(),
					ScriptPath = temp[2].Trim()
				};
				_data.SpawnScripts.Add(spawnScript);
			}

			_curLine++; // Ignore "script" line
			_data.Scripts = new List<ScriptInfo>();
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine].Trim();
				if (line.StartsWith(Keywords.Comment))
				{
					continue;
				}
				string[] temp = line.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
				ScriptInfo script = new()
				{
					ScriptPath = temp[0],
					Options = ""
				};
				if (temp.Length > 1)
				{
					script.Options = temp[1];
				}
				_data.Scripts.Add(script);
			}
		}

		void ReadRegionLocationInfo() {
			_data.RegionLocations = new List<RegionLocationsInfo>();
			for (; _curLine < _lines.Length - 1; _curLine++) {
				string line = _lines[_curLine].Trim();
				string nextLine = _lines[_curLine + 1].Trim();
                //You have gone past the Region section with no data found!
                if (line.StartsWith(Keywords.SpawnScript) || line.StartsWith(Keywords.Script)) {
					break;
				}

				if (line.StartsWith(Keywords.Region)) {
					string[] regionData = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
					string[] locAndTypeData = nextLine.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
					string comment = "";

					//Get any comments placed above info
					int i = _curLine - 1;
					Stack<string> stack = new Stack<string>();
					while (_lines[i].Trim().StartsWith(";")) {
						stack.Push("\n" + _lines[i]);
						i--;
					}

					while (stack.Count > 0) {
						comment += stack.Pop();
					}

					string strX = locAndTypeData[1];
					string strY = locAndTypeData[2];

					if (strX[strX.Length - 1] == ',') {
						strX = strX.Substring(0,strX.Length - 1);
					}


                    RegionLocationsInfo regionInfo = new() {
						comments = comment,
						regionName = regionData[1],
						MapPosition = new Vector2I(int.Parse(strX), int.Parse(strY)),
						objectType = locAndTypeData[0]
					};
					_data.RegionLocations.Add(regionInfo);
				}
			}
		}

			void ReadCampaignOption ()
		{
			for (; _curLine < _lines.Length; _curLine++)
			{
				if (_lines[_curLine].StartsWith(Keywords.Playable)) return; // No campaign option present
				if ( ! _lines[_curLine].StartsWith(Keywords.Options)) continue;
				_data.CampaignOption = _lines[_curLine].Remove(0, Keywords.Options.Length).Trim();
				break;
			}
		}

		void ReadDiplomacy ()
		{
			_data.CoreAttitudes = new List<DiplomacyInfo>();
			_data.FactionRelationships = new List<DiplomacyInfo>();
			_data.FactionAggressions = new List<DiplomacyInfo>();
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine].Trim();
                if (line.StartsWith(Keywords.CoreAttitudes))
				{
					_data.CoreAttitudes.Add(ReadDiplomacyInfo(Keywords.CoreAttitudes));
				}
				else if (line.StartsWith(Keywords.FactionRelationships))
				{
					_data.FactionRelationships.Add(ReadDiplomacyInfo(Keywords.FactionRelationships));
				}
				else if (line.StartsWith(Keywords.FactionAggression))
				{
					_data.FactionAggressions.Add(ReadDiplomacyInfo(Keywords.FactionAggression));
				}
				else if (line.StartsWith(Keywords.SpawnScript) || line.StartsWith(Keywords.Script) || line.Contains(Keywords.Region))
				{
					break;
				}
			}
		}

		DiplomacyInfo ReadDiplomacyInfo (string key)
		{
			DiplomacyInfo info = new()
			{
				FactionTargets = new List<string>()
			};
			string line = _lines[_curLine].Trim();
			string[] split = line.Remove(0, key.Length).Trim().Replace(",", "").Replace("\t", " ")
				.Split(" ", StringSplitOptions.RemoveEmptyEntries);

			info.FactionSource = split[0];
			if (split[1].StartsWith(Keywords.AlliedTo))
			{
				info.DiplomaticValue = 0;
			}
			else if (split[1].StartsWith(Keywords.AtWarWith))
			{
				info.DiplomaticValue = 600;
			}
			else
			{
				info.DiplomaticValue = RtwReaderUtils.IntParse(split[1]);
			}
			info.FactionTargets = new List<string>();
			for (var i = 2; i < split.Length; i++)
			{
				info.FactionTargets.Add(split[i]);
			}
			return info;
		}
	
		void ReadFactions ()
		{
			_data.FactionInfos = new Dictionary<string, FactionInfo>();
			int numberOfFactions = _data.FactionsDeclaration[Keywords.Playable].Count + _data.FactionsDeclaration[Keywords.Unlockable].Count 
																					+ _data.FactionsDeclaration[Keywords.NonPlayable].Count;
			for (var i = 0; i < numberOfFactions; i++)
			{
				ReadFaction();
			}
		}

		void ReadFaction ()
		{
			FactionInfo faction = new()
			{
				AIDoNotAttackFactions = new List<string>(),
				Settlements = new List<SettlementInfo>(),
				Characters = new List<CharacterInfo>(),
				CharacterRecords = new List<CharacterRecord>(),
				RelativesInfo = new List<RelativeInfo>()
			};

			string[] firstLine = _lines[_curLine].Remove(0, Keywords.Faction.Length).Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
			faction.ID = firstLine[0].Trim();
			faction.AIPersonality = firstLine[1].Trim();
			_curLine++; // We already read the first line
		
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine];
				if (line.StartsWith(Keywords.Faction) || line.StartsWith(Keywords.CoreAttitudes)) // Reached next faction / reached diplomacy
				{
					break;
				}

				if (line.StartsWith(Keywords.Settlement))
				{
					faction.Settlements.Add(ReadSettlement());
				}
				else if (line.StartsWith(Keywords.CharacterRecord)) // This has to be checked before characterKey or else character_records will enter the character condition
				{
					faction.CharacterRecords.Add(ReadCharacterRecord());
				}
				else if (line.StartsWith(Keywords.Character))
				{
					faction.Characters.Add(ReadCharacter());
				}
				else if (line.StartsWith(Keywords.Relative))
				{
					faction.RelativesInfo.Add(ReadRelative());
				}
				else if (line.StartsWith(Keywords.AiDoNotAttack))
				{
					string[] noAttackLine = line.Remove(0, Keywords.AiDoNotAttack.Length).Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
					foreach (string noAttackFaction in noAttackLine)
					{
						faction.AIDoNotAttackFactions.Add(noAttackFaction.Trim());
					}
				}
				else if (line.StartsWith(Keywords.Denari))
				{
					faction.StartingMoney = RtwReaderUtils.IntParse(line.Remove(0, Keywords.Denari.Length).Trim());
				}
				else if (line.StartsWith(Keywords.EmergentFaction))
				{
					faction.IsEmergent = true;
				}
			}
			_data.FactionInfos[faction.ID] = faction;
		}

		RelativeInfo ReadRelative ()
		{
			RelativeInfo relative = new()
			{
				OffspringNames = new List<string>()
			};
		
			string[] line = _lines[_curLine].Trim().Remove(0, Keywords.Relative.Length).Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
			for (var i = 0; i < line.Length; i++)
			{
				string parameter = line[i].Trim();
				if (i is 0) // first name is always character name
				{
					relative.Name = parameter;
				}
				else if (i is 1) // second name is always wife name
				{
					relative.WifeName = parameter;
				}
				else if (i == line.Length - 1) // last element exit loop ("end")
				{
					break;
				}
				else // All other elements are the names of the character's children
				{
					relative.OffspringNames.Add(parameter);
				}
			}

			return relative;
		}

		CharacterRecord ReadCharacterRecord ()
		{
			CharacterRecord record = new()
			{
				Traits = new Dictionary<string, int>()
			};
			string[] line = _lines[_curLine].Trim().Remove(0, Keywords.CharacterRecord.Length).Trim().Split(",", StringSplitOptions.RemoveEmptyEntries);
			for (var i = 0; i < line.Length; i++)
			{
				string parameter = line[i].Trim();
				switch (i)
				{
					// name is always first
					case 0:
						record.Name = parameter;
						break;

					// gender is always second
					case 1:
						record.Gender = RtwReaderUtils.GenderParse(parameter);
						break;

					default:
					{
						if (parameter.StartsWith(Keywords.Age))
						{
							record.Age = RtwReaderUtils.IntParse(parameter.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
						}
						else if (parameter.StartsWith(Keywords.Dead) || parameter.StartsWith(Keywords.Alive))
						{
							string[] split = parameter.Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
							if (split.Length > 1)
							{
								record.DeathRelativeToStart = RtwReaderUtils.IntParse(split[1]);
							}
							record.IsAlive = RtwReaderUtils.NonNumericParse(split[0]);
						}
						else if (parameter.StartsWith(Keywords.PastLeader) || parameter.StartsWith(Keywords.NeverALeader))
						{
							if (record.IsAlive is Keywords.Dead)
							{
								record.IsPastLeader = RtwReaderUtils.NonNumericParse(parameter);
							}
							else
							{
								record.IsPastLeader = "";
							}
						}
						break;
					}
				}
			}
		
			// If it exists then read traits list of this character record
			if (_lines[_curLine + 1].StartsWith(Keywords.Traits)) 
			{
				_curLine++; // Walk into this trait line
				string[] traits = _lines[_curLine].Remove(0, Keywords.Traits.Length).Split(",", StringSplitOptions.RemoveEmptyEntries);
				foreach (string trait in traits)
				{
					string[] split = trait.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
					string traitName = split[0].Trim();
					int traitLevel = split.Length > 1 ? RtwReaderUtils.IntParse(split[1].Trim()) : 0;
					record.Traits.Add(traitName, traitLevel);
				}
			}
			return record;
		}

		CharacterInfo ReadCharacter ()
		{
            //Get any comments placed above character
            string comments = "";
            int j = _curLine - 1;
            Stack<string> stack = new Stack<string>();
            while (_lines[j].Trim().StartsWith(";")) {
                stack.Push("\n" + _lines[j]);
                j--;
            }

            while (stack.Count > 0) {
                comments += stack.Pop();
            }

            CharacterInfo character = new()
			{
				SubFaction = "",
				FactionLeadership = Noble.Rank.None,
				Traits = new Dictionary<string, int>(),
				Ancillaries = new List<string>(),
				LeadingArmy = new List<UnitInfo>(),
				comments = comments
			};
            //bool test = false;
            string[] parameterLine = _lines[_curLine].Remove(0, Keywords.Character.Length).Split(",", StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < parameterLine.Length; i++)
			{


				string arg = parameterLine[i].Trim();
				if (arg.StartsWith(Keywords.SubFaction))
				{
					character.SubFaction = arg.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
					i++;
					character.Name = parameterLine[i].Trim();
				}
				else if (i is 0) // If first argument AND not started with sub_faction = character name
				{
					character.Name = arg;
					//Ugrasena
				}
				else if (i is 1 or 2 && arg is Keywords.NamedCharacterSpace or Keywords.NamedCharacterUnderscore or Keywords.General or Keywords.Admiral or 
							Keywords.Diplomat or Keywords.Spy or Keywords.Assassin)
				{
					character.Type = RtwReaderUtils.CharacterTypeParse(arg);
                }
				else if (arg is Keywords.Leader or Keywords.Heir)
				{
					character.FactionLeadership = RtwReaderUtils.NobleRankParse(arg);
                }
				else if (arg.StartsWith(Keywords.Age))
				{
					character.Age = RtwReaderUtils.IntParse(arg.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]);
                }
				else if (arg.StartsWith(Keywords.XCoord))
				{
					character.MapPosition.X = RtwReaderUtils.IntParse(arg);
				}
				else if (arg.StartsWith(Keywords.YCoord))
				{
					character.MapPosition.Y = RtwReaderUtils.IntParse(arg);
				}else if (arg.StartsWith(Keywords.Command)) {
					character.command = RtwReaderUtils.IntParse(arg);
				}else if (arg.StartsWith(Keywords.Influence)) {
                    character.influence = RtwReaderUtils.IntParse(arg);
                }else if (arg.StartsWith(Keywords.Management)) {
                    character.management = RtwReaderUtils.IntParse(arg);
                }else if (arg.StartsWith(Keywords.Subterfuge)) {
                    character.subterfuge = RtwReaderUtils.IntParse(arg);
                }
			}

			_curLine++; // Go over the character line to start reading traits, ancillaries and armies (if any)
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine];
				if (line.StartsWith(Keywords.Comment)) continue;
			
				if (line.StartsWith(Keywords.Traits))
				{
					string[] traits = line.Remove(0, Keywords.Traits.Length).Split(",", StringSplitOptions.RemoveEmptyEntries);
					foreach (string trait in traits)
					{
						string[] split = trait.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
						if (split.Length > 0) {
							string traitName = split[0].Trim();
							int traitLevel = split.Length > 1 ? RtwReaderUtils.IntParse(split[1].Trim()) : 0;
							if (!character.Traits.ContainsKey(traitName))
								character.Traits.Add(traitName, traitLevel);
						}
					}
				}
				else if (line.StartsWith(Keywords.Ancillaries))
				{
					string[] ancillaries = line.Remove(0, Keywords.Ancillaries.Length).Split(",", StringSplitOptions.RemoveEmptyEntries);
                    foreach (string ancillary in ancillaries)
					{
						character.Ancillaries.Add(ancillary.Trim());
					}
				}
				else if (line.StartsWith(Keywords.Army))
				{
					_curLine++;
					for (; _curLine < _lines.Length; _curLine++)
					{
                        line = _lines[_curLine].Trim();
						
						if (line.Contains(";unit")) {
							continue; 
						}
                        if ( ! line.StartsWith(Keywords.Unit))
						{
							return character;
						}

						UnitInfo unit = new();
						string[] nameSplit = line.Remove(0, Keywords.Unit.Length).Trim().Split("\t", StringSplitOptions.RemoveEmptyEntries);
						unit.UnitName = nameSplit[0].Trim();
						if (nameSplit.Length > 1) // See if any unit stats are defined, if missing all 0s is assumed
						{
							string[] statsSplit = nameSplit[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
							if (statsSplit.Length > 1)
							{
								unit.Experience = RtwReaderUtils.IntParse(statsSplit[1]);
							}
							if (statsSplit.Length > 3)
							{
								unit.ArmourLevel = RtwReaderUtils.IntParse(statsSplit[3]);
							}
							if (statsSplit.Length > 5)
							{
								unit.WeaponLevel = RtwReaderUtils.IntParse(statsSplit[5]);
							}
						}

						character.LeadingArmy.Add(unit);
					}
				}
				else // If none of the above stop loop
				{
					return character;
				}
			}
		
			return character;
		}

		SettlementInfo ReadSettlement ()
		{
			SettlementInfo settlement = new()
			{
				BuildingInfos = new List<BuildingInfo>()
			};
			_curLine++; // Ignore settlement opening "{" line
			for (; _curLine < _lines.Length; _curLine++)
			{
				if (_lines[_curLine].StartsWith(Keywords.StructEnd))
				{
					break;
				}

				string line = _lines[_curLine].Trim();
				if (line.StartsWith(Keywords.Building))
				{
					settlement.BuildingInfos.Add(ReadBuilding());
				}
				else if (line.StartsWith(Keywords.Level))
				{
					settlement.Level = RtwReaderUtils.SettlementLevelParse(line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim());
				}
				else if (line.StartsWith(Keywords.Region))
				{
					settlement.Region = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim();
				}
				else if (line.StartsWith(Keywords.Population))
				{
					settlement.StartingPopulation = RtwReaderUtils.IntParse(line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim());
				}
				else if (line.StartsWith(Keywords.PlanSet))
				{
					settlement.PlanSetName = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim();
				}
				else if (line.StartsWith(Keywords.FactionCreator))
				{
					settlement.FactionCreator = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim();
				}
			}
			return settlement;
		}

		BuildingInfo ReadBuilding ()
		{
			BuildingInfo building = new();
			_curLine += 2; // Ignore 2 starting lines -> "building" and "{"
			string[] line = _lines[_curLine].Trim().Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
			building.BuildingTreeName = line[1].Trim();
			building.BuildingLevelName = line[2].Trim();
			return building;
		}

		void ReadResources ()
		{
			_data.Resources = new List<ResourceInfo>();
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine].Trim();
				if (line.StartsWith(Keywords.ResourceQuantityEnabled) || line.StartsWith(Keywords.ResourceQuantityDisabled))
				{
					GD.PushWarning("Resource quantity enabled/disabled is not supported. Ignoring");
					continue; // Not supported for now
				}
				else if (line.StartsWith(Keywords.Resource))
				{
					ResourceInfo resource = new();
					string[] split = line.Replace("\t", "").Remove(0, Keywords.Resource.Length).Trim()
						.Split(",", StringSplitOptions.RemoveEmptyEntries);
					resource.ID = split[0];
					resource.AbundanceLevel = RtwReaderUtils.IntParse(split[1]);
					resource.MapPosition.X =  RtwReaderUtils.IntParse(split[2]);
					string[] posYandRegion = split[3].Trim().Split(Keywords.Comment, StringSplitOptions.RemoveEmptyEntries);
					resource.MapPosition.Y = RtwReaderUtils.IntParse(posYandRegion[0].Trim());
					if (posYandRegion.Length > 1)
					{
						resource.RegionTag = posYandRegion[1].Trim();
					}
					_data.Resources.Add(resource);
				}
				else if (_lines[_curLine].StartsWith(Keywords.Faction))
				{
					break;
				}
			}
		}

		void ReadLandmarks ()
		{
			_data.Landmarks = new List<LandmarkInfo>();
			for (; _curLine < _lines.Length; _curLine++)
			{
				if (_lines[_curLine].StartsWith(Keywords.Landmark))
				{
					LandmarkInfo landmark = new();
					string[] line = _lines[_curLine].Remove(0, Keywords.Landmark.Length).Trim().Split(RtwReaderUtils.Whitespace, StringSplitOptions.RemoveEmptyEntries);
					landmark.ID = line[0].Split("\t", StringSplitOptions.RemoveEmptyEntries)[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
					landmark.MapPosition.X = RtwReaderUtils.IntParse(line[1].Split(",", StringSplitOptions.RemoveEmptyEntries)[0]);
					landmark.MapPosition.Y = RtwReaderUtils.IntParse(line[2]);
					_data.Landmarks.Add(landmark);
				}
				else if (_lines[_curLine].StartsWith(Keywords.Resource))
				{
					break;
				}
			}
		}

		void ReadSpawnValues ()
		{
			var foundBrigand = false;
			var foundPirate = false;
		
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine];
				if (line.StartsWith(Keywords.BrigandSpawn))
				{
					string brigandSpawnValue = line.Remove(0, Keywords.BrigandSpawn.Length).Trim();
					_data.BrigandSpawnValue = RtwReaderUtils.IntParse(brigandSpawnValue);
					foundBrigand = true;
				}
				else if (line.StartsWith(Keywords.PirateSpawn))
				{
					string pirateSpawnValue = line.Remove(0, Keywords.PirateSpawn.Length).Trim();
					_data.PirateSpawnValue = RtwReaderUtils.IntParse(pirateSpawnValue);
					foundPirate = true;
				}
				if (foundBrigand && foundPirate) break;
			}
		}

		void ReadStartEndDates ()
		{
			var foundStart = false;
			var foundEnd = false;
			for (; _curLine < _lines.Length; _curLine++)
			{
				string line = _lines[_curLine];
				if (line.StartsWith(Keywords.StartDate))
				{
					string[] start = line.Remove(0, Keywords.StartDate.Length).Trim().Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
					_data.StartYear = RtwReaderUtils.IntParse(start[0].Trim()); 
					_data.StartSeason = RtwReaderUtils.SeasonParse(start[1].Trim());
					foundStart = true;
				}
				else if (line.StartsWith(Keywords.EndDate))
				{
					string[] end = line.Remove(0, Keywords.EndDate.Length).Trim().Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
					_data.EndYear = RtwReaderUtils.IntParse(end[0].Trim()); 
					_data.EndSeason = RtwReaderUtils.SeasonParse(end[1].Trim());
					foundEnd = true;
				}
				if (foundStart && foundEnd) break;
			}
		}

		void ReadCampaignDeclaration ()
		{
			for (_curLine = 0; _curLine < _lines.Length; _curLine++)
			{
				if ( ! _lines[_curLine].StartsWith(Keywords.Campaign)) continue;
				_data.CampaignType = _lines[_curLine].Remove(0, Keywords.Campaign.Length).Trim();
				break;
			}
		}

		void ReadFactionsList (string playabilityTag)
		{
			_data.FactionsDeclaration ??= new Dictionary<string, List<string>>();
			for (; _curLine < _lines.Length; _curLine++)
			{
				if ( ! _lines[_curLine].StartsWith(playabilityTag)) continue;
			
				_data.FactionsDeclaration[playabilityTag] = new List<string>();
				do
				{
					_curLine++;
					string faction = _lines[_curLine].Trim();
					if (faction == Keywords.End)
					{
						return;
					}
					if ( ! string.IsNullOrEmpty(faction))
					{
						_data.FactionsDeclaration[playabilityTag].Add(faction);
					}
				} while (true);
			}
		}
	}

}