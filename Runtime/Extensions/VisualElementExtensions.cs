using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dropecho {
  public static class VisualElementExtensions {
    public static VisualElement AddStyleSheet(this VisualElement el, string path) {
      var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
      if (styleSheet == null) {
        Debug.LogWarning("No style sheet found at " + path);
      } else {
        el.styleSheets.Add(styleSheet);
      }
      return el;
    }

    public static VisualElement BindAndReturn(this VisualElement el, SerializedObject so) {
      el.Bind(so);
      return el;
    }

    public static VisualElement AddToClassList(this VisualElement el, params string[] classes) {
      foreach (var cls in classes) {
        el.AddToClassList(cls);
      }
      return el;
    }

    public static VisualElement Add(this VisualElement element, params VisualElement[] children) {
      return element.AddChildren(children);
    }

    public static VisualElement AddChildren(this VisualElement element, params VisualElement[] children) {
      foreach (var child in children) {
        element.Add(child);
      }
      return element;
    }
  }
}