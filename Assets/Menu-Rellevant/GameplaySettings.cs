using System;
using UnityEngine;

namespace Cutegame.Options
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Lost Vessel/" + nameof(GameplaySettings))]
    public class GameplaySettings : ScriptableObject
    {
        private const string CutegameSettingsKey = nameof(CutegameSettingsKey);

        [SerializeField] private SettingsData data;

        public Action OnSettingsUpdated;

        public void Initialize()
        {
            data = new SettingsData()
            {
                MouseSensitivity = 10f,
            };
            Load(data);
        }
        
        public void SetMouseSensitivity(float value)
        {
            data.MouseSensitivity = value;
            Save();
        }
        public float GetSensitivity() => data.MouseSensitivity;

        private void Load(SettingsData settings)
        {
            try
            {
                var loadedData =
                    JsonUtility.FromJson<SettingsData>(PlayerPrefs.GetString(CutegameSettingsKey));
                
                Debug.Log(loadedData.MouseSensitivity);
                data = loadedData;
            }
            catch (NullReferenceException)
            {
                
            }
        }

        private void Save()
        {
            PlayerPrefs.SetString(CutegameSettingsKey, JsonUtility.ToJson(data));
            OnSettingsUpdated?.Invoke();
        }
    }

    [Serializable]
    public struct SettingsData
    {
        public float MouseSensitivity;
    }
}