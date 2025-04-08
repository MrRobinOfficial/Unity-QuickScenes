using System.Collections.Generic;

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;

using UnityEngine;

namespace MrRobinOfficial.QuickScenes.Editor
{
    /// <summary>
    /// A search provider for scenes.
    /// </summary>
    public class SceneSearchProvider
        : ScriptableObject
        , ISearchWindowProvider
    {
        /// <summary>
        /// List of search tree entries
        /// </summary>
        private List<SearchTreeEntry> _searchTree;

        /// <summary>
        /// A collection of scenes. Where the key is the group name and the value is list of scenes
        /// </summary>
        private IReadOnlyDictionary<string, SceneSelectorSettings.ISceneList> _sceneDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneSearchProvider"/> class.
        /// </summary>
        /// <param name="scenceDictionary">A collection of scenes. Where the key is the group name and the value is list of scenes</param>
        public void Initialize(
            IReadOnlyDictionary<string, SceneSelectorSettings.ISceneList> scenceDictionary)
        {
            _sceneDictionary = scenceDictionary;
        }

        public List<SearchTreeEntry> CreateSearchTree(
            SearchWindowContext context)
        {
            _searchTree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Select Scene"), 0)
            };

            foreach (KeyValuePair<string, SceneSelectorSettings.ISceneList> group in _sceneDictionary)
            {
                _searchTree.Add(new SearchTreeGroupEntry(new GUIContent(group.Key), 1));

                foreach (SceneAsset sceneAsset in group.Value.Scenes)
                {
                    string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                    string sceneName = sceneAsset.name;

                    _searchTree.Add(new SearchTreeEntry(
                        new GUIContent(sceneName, EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D))
                    {
                        level = 2,
                        userData = scenePath
                    });
                }
            }

            return _searchTree;
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            if (entry.userData is not string scenePath)
                return false;

            EditorSceneManager.OpenScene(scenePath);
            return true;
        }
    }
}
