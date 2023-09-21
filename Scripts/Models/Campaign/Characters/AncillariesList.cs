using System.Collections.Generic;

namespace Model
{

public class AncillariesList
{
	List<string> _ancillaries = new();

	public AncillariesList (List<string> ancillaries)
	{
		SetAncillaries(ancillaries);
	}

	public List<string> GetAncillaries ()
	{
		return new List<string>(_ancillaries);
	}

	void SetAncillaries (List<string> ancillaries)
	{
		_ancillaries = ancillaries;
	}
}

}