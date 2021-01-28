
// using UnityEditor;
// using UnityEngine;

// namespace Dropecho {
//   public class AssetHandler<TSOType, TWindow> : AssetPostprocessor
//   where TSOType : ScriptableObject
//   where TWindow : EditorWindow {
//     /// <summary>
//     /// When a user double clicks a storygen config asset file, this is the handler for it.
//     /// Open the storytype editor window and select the double clicked asset to show it's details.
//     /// </summary>
//     /// <param name="instanceId">The serialized instance id.</param>
//     /// <param name="line">Unused</param>
//     /// <returns></returns>
//     [UnityEditor.Callbacks.OnOpenAsset]
//     public static bool OpenEditor(int instanceId, int line) {
//       if (EditorUtility.InstanceIDToObject(instanceId) is TSOType obj) {
//         // TWindow.Open(obj);
//         EditorWindow.GetWindow<TWindow>().Open();
//         return true;
//       }
//       return false;
//     }

//     /// <summary>
//     /// If you have the storytype editor window open, redraw the gui to load any new assets.
//     /// </summary>
//     /// <param name="importedAssets"></param>
//     /// <param name="deletedAssets"></param>
//     /// <param name="movedAssets"></param>
//     /// <param name="movedFromAssetPaths"></param>
//     static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
//       if (EditorWindow.HasOpenInstances<StoryTypeEditorWindow>()) {
//         EditorWindow.GetWindow<StoryTypeEditorWindow>().CreateGUI();
//       }
//     }
//   }
// }