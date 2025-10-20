using UnityEngine;
using System;

[Serializable]
public class PlayerPrefsWrapper<T>
{
    public int Version;
    public T Data;
}

public enum SaveLoadResult
{
    Success,
    Overwrite,
    Fail
}

public static class PlayerPrefsUtility
{
    public static SaveLoadResult TryAddChangesToPlayerPrefs<T>(string key, T instance, int version = 1, bool writeToFile = false) where T : class
    {
        var result = SaveLoadResult.Fail;
        try
        {
            if(string.IsNullOrWhiteSpace(key) || key == "")
            {
                Debug.LogError("Key cannot be null or whitespace.");
                return SaveLoadResult.Fail;
            }

            if (instance == null)
            {
                Debug.LogError($"Cannot save null instance for key '{key}'.");
                return SaveLoadResult.Fail;
            }

            PlayerPrefsWrapper<T> wrapper = new PlayerPrefsWrapper<T>
            {
                Version = version,
                Data = instance
            };

            var json = JsonUtility.ToJson(wrapper);

            if (string.IsNullOrEmpty(json) || json == "{}")
            {
                Debug.LogError($"Serialization failed for key '{key}'. Object may not be serializable.");
                return SaveLoadResult.Fail;
            }

            if(PlayerPrefs.HasKey(key))            
                result = SaveLoadResult.Overwrite;
            else            
                result = SaveLoadResult.Success;

            PlayerPrefs.SetString(key, json);
            if (writeToFile)
                WriteToPlayerPrefs();

            return result;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save data for key '{key}': {e.Message}");
            return SaveLoadResult.Fail;
        }
    }

    public static void WriteToPlayerPrefs()
    {
        PlayerPrefs.Save();
    }

    public static bool TryLoad<T>(string key, out T instance, int expectedVersion = 1) where T : class
    {
        instance = default;

        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning($"Key '{key}' not found in PlayerPrefs.");
            return false;
        }

        string json = PlayerPrefs.GetString(key);

        try
        {
            var wrapper = JsonUtility.FromJson<PlayerPrefsWrapper<T>>(json);

            if (wrapper == null)
            {
                Debug.LogError($"Problem loading JSON for key '{key}'.");
                return false;
            }

            if (wrapper.Version != expectedVersion)
            {
                Debug.LogWarning($"Version mismatch for key '{key}': expected {expectedVersion}, got {wrapper.Version}.");
                return false;
            }

            instance = wrapper.Data;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to parse JSON for key '{key}': {e.Message}");
            return false;
        }
    }
}
