using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using Gilzoide.EasyProjectSettings;
using MrRobinOfficial.SerializedDictionary;

namespace MrRobinOfficial.QuickScenes.Editor
{
    /// <summary>
    /// The settings for the scene selector.
    /// </summary>
    [ProjectSettings(
        assetPath: "ProjectSettings/SceneSelector/QuickScenesSettings",
        SettingsPath = "Project/Tools/Quick Scenes")]
    public class SceneSelectorSettings : ScriptableObject
    {
        public interface ISceneList
        {
            /// <summary>
            /// List of scenes
            /// </summary>
            public IReadOnlyList<SceneAsset> Scenes { get; }
        }

        /// <summary>
        /// A struct which contains a list of scenes
        /// </summary>
        [Serializable]
        public struct SceneList : ISceneList
        {
            [SerializeField]
            private List<SceneAsset> _scenes;

            public IReadOnlyList<SceneAsset> Scenes => _scenes;
        }

        /// <summary>
        /// A collection of scenes. Where the key is the group name and the value is list of scenes
        /// </summary>
        [SerializeField]
        private SerializedDictionary<string, SceneList> _sceneDictionary = new();

        /// <summary>
        /// Get the scene dictionary
        /// </summary>
        /// <returns>A collection of scenes. Where the key is the group name and the value is list of scenes</returns>
        public IReadOnlyDictionary<string, ISceneList> GetSceneDictionary()
        {
            var dict = new Dictionary<string, ISceneList>(_sceneDictionary.Count);

            foreach (KeyValuePair<string, SceneList> scene in _sceneDictionary)
                dict.Add(scene.Key, scene.Value);

            return dict;
        }

        /// <summary>
        /// Get or create the <see cref="SceneSelectorSettings"/>
        /// </summary>
        /// <returns>A instance of the <see cref="SceneSelectorSettings"/></returns>
        public static SceneSelectorSettings GetOrCreate() => ProjectSettings.LoadOrCreate<SceneSelectorSettings>();
    }
}
