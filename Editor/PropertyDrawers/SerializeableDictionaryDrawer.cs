using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Dropecho;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>), false)]
[CustomPropertyDrawer(typeof(SerializableListDictionary<,>), false)]
public class SerializedDictionaryDrawer : PropertyDrawer {
  public override VisualElement CreatePropertyGUI(SerializedProperty property) {
    var root = new VisualElement()
      .AddToClassList("container", "fill");
    Render(root, property);
    return root;
  }

  private void Render(VisualElement root, SerializedProperty property) {
    root.Clear();
    root.Add(RenderHeader(root, property), RenderRows(root, property));
  }

  private VisualElement RenderHeader(VisualElement root, SerializedProperty property) {
    return new VisualElement()
      .AddToClassList("container", "rows")
      .Add(
        new Label(property.displayName + " : " + property.FindPropertyRelative("_keys").arraySize + " items"),
        new Button(() => AddRow(root, property)) { text = "+" }
      );
  }

  private VisualElement RenderRows(VisualElement root, SerializedProperty property) {
    var container = new VisualElement().AddToClassList("container", "fill");
    var keysProp = property.FindPropertyRelative("_keys");
    var valuesProp = property.FindPropertyRelative("_values");

    for (var i = 0; i < keysProp.arraySize; i++) {
      var currentIndex = i;
      container.Add(
        new VisualElement().AddToClassList("container", "rows")
          .Add(
            new PropertyField(keysProp.GetArrayElementAtIndex(currentIndex), "Key:").BindAndReturn(property.serializedObject),
            new PropertyField(valuesProp.GetArrayElementAtIndex(currentIndex), "Value:").BindAndReturn(property.serializedObject),
            new Button(() => DeleteRow(root, property, currentIndex)) { text = "-" }
          )
      );
    }
    return container;
  }


  private void AddRow(VisualElement root, SerializedProperty property) {
    var keysProp = property.FindPropertyRelative("_keys");
    var valuesProp = property.FindPropertyRelative("_values");

    keysProp.arraySize += 1;
    keysProp.GetArrayElementAtIndex(keysProp.arraySize - 1).stringValue += "(copy)";
    valuesProp.arraySize += 1;
    property.serializedObject.ApplyModifiedProperties();
    Render(root, property);
  }

  private void DeleteRow(VisualElement root, SerializedProperty property, int index) {
    var keysProp = property.FindPropertyRelative("_keys");
    var valuesProp = property.FindPropertyRelative("_values");

    keysProp.DeleteArrayElementAtIndex(index);
    valuesProp.DeleteArrayElementAtIndex(index);
    property.serializedObject.ApplyModifiedProperties();
    Render(root, property);
  }

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUILayout.LabelField("Hello: " + property.FindPropertyRelative("_keys").arraySize);
  }
}