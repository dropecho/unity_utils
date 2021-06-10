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

    public static Vector3 Round(this Vector3 vec, int multiple = 1) {
      return new Vector3(
        RoundToMultiple(vec.x, multiple),
        RoundToMultiple(vec.y, multiple),
        RoundToMultiple(vec.z, multiple)
      );
      // vec.x = RoundToMultiple(vec.x, multiple);
      // vec.y = RoundToMultiple(vec.y, multiple);
      // vec.z = RoundToMultiple(vec.z, multiple);

      // return vec;
    }

    private static int RoundToMultiple(float value, int multiple) {
      return (int)Mathf.Round((value / (float)multiple)) * multiple;
    }
  }
}