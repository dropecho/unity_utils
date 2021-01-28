using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dropecho {
  public static class UIElementUtils {
    public static VisualElement RenderDefaultEditorFor<T>(T obj) where T : Object {
      var objEl = new VisualElement();
      objEl.AddToClassList("default-editor");
      var serialized = new SerializedObject(obj);
      DrawProperties(objEl, serialized);
      return objEl.BindAndReturn(serialized);
    }

    private static void DrawProperties(VisualElement element, SerializedObject serialized) {
      var propIterator = serialized.GetIterator();
      // Must call this once, why?
      propIterator.Next(true);

      while (propIterator.NextVisible(false)) {
        if (propIterator.name == "m_Script") { continue; }
        var propField = new PropertyField(propIterator).BindAndReturn(serialized);

        propField.RegisterCallback<ChangeEvent<object>>(ev => {
          Debug.Log("something changed");
        });
        element.Add(propField);
      }
    }
  }
}