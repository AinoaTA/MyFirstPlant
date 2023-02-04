using System;
using Cutegame.Audio;
using UnityEngine;

namespace Cutegame
{
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text _levelText = null;
        [SerializeField] private GameObject _mutedAudio = null;
        [SerializeField] private GameObject _mutedFX = null;
        [SerializeField] private GameObject _undoButton = null;
        [SerializeField] private TMPro.TMP_Text _undoText = null;

        public Action OnUndo;
        public Action OnRestart;

        protected void Awake()
        {
            // 
            int volumeM = PlayerPrefs.GetInt("VOLUME_M", 1);
            //int volumeM = PlayerPrefs.GetInt("VOLUME_M", 1);
            _mutedAudio.SetActive(volumeM < 1);
            AudioManager.Instance.SetMusicVolume(volumeM);
            int volumefx = PlayerPrefs.GetInt("VOLUME_FX", 1);
            _mutedFX.SetActive(volumefx < 1);
            AudioManager.Instance.SetSFXVolume(volumefx);
        }

        public void SetLevel(int level)
        {
            _levelText.text = level.ToString();
        }

        public void SetUndo(int undo)
        {
            _undoText.text = undo.ToString();
            _undoButton.SetActive(undo > 0);
        }

        public void ToggleAudio()
        {
            int volume = PlayerPrefs.GetInt("VOLUME_M", 1);
            volume = volume > 0 ? 0 : 1;
            PlayerPrefs.SetInt("VOLUME_M", volume);
            _mutedAudio.SetActive(volume < 1);
            AudioManager.Instance.SetMusicVolume(volume);
        }

        public void ToggleFXAudio()
        {
            int volume = PlayerPrefs.GetInt("VOLUME_FX", 1);
            volume = volume > 0 ? 0 : 1;
            PlayerPrefs.SetInt("VOLUME_FX", volume);
            _mutedFX.SetActive(volume < 1);
            AudioManager.Instance.SetSFXVolume(volume);
        }

        public void Undo() => OnUndo?.Invoke();

        public void Restart() => OnRestart?.Invoke();
    }
}