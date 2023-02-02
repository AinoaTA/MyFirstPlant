using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutegame
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private CanvasFadeIn _canvasGroup;

        private bool IsPaused = false;
        public void CheckPause()
        {
            if (IsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            // 
            //_playerInput.SwitchCurrentActionMap("UI");
            //
            _canvasGroup.Show();
            // 
            Cursor.visible = true;

            IsPaused = true;
        }

        public void Continue()
        {
            Time.timeScale = 1f;
            //_playerInput.SwitchCurrentActionMap("Player");

            _canvasGroup.Hide();

            Cursor.visible = false;

            IsPaused = false;
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(1);
        }
    }
}