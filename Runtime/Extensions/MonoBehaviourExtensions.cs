using System;
using System.Collections;
using UnityEngine;

namespace Dropecho {
  public static class MonoBehaviourExtensions {
    public static Coroutine RunContinuously(this MonoBehaviour behavior, float timeIncrement, Action action) {
      return behavior.StartCoroutine(CoroutineActionContinous(action, timeIncrement));
    }

    public static Coroutine LerpFor(this MonoBehaviour behavior, float seconds, Action<float> callback) {
      return behavior.StartCoroutine(LerpOverTime(seconds, callback));
    }

    private static IEnumerator LerpOverTime(float seconds, Action<float> callback) {
      float elapsedTime = 0;
      while (elapsedTime < seconds) {
        callback(elapsedTime / seconds);
        elapsedTime += Time.deltaTime;
        yield return new WaitForEndOfFrame();
      }
    }

    public static Coroutine RunAfterDelay(this MonoBehaviour behavior, float delay, Action action) {
      return behavior.StartCoroutine(CoroutineActionWithDelay(action, delay));
    }

    private static IEnumerator CoroutineActionContinous(Action action, float delay) {
      while (true) {
        action.Invoke();
        yield return new WaitForSeconds(delay);
      }
    }

    private static IEnumerator CoroutineActionWithDelay(Action action, float delay) {
      yield return new WaitForSeconds(delay);
      action.Invoke();
    }
  }
}