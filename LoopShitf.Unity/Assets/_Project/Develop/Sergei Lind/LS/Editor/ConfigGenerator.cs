using Unity.Plastic.Newtonsoft.Json;
using Sergei_Lind.LS.Runtime;
using UnityEditor;
using UnityEngine;
using System.IO;
using Sergei_Lind.LS.Runtime.Utilities;

namespace Sergei_Lind.LS.Editor
{
    public sealed class ConfigGenerator
    {
        [MenuItem("Sergei Lind/Generate Config")]
        public static void Generate()
        {
            var configContainer = new ConfigContainer
            {
                Core = new CoreConfig
                {
                    Player = new PlayerConfig()
                }
            };

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };
            settings.Converters.Add(new Vector2Converter());
            
            var json = JsonConvert.SerializeObject(configContainer, settings);
            var path = Path.Combine(Application.dataPath, "_Project", "Resources",
                RuntimeConstants.Configs.ConfigFileName + ".json");
            File.WriteAllText(path, json);
        }
    }
}