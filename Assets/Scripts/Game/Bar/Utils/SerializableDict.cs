using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDict<T>
{
    public List<SerializeData<T>> data;
    private Dictionary<string, T> dict = new Dictionary<string, T>();

    public Dictionary<string, T> GetDict()
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (dict.ContainsKey(data[i].key))
            {
                continue;
            }
            dict.Add(data[i].key, data[i].value);
        }
        return dict;
    }
}

[Serializable]
public class SerializeData<T>
{
    public string key;
    public T value;
}