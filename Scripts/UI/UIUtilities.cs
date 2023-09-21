using Controller;
using Godot;
using Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UIUtilities {
    public partial class Color32 {
        public byte r;
        public byte g;
        public byte b;
        public byte a;
        public Color32(byte r, byte g, byte b, byte a) {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

    }

    public class UnitConversions {

        public static Vector2 WorldToGameCoordinates(Vector2 mousePos, int gameHeight, int gameWidth, Vector2 mapScale) {
            int x = 0;
            int y = 0;

            y = Mathf.RoundToInt(gameHeight / 2 - 0.5f - ((mousePos.Y * gameHeight) / mapScale.Y));
            x = (gameWidth-1) - Mathf.RoundToInt(gameWidth / 2 - 0.5f - ((gameWidth * mousePos.X)/(mapScale.X)));

            if(x > gameWidth -1)
                x = gameWidth - 1;
            else if(x < 0)
                x = 0;

            if(y > gameHeight -1)
                y = gameHeight - 1;
            else if(y < 0)
                y = 0;
            return new Vector2(x,y);

        }

        public static Vector2 GameToWorldCoordinates(int gameHeight, int gameWidth, int x, int y, Vector2 mapScale) {
            float posY = 0;
            float posX = 0;


            //Y Ratio calculations for indicator
            float posYRatio = (float)y / (float)gameHeight;
            float halfYAdjustment = (1.0f / (float)gameHeight) / 2.0f;
            posY = -(posYRatio * mapScale.Y) + (mapScale.Y/2) - (halfYAdjustment * mapScale.Y);

            //X Ratio calculations for indicator
            float posXRatio = (float)((gameWidth - 1) - x) / (float)gameWidth;
            float halfXAdjustment = (1.0f / (float)gameWidth) / 2.0f;

            // x needs an added ratio because mesh is streched so texture isn't warped
            posX = -(posXRatio * mapScale.X) + ((mapScale.X/2)) - (halfXAdjustment * mapScale.X); 

            return new Vector2(posX, posY);
        }
    }

    public static class FilterClasses {
        public static Model.Resource FilterResource(List<Model.Resource> resources, int x, int y) {
            var foundRes = resources.SingleOrDefault(res => res.MapPosition.X == x && res.MapPosition.Y == y);
            return foundRes;
        }

        public static Landmark FilterLandmarks(List<Landmark> landmarks, int x, int y) {
            var foundLandmark = landmarks.SingleOrDefault(landmark => landmark.MapPosition.X == x && landmark.MapPosition.Y == y);
            return foundLandmark;
        }

        public static List<CharacterDto> FilterCharacterInfo(List<CharacterDto> characters, int x, int y) {
            List<CharacterDto> locationChars = new List<CharacterDto>();
            foreach (var character in characters) {
                if (character.MapPosition.X == x && character.MapPosition.Y == y) {
                    locationChars.Add(character);
                }
            }

            return locationChars;
        }
    }

    public static class SaveClasses {
        public static void SaveResourceInArray(Model.Resource res) {
            bool noChange = true;
            for(int i = 0; i < RtwDataContext.Campaign._resources.Count; i++) {
                if (RtwDataContext.Campaign._resources[i].MapPosition.X == res.MapPosition.X &&
                    RtwDataContext.Campaign._resources[i].MapPosition.Y == res.MapPosition.Y) {
                    RtwDataContext.Campaign._resources[i] = res;
                    noChange = false;
                }
            }
            if(noChange) {
                throw new Exception("SaveResourceInArray function could not find the resource that was changed!");
            }              
        }

        public static void AddResourceToArray(Model.Resource res) {
            RtwDataContext.Campaign._resources.Add(res);
        }

    }

    public static class DeleteClasses {
        public static void RemoveResourceFromArray(Model.Resource res) {
            if (!RtwDataContext.Campaign._resources.Remove(res)) {
                throw new Exception("RemoveResourceToArray could not remove the resource in question!");
            }
        }
    }

        public class Preferences {

        public static string CurrentDataPath() {
            if (File.Exists(@".\prefs.json")) {
                string json = File.ReadAllText(@".\prefs.json");
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                return jsonObj["DataPath"];
            } else {
                return null;
            }
        }

        public static PathResponse SetPath(string path) {
            bool result;
            try {
                result = RtwDataController.Load(path);
            } catch {
                result = false;
            }
            if (result) {
                SavePathToPrefs(path);
                return new PathResponse {
                    validationText = "",
                    result = true
                };
            } else {
                return new PathResponse {
                    validationText = "The path you have entered is not a valid data path for a Rome Total War game. Please enter a valid path!",
                    result = false
                };
            }
        }

        static void SavePathToPrefs(string path) {
            if (!File.Exists(GlobalUIVariables.prefsLocation)) {
                JObject pathData = new JObject(
                    new JProperty("DataPath", path)
                );

                using (StreamWriter file = File.CreateText(GlobalUIVariables.prefsLocation))
                using (JsonTextWriter writer = new JsonTextWriter(file)) {
                    pathData.WriteTo(writer);
                }
            } else {
                string json = File.ReadAllText(GlobalUIVariables.prefsLocation);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                GD.Print(jsonObj["DataPath"]);
                jsonObj["DataPath"] = path;
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(GlobalUIVariables.prefsLocation, output);
            }
        }
    }

    public class PathResponse {
        public string validationText;
        public bool result;
    }

}
