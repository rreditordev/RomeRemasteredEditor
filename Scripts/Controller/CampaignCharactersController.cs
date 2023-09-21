using Model;
using System.Collections.Generic;
using Godot;

namespace Controller
{

	public static class CampaignCharactersController
	{

		public static List<CharacterDto> GetCharacters ()
		{
			List<CharacterDto> dto = new();
			List<Faction> factions = RtwDataContext.Campaign.GetFactions();
			for (var i = 0; i < factions.Count; i++)
			{
				Faction faction = factions[i];
				List<Character> characters = faction.GetCharacters();
				for (var j = 0; j < characters.Count; j++)
				{
					Character character = characters[j];
					CharacterDto charDto = new CharacterDto {
						FactionID = faction.FactionID,
						Name = character.Name,
						Age = character.Age,
						MapPosition = character.MapPosition,
						Type = character.Type,
						FactionLeadership = character is Noble noble ? noble.NobleRank : Noble.Rank.None,
						Command = character.Command,
						Influence = character.Influence,
						Management = character.Management,
						Subterfuge = character.Subterfuge,
						Traits = character is Notable notable ? notable.TraitList : null,
						army = null,
                        Ancillaries = character is Notable note ? note.AncillariesList : null,
                    };

					if (character is Captain captian)
						charDto.army = captian.LeadingArmy;
					else if (character is Noble nob)
						charDto.army = nob.LeadingArmy;

					dto.Add(charDto);
				}
			}
			return dto;
		}
	}

	public class CharacterDto
	{
		public string FactionID;
		public string Name;
		public int Age;
		public Vector2I MapPosition;
		public CharacterType Type;
		public Noble.Rank FactionLeadership;
		public int Command;
		public int Influence;
		public int Management;
		public int Subterfuge;
		public TraitList Traits;
		public Army army;
		public AncillariesList Ancillaries;
    }

}