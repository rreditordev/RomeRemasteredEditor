using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class UnitDefinitions
{
	List<UnitDefinition> _unitDefinitions = new();
	
	public UnitDefinitions (ExportDescrUnit edu)
	{
		for (var i = 0; i < edu.UnitDefinitions.Count; i++)
		{
			_unitDefinitions.Add(new UnitDefinition(edu.UnitDefinitions[i]));
		}
	}
}

}