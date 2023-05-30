using System;
using System.IO;
using UnityEngine;
using VContainer.Unity;

namespace Utility.Jsons
{
    public class JsonSerialization : IJsonSerialization, IStartable
    {
        private string baseFilePath;

        public void Start()
        {
            baseFilePath = Path.Combine(Application.persistentDataPath, "FantasyBank");
        }

        public void WriteDataToFile<T>(T dataObj, string fileName)
        {
            var jsonString = ToJson(dataObj);
        }

        public void WriteDataToFile<T>(T[] dataObjArray, string fileName)
        {
            var jsonString = ToJson(dataObjArray);

        }

        private string ToJson<T>(T[] array)
        {
            ArrayWrapper<T> wrapper = new ArrayWrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        private string ToJson<T>(T obj)
        {
            SingleObjWrapper<T> wrapper = new SingleObjWrapper<T>();
            wrapper.Item = obj;
            return JsonUtility.ToJson(wrapper);
        }


        [Serializable]
        private class ArrayWrapper<T>
        {
            public T[] Items;
        }

        [Serializable]
        private class SingleObjWrapper<T>
        {
            public T Item;
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
