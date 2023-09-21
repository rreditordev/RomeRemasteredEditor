using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class Army
{
	List<Unit> _units = new();
	
	public Army (List<UnitInfo> unitInfoList)
	{
		SetUnits(unitInfoList);
	}

	public List<Unit> GetUnits ()
	{
		return new List<Unit>(_units);
	}
	
	public int GetNumberOfUnits ()
	{
		return _units.Count;
	}
	
	void SetUnits (List<UnitInfo> unitInfoList)
	{
		foreach (UnitInfo unitInfo in unitInfoList)
		{
			_units.Add(new Unit(unitInfo));
		}
	}
}

public class Unit
{
	public string UnitID => _unitID;
	public int Experience => _experience;
	public int ArmourLevel => _armourLevel;
	public int WeaponLevel => _weaponLevel;
	
	string _unitID;
	int _experience;
	int _armourLevel;
	int _weaponLevel;

	public Unit (UnitInfo unitInfo)
	{
		SetUnitID(unitInfo.UnitName);
		SetExperience(unitInfo.Experience);
		SetArmourLevel(unitInfo.ArmourLevel);
		SetWeaponLevel(unitInfo.WeaponLevel);
	}

	void SetUnitID (string unitID)
	{
		_unitID = unitID;
	}

	void SetExperience (int experience)
	{
		_experience = experience;
	}

	void SetArmourLevel (int armourLevel)
	{
		_armourLevel = armourLevel;
	}

	void SetWeaponLevel (int weaponLevel)
	{
		_weaponLevel = weaponLevel;
	}
}

}