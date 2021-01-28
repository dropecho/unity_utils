using System;
using System.Collections.Generic;
using System.Linq;

namespace Dropecho {
  /// <summary>
  /// A wrapper around a list for saving nested lists, i.e. List<SerializableList<string>>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  [Serializable]
  public class SerializableList<T> {
    public T[] values;

    public static implicit operator List<T>(SerializableList<T> list) => list.values.ToList();
    public static implicit operator T[](SerializableList<T> list) => list.values;
    public static explicit operator SerializableList<T>(T[] arr) => new SerializableList<T>() { values = arr };

    public T this[int key] {
      get {
        return values[key];
      }
      set {
        values[key] = value;
      }
    }
  };
}