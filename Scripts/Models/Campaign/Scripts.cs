using RtwFileIO;
using System.Collections.Generic;

namespace Model
{

public class Scripts
{
	List<ScriptInfo> _scripts;
	List<SpawnScriptInfo> _spawnScripts;

	public Scripts (List<ScriptInfo> scripts, List<SpawnScriptInfo> spawnScripts)
	{
		_scripts = scripts;
		_spawnScripts = spawnScripts;
	}

	public List<ScriptInfo> GetScripts ()
	{
		return new List<ScriptInfo>(_scripts);
	}
	
	public List<SpawnScriptInfo> GetSpawnScripts ()
	{
		return new List<SpawnScriptInfo>(_spawnScripts);
	}
}

}