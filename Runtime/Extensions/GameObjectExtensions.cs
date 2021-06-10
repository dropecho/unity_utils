using UnityEngine;

public static class GameObjectExtensions {
  public static bool HasComponent<T>(this GameObject obj) where T : Behaviour {
    return obj.GetComponent<T>() != null;
  }

  public static void EnableComponent<T>(this GameObject obj) where T : Behaviour {
    var component = obj.GetComponent<T>();
    if (component != null) component.enabled = true;
  }

  public static void DisableComponent<T>(this GameObject obj) where T : Behaviour {
    var component = obj.GetComponent<T>();
    if (component != null) component.enabled = false;
  }


  public static void SetEnabledOnComponent<T>(this Component obj, bool enabled) where T : Behaviour {
    var component = obj.GetComponent<T>();
    if (component != null) component.enabled = enabled;
  }

  public static void EnableComponent<T>(this Component obj) where T : Behaviour {
    var component = obj.GetComponent<T>();
    if (component != null) component.enabled = true;
  }

  public static void DisableComponent<T>(this Component obj) where T : Behaviour {
    var component = obj.GetComponent<T>();
    if (component != null) component.enabled = false;
  }
}