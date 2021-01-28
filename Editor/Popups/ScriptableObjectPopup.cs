using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dropecho {
  public class ScriptableObjectPopup<TValue, TWindow> : EditorWindow
      where TWindow : ScriptableObjectPopup<TValue, TWindow>
      where TValue : ScriptableObject {

    public static void Open() {
      // Use create instance instead of get window so popup styles are applied correctly.
      var window = CreateInstance<TWindow>();
      window.position = new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 4, Screen.height / 4);
      window.ShowPopup();
    }

    void CreateGUI() {
      rootVisualElement
        .AddStyleSheet("Assets/Scripts/Utils/Editor/Popups/Index.uss")
        .AddToClassList("popup");
        
      Render();
    }

    VisualElement Render() {
      rootVisualElement.Clear();
      return rootVisualElement.Add(
        RenderHeader(),
        RenderFields(),
        RenderFooter()
      );
    }

    private string _path;
    protected string _pathTemplate;

    private VisualElement RenderHelpLabel() {
      return new Label(string.Format("Asset Path: {0}",
          string.Format(_pathTemplate, "{Asset Name}")
        )).AddToClassList("help", "");
    }

    private VisualElement RenderHeader() {
      return new VisualElement()
        .AddToClassList("header", "")
        .Add(
          new Label("Create " + ObjectNames.NicifyVariableName(typeof(TValue).Name)),
          new Button(Close) { text = "X" }
        );
    }

    private VisualElement RenderFields() {
      var nameField = new TextField() {
        label = "Asset Name",
        value = _path,
        name = "field-name",
        tooltip = "The name and type name of the story generator asset file you want to save. (Required)"
      };
      nameField.RegisterValueChangedCallback(ev => {
        _path = ev.newValue;
        if (!string.IsNullOrEmpty(_path)) {
          nameField.RemoveFromClassList("error");
        }
      });

      return new VisualElement()
        .AddToClassList("form", "")
        .AddChildren(RenderHelpLabel(), nameField);
    }

    private VisualElement RenderFooter() {
      return new VisualElement()
        .AddToClassList("footer", "")
        .Add(
          new Button(Save) { text = "OK" },
          new Button(Close) { text = "Cancel" }
        );
    }

    private void Save() {
      if (string.IsNullOrWhiteSpace(_path)) {
        rootVisualElement.Q<TextField>("field-name").AddToClassList("error");
      } else if (string.IsNullOrWhiteSpace(_pathTemplate)) {
        Debug.LogError(string.Format(
          "Popup editor has an invalid path template for {0}. Try setting _pathTemplate in the popup windows on enable function.",
          typeof(TValue).Name
        ) + "\nExample Asset Path Template: Assets/Data/MyDataType/{0}.asset");
      } else {
        AssetDatabase.OpenAsset(Utils.CreateAsset<TValue>(string.Format(_pathTemplate, _path)));
        Close();
      }
    }
  }
}