using UnityEngine;
using UnityEngine.UIElements;

namespace Dropecho {
  public static partial class VisualElementExtensions {
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