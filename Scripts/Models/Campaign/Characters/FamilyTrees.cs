using RtwFileIO;
using System.Collections.Generic;
using System.Linq;
using System;
using Godot;

namespace Model
{

public class FamilyTrees
{
	readonly List<Couple> _families = new();
	readonly List<Noble> _familyMembers;
	readonly List<CharacterRecord> _records;
	readonly List<RelativeInfo> _relatives;

	public FamilyTrees (List<Noble> familyMembers, List<CharacterRecord> records, List<RelativeInfo> relatives)
	{
		_familyMembers = familyMembers;
		_records = records;
		_relatives = relatives;
		BuildFamilies();
	}

	public List<Couple> GetFoundingCouples ()
	{
		List<Couple> foundingCouples = new();
		for (var i = 0; i < _families.Count; i++)
		{
			foundingCouples.Add(_families[i]);
		}
		return foundingCouples;
	}

	public bool HasFamilyTree ()
	{
		return _relatives.Count > 0;
	}

	public List<CharacterRecord> GetStratCharacterRecords ()
	{
		return new List<CharacterRecord>(_records);
	}

	public List<RelativeInfo> GetStratRelativeInfo ()
	{
		return new List<RelativeInfo>(_relatives);
	}

	void BuildFamilies ()
	{
		foreach (RelativeInfo relativeInfo in _relatives)
		{
			if (PlaceCouple(relativeInfo)) continue;
			
			// If not placed inside a preexisting family then create new family
			Couple couple = new()
			{
				Husband = FindCharInfo(relativeInfo.Name),
				Wife = FindCharInfo(relativeInfo.WifeName),
				Descendants = InitChildren(relativeInfo.OffspringNames)
			};
			_families.Add(couple);
		}
	}

	bool PlaceCouple (RelativeInfo relativeInfo)
	{
		for (var i = 0; i < _families.Count; i++)
		{
			Couple foundingCouple = _families[i];
			if (PlaceCoupleInTree(foundingCouple, relativeInfo))
			{
				return true;
			}
		}
		return false;
	}

	bool PlaceCoupleInTree (Couple curCouple, RelativeInfo relative)
	{
		bool relativeIsFather = relative.OffspringNames.Any(offspring => offspring == curCouple.Husband.Name || offspring == curCouple.Wife.Name);
		if (relativeIsFather)
		{
			GD.PushWarning($"Couple is placed as a parent! {relative.Name}, {relative.WifeName}. Not accepted.");
			return false; // Logic for placing a couple AS PARENT is not implemented
		}
		
		bool relativeIsChild = curCouple.Descendants.Any(descendant => descendant.Husband.Name == relative.Name || descendant.Wife.Name == relative.WifeName);
		if (relativeIsChild)
		{
			Couple couple = new()
			{
				Husband = FindCharInfo(relative.Name),
				Wife = FindCharInfo(relative.WifeName),
				Descendants = InitChildren(relative.OffspringNames)
			};
			curCouple.UpdateDescendant(couple);
			return true;
		}
		
		for (var i = 0; i < curCouple.Descendants.Count; i++)
		{
			if (PlaceCoupleInTree(curCouple.Descendants[i], relative))
			{
				return true;
			}
		}

		return false;
	}

	CharInfo FindCharInfo (string name)
	{
		foreach (CharacterRecord characterRecord in _records)
		{
			if (characterRecord.Name == name)
			{
				CharInfo info = new()
				{
					Name = name,
					Age = characterRecord.Age,
					Gender = characterRecord.Gender,
					IsAlive = characterRecord.IsAlive is "alive",
					Rank = characterRecord.IsPastLeader is "past_leader" ? Noble.Rank.Leader : Noble.Rank.None
				};
				return info;
			}
		}
		
		foreach (Noble noble in _familyMembers)
		{
			if (noble.Name == name)
			{
				CharInfo info = new()
				{
					Name = name,
					Age = noble.Age,
					Gender = Gender.male,
					IsAlive = true,
					Rank = noble.NobleRank
				};
				return info;
			}
		}

		throw new ArgumentException($"Error: Failed to find character info for name \"{name}\" when building family tree.");
	}

	List<Couple> InitChildren (List<string> children)
	{
		List<Couple> descendants = new();
		foreach (string childName in children)
		{
			CharInfo info = FindCharInfo(childName);
			Couple couple = new();
			switch (info.Gender)
			{
				case Gender.male:
					couple.IsHusbandDescendant = true;
					couple.Husband = info;
					break;
				case Gender.female:
					couple.IsHusbandDescendant = false;
					couple.Wife = info;
					break;
			}
			descendants.Add(couple);
		}
		return descendants;
	}
	
	public struct CharInfo
	{
		public string Name; 
		public int Age;
		public Gender Gender;
		public bool IsAlive;
		public Noble.Rank Rank;
	}

	public class Couple
	{
		public CharInfo Husband;
		public CharInfo Wife;
		public List<Couple> Descendants = new();
		public bool IsHusbandDescendant = true;
		public Couple OriginCouple = null;

		public void UpdateDescendant (Couple couple)
		{
			for (var i = 0; i < Descendants.Count; i++)
			{
				Couple descendant = Descendants[i];
				if (descendant.Husband.Name == couple.Husband.Name || descendant.Wife.Name == couple.Wife.Name)
				{
					Descendants[i] = couple;
					couple.OriginCouple = this;
					couple.IsHusbandDescendant = descendant.Husband.Name == couple.Husband.Name;
					return;
				}
			}
		}
	}
}

}