using UnityEditor;
using UnityEngine;

namespace NMJ.HotkeyScenes
{
    public class SceneHotkeyData : ScriptableObject
    {
        [SerializeField] private SceneAsset[] hotkeySceneAssets = new SceneAsset[10];
        public SceneAsset[] HotkeySceneAssets => hotkeySceneAssets;
    }
}