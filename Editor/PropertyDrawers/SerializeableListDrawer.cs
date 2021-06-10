using System.Collections.Generic;
using Dropecho;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SerializableList<>))]
public class SerializeableListDrawer : PropertyDrawer {
  public override VisualElement CreatePropertyGUI(SerializedProperty property) {
    return Rerender(new VisualElement().AddToClassList("container", "rows", "fill"), property);
  }

  private VisualElement Rerender(VisualElement root, SerializedProperty property) {
    property.serializedObject.ApplyModifiedProperties();

    return root.Replace(
      RenderFoldout(root, property)
        .AddChildren(RenderRows(root, property)),
      RenderAddButton(root, property)
    );
  }

  private VisualElement RenderFoldout(VisualElement root, SerializedProperty property) {
    var valuesProp = property.FindPropertyRelative("values");

    var container = new Foldout() {
      text = valuesProp.arraySize + " value" + (valuesProp.arraySize != 1 ? "s" : ""),
      value = valuesProp.isExpanded
    };
    container
      .AddClasses("container", "fill")
      .RegisterValueChangedCallback(ev => valuesProp.isExpanded = ev.newValue);

    return container;
  }

  private List<VisualElement> RenderRows(VisualElement root, SerializedProperty property) {
    var valuesProp = property.FindPropertyRelative("values");

    var rows = new List<VisualElement>();
    for (var i = 0; i < valuesProp.arraySize; i++) {
      var row = new VisualElement().AddClasses("container", "rows");
      var currentIndex = i;
      var deleteButton = new Button(() => {
        valuesProp.DeleteArrayElementAtIndex(currentIndex);
        Rerender(root, property);
      }) {
        text = "-"
      };

      rows.Add(
        row.Add(
          new PropertyField(valuesProp.GetArrayElementAtIndex(i))
            .AddClasses("dictionary-field")
            .BindAndReturn(property.serializedObject),
          deleteButton
        )
      );
    }

    return rows;
  }

  private VisualElement RenderAddButton(VisualElement root, SerializedProperty property) {
    var valuesProp = property.FindPropertyRelative("values");
    return new Button(() => {
      valuesProp.arraySize += 1;
      Rerender(root, property);
    }) { text = "+" };
  }

  // Draw the property inside the given rect
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    // EditorGUILayout.LabelField("Hello: " + property.FindPropertyRelative("_keys").arraySize);
  }
}