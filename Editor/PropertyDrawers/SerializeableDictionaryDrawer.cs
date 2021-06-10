using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Dropecho;
using UnityEditor.EditorTools;
using System;
using System.Collections;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>), false)]
[CustomPropertyDrawer(typeof(SerializableListDictionary<,>), false)]
public class SerializedDictionaryDrawer : PropertyDrawer {
  public override VisualElement CreatePropertyGUI(SerializedProperty property) {
    return Render(new VisualElement().AddToClassList("container", "fill"), property);
  }

  private VisualElement Render(VisualElement root, SerializedProperty property) {
    return root.Replace(RenderHeader(root, property), RenderRows(root, property));
  }

  private VisualElement RenderHeader(VisualElement root, SerializedProperty property) {
    var keysProp = property.FindPropertyRelative("_keys");
    var button = new Button(() => AddRow(root, property)) { text = "+" };

    if (keysProp.arraySize >= keysProp.enumNames.Length) {
      button.SetEnabled(false);
    }

    return new VisualElement()
      .AddToClassList("container", "rows")
      .Add(
        new Label(property.displayName + " : " + property.FindPropertyRelative("_keys").arraySize + " items"),
        button
      );
  }

  private VisualElement RenderRows(VisualElement root, SerializedProperty property) {
    var container = new VisualElement().AddClasses("container", "fill");
    var keysProp = property.FindPropertyRelative("_keys");
    var valuesProp = property.FindPropertyRelative("_values");

    for (var i = 0; i < keysProp.arraySize; i++) {
      var currentIndex = i;

      var key = keysProp.GetArrayElementAtIndex(currentIndex);
      var value = valuesProp.GetArrayElementAtIndex(currentIndex);

      container.Add(
        new VisualElement().AddToClassList("container", "rows")
          .Add(
            RenderKeyField(key, keysProp, currentIndex),
            new PropertyField(value)
              .AddClasses("dictionary-field")
              .BindAndReturn(value.serializedObject),
            new Button(() => DeleteRow(root, property, currentIndex)) { text = "-" }
          )
      );
    }
    return container;
  }

  private VisualElement RenderKeyField(SerializedProperty key, SerializedProperty keys, int currentIndex) {
    var field = new PropertyField(key, " ")
      .AddClasses("dictionary-field")
      .BindAndReturn(key.serializedObject);

    return field;
  }

  private void AddRow(VisualElement root, SerializedProperty property) {
    var keysProp = property.FindPropertyRelative("_keys");
    var valuesProp = property.FindPropertyRelative("_values");

    keysProp.InsertArrayElementAtIndex(keysProp.arraySize);
    valuesProp.InsertArrayElementAtIndex(valuesProp.arraySize);

    var keyType = keysProp.arrayElementType;
    var key = keysProp.GetArrayElementAtIndex(keysProp.arraySize - 1);

    if (keyType == "string") {
      key.stringValue += "(copy)";
    } else if (keyType == typeof(Enum).Name && key != null) {
      var unused = 0;
      for (var i = 0; i < keysProp.arraySize; i++) {
        if (keysProp.GetArrayElementAtIndex(i).enumValueIndex != i) {
          break;
        }
      }

      key.enumValueIndex = unused;
    }
    property.serializedObject.ApplyModifiedProperties();
    Render(root, property);
  }

  private void DeleteRow(VisualElement root, SerializedProperty property, int index) {
    property.FindPropertyRelative("_keys").DeleteArrayElementAtIndex(index);
    property.FindPropertyRelative("_values").DeleteArrayElementAtIndex(index);
    property.serializedObject.ApplyModifiedProperties();
    Render(root, property);
  }

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    // Using BeginProperty / EndProperty on the parent property means that prefab override logic works on the entire property.
    EditorGUI.BeginProperty(position, label, property);

    // Draw label
    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    // Don't make child fields be indented
    var indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0;

    // Show not supported label.
    EditorGUI.LabelField(position, "SerializableDictionary not supported in IMGUI");

    // Set indent back to what it was
    EditorGUI.indentLevel = indent;

    EditorGUI.EndProperty();
  }
}