using System;
using System.Collections.Generic;
using System.IO;

namespace RtwFileIO
{

public class DescrSmFactionsReader
{
	DescrSmFactions _data;
	readonly string _filepath;
	string[] _lines;
	int _curLine = 0;
	
	public DescrSmFactionsReader (string filePath)
	{
		_filepath = filePath;
	}

	public DescrSmFactions Read ()
	{
		_data = new DescrSmFactions
		{
			Factions = new List<FactionDescription>()
		};
		_lines = File.ReadAllLines(_filepath);

		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (string.IsNullOrEmpty(line) || line.StartsWith(Keywords.Comment)) continue;

			if (line.StartsWith("\"factions\""))
			{
				ReadFactions();
				return _data;
			}
		}

		return _data;
	}

	void ReadFactions ()
	{
		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine];
			if (line.StartsWith("\t\""))
			{
				FactionDescription faction = new();
				faction.FactionID = line.Trim().Replace(":", "").Trim().Split('"', '"')[1];

				for (; _curLine < _lines.Length; _curLine++)
				{
					string factionLine = _lines[_curLine];
					if (factionLine.StartsWith("\t\t\"culture\""))
					{
						faction.Culture = factionLine.Trim().Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Trim().Split('"', '"')[1].Trim();
						break;
					}
				}

				_data.Factions.Add(faction);
			}
		}
	}
}

public struct DescrSmFactions
{
	public List<FactionDescription> Factions;
}

public struct FactionDescription
{
	public string FactionID;
	public string Culture;
}

}