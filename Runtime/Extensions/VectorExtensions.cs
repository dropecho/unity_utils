using UnityEngine;

namespace Dropecho {
  public static class VectorExtensions {
    public static Vector3 ClampX(this Vector3 vector, float min, float max) {
      vector.x = Mathf.Clamp(vector.x, min, max);
      return vector;
    }

    public static Vector3 ClampY(this Vector3 vector, float min, float max) {
      vector.y = Mathf.Clamp(vector.y, min, max);
      return vector;
    }
    
    public static Vector3 ClampZ(this Vector3 vector, float min, float max) {
      vector.z = Mathf.Clamp(vector.z, min, max);
      return vector;
    }
  }
}