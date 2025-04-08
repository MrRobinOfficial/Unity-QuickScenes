using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEditor.Experimental.GraphView;

using UnityEngine;

namespace MrRobinOfficial.QuickScenes.Editor
{
    /// <summary>
    /// The overlay for the scene selector
    /// </summary>
    [Overlay(typeof(SceneView), "Scene Selector", true)]
    public class SceneSelectorOverlay : ToolbarOverlay
    {
        public SceneSelectorOverlay() : base(SceneSelectorButton.ID) { }
    }

    /// <summary>
    /// The button for the scene selector
    /// </summary>
    [EditorToolbarElement(ID, typeof(SceneView))]
    public class SceneSelectorButton : EditorToolbarButton
    {
        /// <summary>
        /// The id for the scene selector
        /// </summary>
        public const string ID = "SceneSelectorButton";

        public SceneSelectorButton()
        {
            text = "Scenes";
            icon = EditorGUIUtility.IconContent("SceneAsset Icon").image as Texture2D;
            tooltip = "Open Scene Selector";
            clicked += Button_OnClicked;
        }

        private void Button_OnClicked()
        {
            // Get the settings
            var settings = SceneSelectorSettings.GetOrCreate();

            // Create the search window and initialize it
            var searchWindow = ScriptableObject.CreateInstance<SceneSearchProvider>();
            searchWindow.Initialize(settings.GetSceneDictionary());

            // Open the search window
            SearchWindow.Open(
                new SearchWindowContext(
                    screenMousePosition: GUIUtility.GUIToScreenPoint(Event.current.mousePosition),
                    requestedWidth: 256f,
                    requestedHeight: 0
                ), searchWindow
            );
        }
    }
}
