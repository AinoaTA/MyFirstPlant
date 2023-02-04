using System;
using System.Collections;
using System.Collections.Generic;
using Cutegame.Loading;
using Cutegame.Utils;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuState
{
    Main,
    Options,
    Controls,
    Localization,
    Credits,
    Shop
}

namespace Cutegame.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        private MenuState currentMenu = MenuState.Main;
        private MenuState previousMenu;

        public CanvasFadeIn MainCanvas;
        public CanvasFadeIn OptionsCanvas;
        public CanvasFadeIn ControlsCanvas;
        public CanvasFadeIn LocalizationCanvas;
        public CanvasFadeIn CreditsCanvas;
        public CanvasFadeIn Fade;
        public float Duration = 0.4f;

        [SerializeField] private GameStateCanvasTable gameStateCanvasTable;

        private readonly string twitterNameParameter =
            "Juega a este increíble juego creado por @andrew_raaya @JordiAlbaDev @Sergisggs @GuillemLlovDev @Belmontes_ART @montane @ovillaloboss_ y @RenderingCode hecho para la #IndieSpainJam (@IndieDevDay @spaingamedevs)!\n\nAquí tenéis el link:\n\n";

        private readonly string twitterDescriptionParam = "";
        private readonly string twitterAdress = "https://twitter.com/intent/tweet";
        private readonly string jamLink = "https://andrew-raya.itch.io/cosmic-defender";

        [SerializeField] private string boatSceneName = "Barco";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 1f;

            gameStateCanvasTable.Add(MenuState.Main, MainCanvas);
            gameStateCanvasTable.Add(MenuState.Options, OptionsCanvas);
            gameStateCanvasTable.Add(MenuState.Controls, ControlsCanvas);
            gameStateCanvasTable.Add(MenuState.Localization, LocalizationCanvas);
            gameStateCanvasTable.Add(MenuState.Credits, CreditsCanvas);

            foreach (var item in gameStateCanvasTable)
            {
                if (item.Key == MenuState.Main)
                    item.Value.Show();
                else
                    item.Value.Hide();
            }
        }

        public void PlayGame()
        {
            LoadScene("LoadingScreen");
        }

        public void LoadScene(string sceneName)
        {
            //BackgroundMusic.Stop();
            MainCanvas.GetComponent<CanvasGroup>().interactable = false;
            //LoadingScreen.Show();
            StartCoroutine(LoadAfterFade(sceneName, 2f));
            PlayClickSound();
        }

        IEnumerator LoadAfterFade(string sceneName, float duration)
        {
            Fade.Show();
            yield return new WaitForSecondsRealtime(duration);
            Debug.Log("Should load scene");
            SceneManager.LoadScene(sceneName);
        }

        public void PlayHoverSound()
        {
            //HoverSoundRef.Play();
        }

        public void PlayClickSound()
        {
            // ClickSoundRef.Play();
            //CircleMenuAnimator.SetTrigger("ButtonPressed");
            //PlayerMenuAnimator.SetTrigger("ButtonPressed");
            //startGameButton.onClick.AddListener(() => { PlayerMenuAnimator.SetTrigger("StartPressed"); });
        }

        public void MainMenuFade()
        {
            SetCanvas(MenuState.Main);
        }


        public void OptionsMenu()
        {
            SetCanvas(MenuState.Options);
        }

        public void ControlsMenu()
        {
            SetCanvas(MenuState.Controls);
        }

        public void LocalizationsMenu()
        {
            SetCanvas(MenuState.Localization);
        }

        public void CreditsMenu()
        {
            SetCanvas(MenuState.Credits);
        }

        public void SetCanvas(MenuState newMenu)
        {
            previousMenu = currentMenu;
            currentMenu = newMenu;

            gameStateCanvasTable[newMenu].Show();
            gameStateCanvasTable[previousMenu].Hide();

            PlayClickSound();
        }

        public void QuitGame()
        {
            Application.Quit();
            // if Unity Editor then remove Play.
        }

        public void ShareTwitter()
        {
            Application.OpenURL(twitterAdress + "?text=" +
                                UnityWebRequest.EscapeURL(twitterNameParameter + "\n" + twitterDescriptionParam + "\n" +
                                                          jamLink));
            PlayClickSound();
        }

        public void OpenTwitter(string profile)
        {
            Application.OpenURL($"https://twitter.com/{profile}");
            PlayClickSound();
        }

        //Links for Twitter
        public void JordiTwitter()
        {
            OpenTwitter("JordiAlbaDev");
        }

        public void AndrewTwitter()
        {
            OpenTwitter("andrew_raaya");
        }

        public void SergiTwitter()
        {
            OpenTwitter("Sergisggs");
        }

        [Serializable]
        public class GameStateCanvasTable : UnitySerializedDictionary<MenuState, CanvasFadeIn>
        {
        }

        [Serializable]
        public class GameStateButtonTable : UnitySerializedDictionary<MenuState, ButtonsList>
        {
        }

        [Serializable]
        public class ButtonsList
        {
            public List<Button> buttonsList;
        }
    }
}