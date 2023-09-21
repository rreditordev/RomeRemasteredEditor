using System.Collections.Generic;
using System.IO;

namespace RtwFileIO
{

public class ExportDescrUnitReader
{
	ExportDescrUnit _data;
	readonly string _filepath;
	string[] _lines;
	int _curLine = 0;
	
	public ExportDescrUnitReader (string filepath)
	{
		_filepath = filepath;
	}
	
	public ExportDescrUnit Read ()
	{
		_lines = File.ReadAllLines(_filepath);
		
		_data = new ExportDescrUnit
		{
			UnitDefinitions = new List<UnitDefinitionDto>()
		};
		
		for (;_curLine < _lines.Length; _curLine++)
		{
			if (_lines[_curLine].StartsWith(Keywords.Type))
			{
				_data.UnitDefinitions.Add(ReadUnit());
			}
		}
		
		return _data;
	}

	UnitDefinitionDto ReadUnit ()
	{
		UnitDefinitionDto unitDto = new();
		
		// Read ID
		string unitID = _lines[_curLine].Trim().Remove(0, Keywords.Type.Length).Trim();
		unitDto.UnitID = unitID;
		_curLine++;
		
		for (; _curLine < _lines.Length; _curLine++)
		{
			string line = _lines[_curLine].Trim();
			if (line.StartsWith(Keywords.Ownership)) // Last attribute of unit, read it and return
			{
				// Read ownership then return
				return unitDto;
			}
		}
		
		return unitDto;
	}
}

}