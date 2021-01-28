using System.Linq;
using Dropecho;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ScriptableObjectEditorWindow<TValue> : EditorWindow
  where TValue : ScriptableObject {

  public static void Open<T>(TValue obj) where T : ScriptableObjectEditorWindow<TValue> {
    var window = GetWindow<T>(ObjectNames.NicifyVariableName(typeof(TValue).Name) + " Editor");
    window.minSize = new Vector2(800, 600);
    window.SetSelected(obj);
  }

  public virtual void CreateGUI() { }

  protected void SetSelected(TValue obj) {
    CreateGUI();

    if (obj) {
      var list = rootVisualElement.Q<ListView>("type-list");
      if (list != null) {
        var index = list.itemsSource.IndexOf(obj);
        list.SetSelection(index);
      }
    }
  }

  protected virtual ListView RenderTypeList() {
    var types = Utils.GetAssetsOfType<TValue>();

    var list = new ListView {
      name = "type-list",
      makeItem = () => new Label(),
      bindItem = (el, i) => (el as Label).text = types[i].name,
      itemsSource = types,
      itemHeight = 16,
      selectionType = SelectionType.Single
    };

    list.onSelectionChange += (selected) => {
      var rightPanel = rootVisualElement.Q<Box>("right-panel");
      rightPanel.Clear();
      rightPanel.Add(UIElementUtils.RenderDefaultEditorFor(selected.First() as TValue));
    };

    return list;
  }
}