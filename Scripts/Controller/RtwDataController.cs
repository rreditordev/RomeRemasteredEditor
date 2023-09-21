using Model;

namespace Controller
{

public static class RtwDataController
{
	public static bool Load (string path)
	{
		return RtwDataContext.Load(path);
	}

	public static void Save ()
	{
		RtwDataContext.Save();
	}
}

}