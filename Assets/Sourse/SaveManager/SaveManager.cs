using UnityEngine;

public static class SaveManager
{
    public static void Save<T>(string key, T savaData)
    {
        string jsonDataString = JsonUtility.ToJson(savaData, true);
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static T Load<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(loadedString);
        }
        else
        {
            return new T();
        }
    }
}
