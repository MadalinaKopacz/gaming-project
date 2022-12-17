using System.Collections.Generic;
using UnityEngine;

// Generic class to help with serializing list
// improved from comment
// https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        List<T> result = new List<T>();

        // Start parsing
        // Accept format {"Items": [...]}

        if (!json.StartsWith("{") || !json.EndsWith("}"))
        {
            throw new System.Exception("Json invalid");
        } 

        json = json.TrimEnd('}');
        json = json.TrimStart('{');

        if (!json.StartsWith("\"Items\":") )
        {
            throw new System.Exception("Json invalid");
        }

        
        json = json.Substring(8);

        if (!json.StartsWith('[') || !json.EndsWith(']'))
        {
            throw new System.Exception("Json invalid");
        }

        json = json.TrimEnd(']');
        json = json.TrimStart('[');

        string[] items = json.Split("},");
        for (int i = 0; i < items.Length; i++)
        {
            if (i != items.Length - 1)
                items[i] = items[i] + "}";
            
            T transformedItem = JsonUtility.FromJson<T>(items[i]);
            result.Add(transformedItem);
        }

        return result;
    }

    public static string ToJson<T>(List<T> array)
    {
        Wrapper<string> wrapper = new Wrapper<string>();
        wrapper.Items = new List<string>();
        foreach(T item in array)
        {
            wrapper.Items.Add(JsonUtility.ToJson(item));
        }

        return JsonUtility.ToJson(wrapper).Replace("\\\"", "\"").
            Replace("[\"", "[").Replace("\"]", "]")
            .Replace("}\"", "}").Replace("\"{", "{");
    }

    public static string ToJson<T>(List<T> array, bool prettyPrint)
    {
        Wrapper<string> wrapper = new Wrapper<string>();
        wrapper.Items = new List<string>();
        foreach(T item in array)
        {
            wrapper.Items.Add(JsonUtility.ToJson(item, prettyPrint));
        }

        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}