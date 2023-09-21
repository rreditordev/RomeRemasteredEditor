using RtwFileIO;

namespace Model
{

public class UnitDefinition
{
	string _unitID;

	public UnitDefinition (UnitDefinitionDto dto)
	{
		_unitID = dto.UnitID;
	}
}

}