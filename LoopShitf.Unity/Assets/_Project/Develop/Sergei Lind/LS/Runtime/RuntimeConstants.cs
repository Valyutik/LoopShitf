using UnityEngine.SceneManagement;

namespace Sergei_Lind.LS.Runtime
{
    public static class RuntimeConstants
    {
        public static class Player
        {
            public const string Prefab = "Player";
        }
        
        public static class Configs
        {
            public const string ConfigFileName = "Config";
        }

        public static class Scenes
        {
            public static readonly int Bootstrap = SceneUtility.GetBuildIndexByScenePath("0.Bootstrap");
            public static readonly int Core = SceneUtility.GetBuildIndexByScenePath("1.Core");
        }
    }
}