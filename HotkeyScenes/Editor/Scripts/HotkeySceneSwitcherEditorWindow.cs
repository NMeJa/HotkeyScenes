using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NMJ.HotkeyScenes
{
    public class HotkeySceneSwitcherEditorWindow : EditorWindow
    {
        private const string cMenuPath = "Tools/NMJ/SceneManager/Hotkeys/";

        #region Data Object

        private static SceneHotkeyData data;

        private static SceneHotkeyData Data => data ? data : data = CreateSceneDataContainer();

        private static SceneHotkeyData CreateSceneDataContainer()
        {
            const string dataContainerName = "NMJHotkeySceneDataContainer";
            const string containerPath = "Assets/Editor Default Resources/NMJ/SceneSwitcher";
            string[] assetsPaths = AssetDatabase.FindAssets(dataContainerName);
            if (assetsPaths.Length is 0)
            {
                Debug.LogWarning($"Data container wasn't found!\n Creating New One in directory{containerPath}!");
                System.IO.Directory.CreateDirectory(containerPath);
                AssetDatabase.Refresh();

                var dataContainer = CreateInstance<SceneHotkeyData>();
                dataContainer.name = dataContainerName;
                AssetDatabase.CreateAsset(dataContainer, containerPath + "/" + dataContainerName + ".asset");
                Debug.Log($"Created: {dataContainerName} at Directory Path: {containerPath}");
                return dataContainer;
            }
            else
            {
                string giud = assetsPaths[0];
                string assetPath = AssetDatabase.GUIDToAssetPath(giud);
                var dataContainer = AssetDatabase.LoadAssetAtPath<SceneHotkeyData>(assetPath);
                Debug.Log($"Loaded Asset: {dataContainer.name} from Path : {assetPath}");
                return dataContainer;
            }
        }

        #endregion

        #region Open Menu

        [MenuItem(cMenuPath + "Edit #e")]
        private static void OpenWindow()
        {
            var wnd = GetWindow<HotkeySceneSwitcherEditorWindow>();
            wnd.titleContent = new GUIContent("Hotkey Scenes");
            wnd.Show();
        }

        [MenuItem(cMenuPath + "Scene 0 #0")]
        private static void OpenScene0() => LoadScene(Data.HotkeySceneAssets[0]);

        [MenuItem(cMenuPath + "Scene 1 #1")]
        private static void OpenScene1() => LoadScene(Data.HotkeySceneAssets[1]);

        [MenuItem(cMenuPath + "Scene 2 #2")]
        private static void OpenScene2() => LoadScene(Data.HotkeySceneAssets[2]);

        [MenuItem(cMenuPath + "Scene 3 #3")]
        private static void OpenScene3() => LoadScene(Data.HotkeySceneAssets[3]);

        [MenuItem(cMenuPath + "Scene 4 #4")]
        private static void OpenScene4() => LoadScene(Data.HotkeySceneAssets[4]);

        [MenuItem(cMenuPath + "Scene 5 #5")]
        private static void OpenScene5() => LoadScene(Data.HotkeySceneAssets[5]);

        [MenuItem(cMenuPath + "Scene 6 #6")]
        private static void OpenScene6() => LoadScene(Data.HotkeySceneAssets[6]);

        [MenuItem(cMenuPath + "Scene 7 #7")]
        private static void OpenScene7() => LoadScene(Data.HotkeySceneAssets[7]);

        [MenuItem(cMenuPath + "Scene 8 #8")]
        private static void OpenScene8() => LoadScene(Data.HotkeySceneAssets[8]);

        [MenuItem(cMenuPath + "Scene 9 #9")]
        private static void OpenScene9() => LoadScene(Data.HotkeySceneAssets[9]);

        private static void LoadScene(SceneAsset scene)
        {
            if (Data.HotkeySceneAssets.Length < 1) return;
            if (scene is null) return;
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            string scenePath = AssetDatabase.GetAssetOrScenePath(scene);
            if (string.IsNullOrEmpty(scenePath)) return;
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }

        #endregion

        private void CreateGUI()
        {
            var root = rootVisualElement;
            var serializedObject = new SerializedObject(Data);
            var hotkeyMainContainerVE = new VisualElement
            {
                pickingMode = PickingMode.Ignore,
                style =
                {
                    flexShrink = 1,
                    flexGrow = 1,
                    borderBottomColor = new StyleColor(Color.black),
                    borderLeftColor = new StyleColor(Color.black),
                    borderRightColor = new StyleColor(Color.black),
                    borderTopColor = new StyleColor(Color.black),
                    borderBottomWidth = 1.5f,
                    borderLeftWidth = 1.5f,
                    borderRightWidth = 1.5f,
                    borderTopWidth = 1.5f
                }
            };
            root.Add(hotkeyMainContainerVE);

            var headerL = new Label
            {
                text = "Hotkey Scenes",
                tooltip = "Hotkey Scenes for fast scene changing using Shift+'number':",
                style =
                {
                    fontSize = 20f,
                    unityTextAlign = TextAnchor.MiddleCenter,
                    backgroundColor = new StyleColor(Color.black)
                }
            };
            hotkeyMainContainerVE.Add(headerL);
            var hotkeyContainer = new VisualElement
            {
                pickingMode = PickingMode.Ignore,
                style =
                {
                    flexShrink = 1,
                    flexGrow = 1
                }
            };
            hotkeyMainContainerVE.Add(hotkeyContainer);
            var sceneListProperty = serializedObject.FindProperty("hotkeySceneAssets");

            for (int i = 1; i < 10; i++)
            {
                var sceneProperty = sceneListProperty.GetArrayElementAtIndex(i);
                string tooltipName = sceneProperty.objectReferenceValue
                    ? sceneProperty.objectReferenceValue.name
                    : "It is empty";
                var propertyField = new PropertyField(sceneProperty)
                {
                    label = $"Scene {i}",
                    tooltip = tooltipName
                };
                propertyField.Bind(serializedObject);
                hotkeyContainer.Add(propertyField);
            }

            {
                var sceneProperty = sceneListProperty.GetArrayElementAtIndex(0);
                string tooltipName = sceneProperty.objectReferenceValue
                    ? sceneProperty.objectReferenceValue.name
                    : "It is empty";
                var propertyField = new PropertyField(sceneProperty)
                {
                    label = $"Scene 0",
                    tooltip = tooltipName
                };
                propertyField.Bind(serializedObject);
                hotkeyContainer.Add(propertyField);
            }
        }
    }
}