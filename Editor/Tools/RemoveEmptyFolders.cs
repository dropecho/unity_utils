using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

class RemoveEmptyFoldersPostProcessor : AssetPostprocessor {
  static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
    RemoveEmptyFoldersEditorWindow.RefreshFolderList();
  }
}

public class RemoveEmptyFoldersEditorWindow : EditorWindow {
  [MenuItem("Tools/Dropecho/Empty Folder Remover")]
  public static void Open() => GetWindow<RemoveEmptyFoldersEditorWindow>("Empty Folder Remover");

  static string[] folders;
  static Vector2 scrollPos;
  static int startStringIndex;

  void OnEnable() {
    RefreshFolderList();
    startStringIndex = Application.dataPath.IndexOf("/Assets");
  }

  public void OnGUI() {
    if (folders != null && folders.Count() > 0) {
      GUI.backgroundColor = Color.red;
      GUILayout.Box($"Empty Folders: {folders.Count()}",
        new GUILayoutOption[] { GUILayout.MinHeight(20), GUILayout.ExpandWidth(true) });
      EditorGUILayout.Separator();
      scrollPos = GUILayout.BeginScrollView(scrollPos);
      foreach (var folder in folders) {
        GUILayout.Label(folder.Substring(startStringIndex + 1));
      }
      GUILayout.EndScrollView();

      GUI.backgroundColor = Color.red;
      if (GUILayout.Button("DELETE EMPTY FOLDERS", new GUILayoutOption[] { GUILayout.MinHeight(40) })) {
        RemoveEmptyFolders();
      }
    } else {
      GUI.backgroundColor = Color.green;
      GUILayout.Box("\n\nGreat job, there are no empty folders in your project.",
        new GUILayoutOption[] { GUILayout.MinHeight(80), GUILayout.ExpandWidth(true) });
    }
  }

  // Get every empty folder sub folder in the project.
  public static void RefreshFolderList(string[] _ = null) {
    folders = Directory
      .GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories)
      .Select(x => x.Replace('\\', '/'))
      .Where(IsEmptyRecursive)
      .ToArray();
  }

  private static void RemoveEmptyFolders() {
    foreach (var folder in folders) {
      var assetPath = folder.Substring(startStringIndex + 1);
      // Sync AssetDatabase with the delete operation.
      if (!AssetDatabase.DeleteAsset(assetPath)) {
        Debug.LogWarning($"Failed to delete asset at: {assetPath}");
      }
    }

    // Refresh the asset database once we're done.
    AssetDatabase.Refresh();
  }

  /// <summary>
  /// A helper method for determining if a folder is empty or not.
  /// </summary>
  private static bool IsEmptyRecursive(string path) {
    // A folder is empty if it (and all its subdirs) have no files (ignore .meta files)
    return Directory.GetFiles(path).Select(file => !file.EndsWith(".meta")).Count() == 0
      && Directory.GetDirectories(path, string.Empty, SearchOption.AllDirectories).All(IsEmptyRecursive);
  }
}