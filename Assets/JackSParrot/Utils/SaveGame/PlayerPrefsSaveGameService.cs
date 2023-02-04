using UnityEngine;

public class PlayerPrefsSaveGameService : ISaveGameService
{
    public bool Has(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public int Get(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public string Get(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public float Get(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public T Get<T>(string key) where T : class
    {
        return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key, "{}"));
    }

    public void Set(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void Set(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void Set(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void Set<T>(string key, T value) where T : class
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
    }

    public void Dispose()
    {
    }
}