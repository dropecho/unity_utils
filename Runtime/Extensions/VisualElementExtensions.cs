using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dropecho {
  public static partial class VisualElementExtensions {
    public static T Replace<T>(this T el, params VisualElement[] children) where T : VisualElement {
      el.Clear();
      return el.AddChildren(children);
    }

    public static T AddClasses<T>(this T el, params string[] classes) where T : VisualElement {
      foreach (var cls in classes) {
        el.AddToClassList(cls);
      }
      return el;
    }

    public static T AddToClassList<T>(this T el, params string[] classes) where T : VisualElement {
      return el.AddClasses(classes);
    }

    public static T Add<T>(this T element, params VisualElement[] children) where T : VisualElement {
      return element.AddChildren(children);
    }

    public static T AddChildren<T>(this T element, params VisualElement[] children) where T : VisualElement {
      foreach (var child in children) {
        element.Add(child);
      }
      return element;
    }

    public static T AddChildren<T>(this T element, IEnumerable<VisualElement> children) where T : VisualElement {
      return element.AddChildren(children.ToArray());
    }
  }
}