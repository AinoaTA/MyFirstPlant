using System;

public interface ISaveGameService : IDisposable
{
    bool Has(string key);
    void Delete(string key);

    int Get(string key, int defaultValue);
    string Get(string key, string defaultValue);
    float Get(string key, float defaultValue);
    T Get<T>(string key) where T : class;

    void Set(string key, int value);
    void Set(string key, string value);
    void Set(string key, float value);
    void Set<T>(string key, T value) where T : class;
}