using RtwFileIO;
using System.Collections.Generic;
using Godot;
using CharacterInfo = RtwFileIO.CharacterInfo;

namespace Model
{

	public enum CharacterType
	{
		NamedCharacter,
		General,
		Admiral,
		Spy,
		Diplomat,
		Assassin,
		Merchant
	}

	public enum Gender
	{
		male,
		female
	}

	/// <summary>
	/// Base class of all characters present on the map.
	/// </summary>
	public abstract class Character
	{
		public string Name => _name;
		public int Age => _age;
		public Vector2I MapPosition => _mapPosition;
		public CharacterType Type => _characterType;
		public int Command => _command;
		public int Influence => _influence;
		public int Management => _management;
		public int Subterfuge => _subterfuge;
		public string Comments => _comments;

			string _name;
		int _age;
		Vector2I _mapPosition;
		CharacterType _characterType;
		public int _command;
		public int _influence;
		public int _management;
		public int _subterfuge;
		public string _comments;

		protected Character (CharacterInfo characterInfo)
		{
			SetName(characterInfo.Name);
			SetAge(characterInfo.Age);
			SetMapPosition(characterInfo.MapPosition);
			SetCharacterType(characterInfo.Type);
			SetCommand(characterInfo.command);
			SetInfluence(characterInfo.influence);
			SetManagement(characterInfo.management);
			SetSubterfuge(characterInfo.subterfuge);
			SetComments(characterInfo.comments);

        }

		void SetName (string name)
		{
			_name = name;
		}

		void SetAge (int age)
		{
			_age = age;
		}

		void SetMapPosition (Vector2I mapPosition)
		{
			_mapPosition = mapPosition;
		}
	
		void SetCharacterType (CharacterType characterInfoType)
		{
			_characterType = characterInfoType;
		}

        void SetCommand(int command) {
            _command = command;
        }

        void SetInfluence(int influence) {
            _influence = influence;
        }

        void SetManagement(int managemet) {
            _management = managemet;
        }

        void SetSubterfuge(int subterfuge) {
            _subterfuge = subterfuge;
        }

        void SetComments(string comments) {
            _comments = comments;
        }
    }

	/// <summary>
	/// Class representing generals and admirals who are NOT family members.
	/// They command an army but have neither traits nor ancillaries.
	/// </summary>
	public class Captain : Character, IArmyLeader
	{
		public Army LeadingArmy => _army;
		Army _army;
	
		public Captain (CharacterInfo characterInfo) : base(characterInfo)
		{
			SetArmy(characterInfo.LeadingArmy);
		}

		void SetArmy (List<UnitInfo> unitInfoList)
		{
			_army = new Army(unitInfoList);
		}
	}

	/// <summary>
	/// Base class of all characters present on the map which have traits and ancillaries.
	/// </summary>
	public class Notable : Character
	{
		public TraitList TraitList => _traits;
		public AncillariesList AncillariesList => _ancillaries;
	
		TraitList _traits;
		AncillariesList _ancillaries;
	
		public Notable (CharacterInfo characterInfo) : base(characterInfo)
		{
			SetTraits(characterInfo.Traits);
			SetAncillaries(characterInfo.Ancillaries);
		}
	
		void SetTraits (Dictionary<string, int> traits)
		{
			_traits = new TraitList(traits);
		}

		void SetAncillaries (List<string> ancillaries)
		{
			_ancillaries = new AncillariesList(ancillaries);
		}
	}

	/// <summary>
	/// Family members. They are Notable (have traits and ancillaries) but unlike
	/// them also are at the command of an army while also having the possibility
	/// of being faction leader or heir.
	/// </summary>
	public class Noble : Notable, IArmyLeader
	{
		public Rank NobleRank => _rank;
		public Army LeadingArmy => _army;
	
		public enum Rank
		{
			None,
			Heir,
			Leader
		}

		Rank _rank;
		Army _army;
	
		public Noble (CharacterInfo characterInfo) : base(characterInfo)
		{
			_rank = characterInfo.FactionLeadership;
			_army = new Army(characterInfo.LeadingArmy);
		}
	}

	/// <summary>
	/// Family members of the slave faction have the extra sub-faction modifier.
	/// </summary>
	public class SlaveLeader : Noble
	{
		public string SubFactionID => _subFactionID;
		string _subFactionID;

		public SlaveLeader (CharacterInfo characterInfo) : base(characterInfo)
		{
			SetSubFaction(characterInfo.SubFaction);
		}
	
		void SetSubFaction (string subFactionID)
		{
			_subFactionID = subFactionID;
		}
	}

	public interface IArmyLeader
	{
		public Army LeadingArmy { get; }
	}

}