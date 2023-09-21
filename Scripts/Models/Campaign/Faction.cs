using RtwFileIO;
using System.Collections.Generic;
using System.Linq;

namespace Model
{

public class Faction
{
	public string FactionID => _factionID;
	public string AIPersonality => _aiPersonality;
	public bool IsEmergent => _isEmergent;
	public int StartingMoney => _startingMoney;
	public FamilyTrees FamilyTrees => _familyTrees;

	string _factionID;
	string _aiPersonality;
	bool _isEmergent;
	List<string> _aiDoNotAttackFactions = new();
	int _startingMoney;
	List<Settlement> _settlements = new();
	List<Character> _characters = new();
	FamilyTrees _familyTrees;

	public Faction (FactionInfo stratFactionInfo)
	{
		SetFactionID(stratFactionInfo.ID);
		SetAIPersonality(stratFactionInfo.AIPersonality);
		SetIsEmergent(stratFactionInfo.IsEmergent);
		SetAIDoNotAttackFactions(stratFactionInfo.AIDoNotAttackFactions);
		SetStartingMoney(stratFactionInfo.StartingMoney);
		SetSettlements(stratFactionInfo.Settlements);
		SetCharacters(stratFactionInfo.Characters);
		SetFamilies(stratFactionInfo.CharacterRecords, stratFactionInfo.RelativesInfo);
	}

	public List<string> GetAiDoNotAttackFactions ()
	{
		return new List<string>(_aiDoNotAttackFactions);
	}
	
	public List<Settlement> GetSettlements ()
	{
		return new List<Settlement>(_settlements);
	}

	public List<Character> GetCharacters ()
	{
		return new List<Character>(_characters);
	}

	public int GetNumberOfSettlements ()
	{
		return _settlements.Count;
	}

	public int GetNumberOfNobles ()
	{
		return _characters.Count(character => character is Noble);
	}
	
	public int GetTotalPopulation ()
	{
		var totalPop = 0;
		for (var i = 0; i < _settlements.Count; i++)
		{
			totalPop += _settlements[i].StartingPopulation;
		}
		return totalPop;
	}
	
	public int GetTotalUnits ()
	{
		List<IArmyLeader> armyLeaders = _characters.OfType<IArmyLeader>().ToList();
		var totalUnits = 0;
		for (var i = 0; i < armyLeaders.Count; i++)
		{
			totalUnits += armyLeaders[i].LeadingArmy.GetNumberOfUnits();
		}
		return totalUnits;
	}
	
	public void AddAiDoNotAttackFaction (string aiDoNotAttackFactionID)
	{
		if (_aiDoNotAttackFactions.Contains(aiDoNotAttackFactionID)) return;
		_aiDoNotAttackFactions.Add(aiDoNotAttackFactionID);
	}

	public void RemoveAiDoNotAttackFaction (string aiDoNotAttackFactionID)
	{
		_aiDoNotAttackFactions.Remove(aiDoNotAttackFactionID);
	}
	
	public Settlement GetSettlement (string region)
	{
		return _settlements.Find(settlement => settlement.RegionID == region);
	}
	
	public void AddSettlement (Settlement settlement)
	{
		_settlements.Add(settlement);
	}
	
	public Settlement RemoveSettlement (string regionID)
	{
		int index = _settlements.FindIndex(settlement => settlement.RegionID == regionID);
		Settlement settlement = _settlements[index];
		_settlements.RemoveAt(index);
		return settlement;
	}

	void SetFactionID (string factionID)
	{
		_factionID = factionID;
	}

	void SetAIPersonality (string aiPersonality)
	{
		_aiPersonality = aiPersonality;
	}

	void SetIsEmergent (bool isEmergent)
	{
		_isEmergent = isEmergent;
	}

	void SetAIDoNotAttackFactions (List<string> aiDoNotAttackFactions)
	{
		_aiDoNotAttackFactions = aiDoNotAttackFactions;
	}

	void SetStartingMoney (int startingMoney)
	{
		_startingMoney = startingMoney;
	}
	
	void SetSettlements (List<SettlementInfo> settlementInfos)
	{
		for (var i = 0; i < settlementInfos.Count; i++)
		{
			Settlement settlement = new(settlementInfos[i]);
			_settlements.Add(settlement);
		}
	}
	
	void SetCharacters (List<CharacterInfo> characters)
	{
		foreach (CharacterInfo characterInfo in characters)
		{
			switch (characterInfo.Type)
			{
				case CharacterType.NamedCharacter:
					_characters.Add(string.IsNullOrEmpty(characterInfo.SubFaction) ? new Noble(characterInfo) : new SlaveLeader(characterInfo));
					break;
				
				case CharacterType.General:
				case CharacterType.Admiral:
					_characters.Add(new Captain(characterInfo));
					break;
				
				case CharacterType.Spy:
				case CharacterType.Diplomat:
				case CharacterType.Assassin:
				case CharacterType.Merchant:
					_characters.Add(new Notable(characterInfo));
					break;
			}
		}
	}
	
	void SetFamilies (List<CharacterRecord> characterRecords, List<RelativeInfo> relativesInfo)
	{
		List<Noble> familyMembers = _characters.OfType<Noble>().ToList();
		_familyTrees = new FamilyTrees(familyMembers, characterRecords, relativesInfo);
	}
}

}