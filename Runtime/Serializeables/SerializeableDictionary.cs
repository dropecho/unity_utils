using System;
using System.Collections;
using System.Collections.Generic;
using Dropecho;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IDictionary, ISerializationCallbackReceiver {
  [SerializeField]
  protected List<TKey> _keys = new List<TKey>();
  [SerializeField]
  protected List<TValue> _values = new List<TValue>();

  // save the dictionary to lists
  void ISerializationCallbackReceiver.OnBeforeSerialize() {
    _keys.Clear();
    _values.Clear();
    foreach (KeyValuePair<TKey, TValue> pair in this) {
      _keys.Add(pair.Key);
      _values.Add(pair.Value);
    }
  }

  // load dictionary from lists
  void ISerializationCallbackReceiver.OnAfterDeserialize() {
    Clear();

    if (_keys.Count != _values.Count) {
      throw new Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", _keys.Count, _values.Count));
    }

    for (int i = 0; i < _keys.Count; i++) {
      Add(_keys[i], _values[i]);
    }
  }
}

/// <summary>
/// A dictionary that holds a nested list of the value type, i.e. string becomes string[]
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[Serializable]
public class SerializableListDictionary<TKey, TValue> : Dictionary<TKey, TValue[]>, IDictionary, ISerializationCallbackReceiver {
  [SerializeField]
  private List<TKey> _keys = new List<TKey>();
  [SerializeField]
  private List<SerializableList<TValue>> _values = new List<SerializableList<TValue>>();

  // save the dictionary to lists
  void ISerializationCallbackReceiver.OnBeforeSerialize() {
    _keys.Clear();
    _values.Clear();
    foreach (var pair in this) {
      _keys.Add(pair.Key);
      _values.Add(new SerializableList<TValue>() { values = pair.Value });
    }
  }

  // load dictionary from lists
  void ISerializationCallbackReceiver.OnAfterDeserialize() {
    Clear();

    if (_keys.Count != _values.Count) {
      throw new Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", _keys.Count, _values.Count));
    }

    for (int i = 0; i < _keys.Count; i++) {
      Add(_keys[i], _values[i].values);
    }
  }
}