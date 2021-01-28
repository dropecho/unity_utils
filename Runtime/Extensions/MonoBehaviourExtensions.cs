using System;
using System.Collections;
using UnityEngine;

namespace Dropecho {
  public static class MonoBehaviourExtensions {
    public static Coroutine RunContinuously(this MonoBehaviour behavior, Action action, float timeIncrement = 1) {
      return behavior.StartCoroutine(CoroutineActionContinous(action, timeIncrement));
    }

    public static Coroutine RunAfterDelay(this MonoBehaviour behavior, Action action, float delay = 0) {
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