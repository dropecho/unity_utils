using UnityEngine;

namespace Dropecho {
  public static class InputUtils {
    public static Vector2 GetAxis2D(string xaxis, string yaxis) {
      return new Vector2(Input.GetAxis(xaxis), Input.GetAxis(yaxis));
    }
  }
}