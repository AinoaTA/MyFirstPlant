using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Cutegame.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _is;

        public static AudioManager Instance
        {
            get => _is;
            set
            {
                if (_is != null)
                {
                    Destroy(value.gameObject);
                    return;
                }

                _is = value;
                DontDestroyOnLoad(_is.gameObject);
            }
        }

        [Header("Mixer")] 
        public AudioMixer Mixer;
        
        [Header("Audios")]
        [SerializeField] private AudioSource voAudio;
        [SerializeField] private AudioSource musicAudio;
        [SerializeField] private AudioSource soundAudio;

        private void Awake()
        {
            Instance = this;
        }

        public void PlaySound(AudioClip clip)
        {
            Debug.Log($"Playing {clip.name} as sound");
            soundAudio.clip = clip;
            soundAudio.Play();
        }

        public void PlayVO(AudioClip clip)
        {
            Debug.Log($"Playing {clip.name} as VO");
            voAudio.clip = clip;
            voAudio.Play();
        }

        public void PlayMusic(AudioClip clip)
        {
            Debug.Log($"Playing {clip.name} as music");
            musicAudio.clip = clip;
            musicAudio.Play();
        }

        public void SetMasterValue(float volume)
        {
            SetMixerVariable("Master", volume);
        }

        public void SetSFXVolume(float volume)
        {
            SetMixerVariable("SFX", volume);
        }

        public void SetMusicVolume(float volume)
        {
            SetMixerVariable("Music", volume);
        }
        
        public void SetAmbienceVolume(float volume)
        {
            SetMixerVariable("Ambience", volume);
        }

        private void SetMixerVariable(string name, float value)
        {
            Mixer.SetFloat(name, FloatToDb(value));
            PlayerPrefs.SetFloat(name, value);
        }

        private float FloatToDb(float value)
        {
            float db;

            if (value > 0.0f)
                db = 20.0f * Mathf.Log10(value);
            else
                db = -144.0f;  // effectively minus infinity

            return db;
        }
    }
}