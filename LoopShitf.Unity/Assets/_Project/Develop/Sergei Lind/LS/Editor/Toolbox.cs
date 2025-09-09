using UnityEditor;
using UnityEditor.SceneManagement;

namespace Sergei_Lind.LS.Editor
{
    public sealed class ToolBox
    {
        [MenuItem("Sergei Lind/Scenes/Bootstrap &1", priority = 202)]
        public static void OpenBootstrapScene()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/_Project/Scenes/0.Bootstrap.unity");
        }

        [MenuItem("Sergei Lind/Scenes/Core &2", priority = 202)]
        public static void OpenCoreScene()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/_Project/Scenes/1.Core.unity");
        }
    }
}