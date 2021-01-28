using System.Collections.Generic;
using Dropecho;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SerializableList<>))]
public class SerializeableListDrawer : PropertyDrawer {
  public override VisualElement CreatePropertyGUI(SerializedProperty property) {
    var root = new VisualElement();
    root.AddToClassList("container");
    root.AddToClassList("rows");
    Rerender(root, property);
    return root;
  }

  private void Rerender(VisualElement root, SerializedProperty property) {
    property.serializedObject.ApplyModifiedProperties();

    root.Clear();
    var foldout = RenderFoldout(root, property);
    foreach (var row in RenderRows(root, property)) {
      foldout.Add(row);
    }
    root.Add(foldout);
    root.Add(RenderAddButton(root, property));
  }

  private VisualElement RenderFoldout(VisualElement root, SerializedProperty property) {
    var valuesProp = property.FindPropertyRelative("values");

    var container = new Foldout() {
      text = valuesProp.arraySize + " items",
      value = valuesProp.isExpanded
    };
    container.RegisterValueChangedCallback(ev => valuesProp.isExpanded = ev.newValue);
    container.AddToClassList("container");
    container.AddToClassList("fill");

    return container;
  }

  private List<VisualElement> RenderRows(VisualElement root, SerializedProperty property) {
    var valuesProp = property.FindPropertyRelative("values");

    var rows = new List<VisualElement>();
    for (var i = 0; i < valuesProp.arraySize; i++) {
      var row = new VisualElement();
      row.AddToClassList("container");
      row.AddToClassList("rows");
      var currentIndex = i;
      var field = new PropertyField(valuesProp.GetArrayElementAtIndex(currentIndex), " ").BindAndReturn(property.serializedObject);

      var deleteButton = new Button(() => {
        valuesProp.DeleteArrayElementAtIndex(currentIndex);
        Rerender(root, property);
      }) {
        text = "-"
      };

      row.Add(field);
      row.Add(deleteButton);
      rows.Add(row);
    }

    return rows;
  }

  private VisualElement RenderAddButton(VisualElement root, SerializedProperty property) {
    var valuesProp = property.FindPropertyRelative("values");
    var button = new Button(() => {
      valuesProp.arraySize += 1;
      Rerender(root, property);
    }) {
      text = "+"
    };
    return button;
  }

  // Draw the property inside the given rect
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    // EditorGUILayout.LabelField("Hello: " + property.FindPropertyRelative("_keys").arraySize);
  }
}