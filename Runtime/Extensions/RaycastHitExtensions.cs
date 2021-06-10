using UnityEngine;

public static class RaycastHitExtensions {
  public static bool HasComponent<T>(this RaycastHit hit) where T : MonoBehaviour {
    return hit.transform?.gameObject?.GetComponent<T>() != null;
  }

  public static T GetComponent<T>(this RaycastHit hit) where T : MonoBehaviour {
    return hit.transform?.gameObject?.GetComponent<T>();
  }

  public static void EnableComponent<T>(this RaycastHit obj) where T : MonoBehaviour {
    var component = obj.GetComponent<T>();
    if (component != null) component.enabled = true;
  }

  public static void DisableComponent<T>(this RaycastHit obj) where T : MonoBehaviour {
    var component = obj.GetComponent<T>();
    if (component != null) component.enabled = false;
  }
}