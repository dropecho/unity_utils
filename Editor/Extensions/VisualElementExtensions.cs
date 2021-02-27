using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dropecho {
  public static partial class VisualElementExtensions {
    public static VisualElement BindAndReturn(this VisualElement el, SerializedObject so) {
      el.Bind(so);
      return el;
    }

    public static VisualElement AddStyleSheet(this VisualElement el, string path) {
      var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
      if (styleSheet == null) {
        Debug.LogWarning("No style sheet found at " + path);
      } else {
        el.styleSheets.Add(styleSheet);
      }
      return el;
    }
  }
}