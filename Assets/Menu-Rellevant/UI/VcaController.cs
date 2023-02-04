using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    [SerializeField] private AudioMixer controller;
    [SerializeField] private string variableName;

    private Slider slider;

    void Start()
    {
        string varName = variableName + "Volume";
        float startingVolume = PlayerPrefs.GetFloat(varName, 0.2f);
        slider = GetComponent<Slider>();
        controller.SetFloat(varName, Float2DB(startingVolume));
        slider.value = startingVolume;
    }

    public void SetVolume(float newVolume)
    {
        string varName = variableName + "Volume";
        controller.SetFloat(varName, Float2DB(newVolume));
        PlayerPrefs.SetFloat(varName, newVolume);
    }

    private float Float2DB(float value)
    {
        float db;

        if (value > 0.0f)
            db = 20.0f * Mathf.Log10(value);
        else
            db = -144.0f;  // effectively minus infinity

        return db;
    }
}