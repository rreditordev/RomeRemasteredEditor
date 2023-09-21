using Newtonsoft.Json;
using System.IO;

public class GlobalUIVariables
{

    public static string prefsLocation = @".\prefs.json";
    public static bool dataLoaded = false;

    //A save that outputs to a output folder rather than direct script saving!
    public static bool safeSave = true;

    public static string CurrentDataPath() {
        if (File.Exists(prefsLocation)) {
            string json = File.ReadAllText(prefsLocation);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            return jsonObj["DataPath"];
        }else { 
            return null; 
        }
    }


}
