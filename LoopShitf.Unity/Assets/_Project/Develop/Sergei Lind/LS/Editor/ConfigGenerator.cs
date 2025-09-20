using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using System.IO;

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
                    Player = new PlayerConfig
                    {
                        Ring = new RingConfig()
                    },
                    Enemy = new EnemyConfig()
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