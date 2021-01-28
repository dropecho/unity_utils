using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Dropecho {
  public static partial class Utils {
    public static T CreateAsset<T>(string path) where T : ScriptableObject {
      if (!Directory.Exists(path)) {
        Directory.CreateDirectory(path);
      }
      var instance = ScriptableObject.CreateInstance<T>();
      AssetDatabase.CreateAsset(instance, path);
      return instance;
    }

    public static T[] GetAssetsOfType<T>() where T : UnityEngine.Object {
      var typeName = typeof(T).Name;

      return AssetDatabase.FindAssets("t:" + typeName)
        .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
        .Select(path => AssetDatabase.LoadAssetAtPath<T>(path))
        .ToArray();
    }
  }
}