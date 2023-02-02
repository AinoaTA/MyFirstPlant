using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cutegame
{
    public class SettingsMenu : MonoBehaviour
    {
        public TMPro.TMP_Dropdown resolutionDropdown;

        public Toggle fullscreenToggle;

        Resolution[] resolutions;

        private void Start()
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            int iter = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                if (options.Contains(option.ToLower()))
                {
                    continue;
                }

                options.Add(option);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = iter;
                }

                iter++;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            fullscreenToggle.isOn = Screen.fullScreen;
        }
        
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        private void OnEnable()
        {
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }

        private void OnDisable()
        {
            fullscreenToggle.onValueChanged.RemoveListener(SetFullscreen);
        }

        public void SetResolution(int resolutionIndex)
        {
            var width = int.Parse(resolutionDropdown.options[resolutionIndex].text.Split('x')[0]);
            var height = int.Parse(resolutionDropdown.options[resolutionIndex].text.Split('x')[1]);

            Screen.SetResolution(width, height, Screen.fullScreen);
        }
    }
}