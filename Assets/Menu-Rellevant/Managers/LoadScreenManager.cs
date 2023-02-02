using System;
using System.Collections;
using Cutegame.Subtitles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cutegame.Loading
{
    public class LoadScreenManager : MonoBehaviour
    {
        public bool CanStartScene = false;
        
        [SerializeField] private LoadSceneData loadSceneData;
        [SerializeField] private Image fillImage;
        [SerializeField] private TMP_Text pressAnyKeyText;
        
        [Header("Others")] [SerializeField] private DialogueData _dialogueData;

        private AsyncOperation newScene;
        private Coroutine LoadProgress;

        private void Awake()
        {
            _dialogueData.LoadAssetFile();
        }

        private void Start()
        {
            if (LoadProgress != null) return;
            newScene = loadSceneData.SetLoadedSceneAdditive();
            LoadProgress = StartCoroutine(WaitAsyncProgress(newScene, NewSceneLoadEnded));
        }

        IEnumerator WaitAsyncProgress(AsyncOperation scene, Action OnEnd)
        {
            yield return null;

            //When the load is still in progress, output the Text and progress bar
            while (scene.progress < 0.9f)
            {
                //Output the current progress
                Debug.Log($"Loading progress: {scene.progress * 100}%");
                fillImage.fillAmount = scene.progress;
            }
            
            Debug.Log($"Loading progress finished at {scene.progress * 100}%");
            
            OnEnd?.Invoke();
            fillImage.transform.parent.gameObject.SetActive(false);
            pressAnyKeyText.gameObject.SetActive(true);
            LoadProgress = null;
        }

        private void NewSceneLoadEnded()
        {
            // Show press any key.
            
            // Hide the bar or fully loaded.
            fillImage.fillAmount = 1f;
            // Startable scene
            CanStartScene = true;
        }

        public void OnAnyKey()
        {
            StartIt();
        }

        private void StartIt()
        {
            if (!CanStartScene) return;

            if (LoadProgress != null) return;
            
            CanStartScene = false;
            newScene.allowSceneActivation = true;

            // Destroy this scene first!
            // Fade to black all the block.
            // Start unloading scene.
      
            // Hide current scene?
            
            //var UnloadProgress = loadSceneData.UnloadCurrentScene();
            //LoadProgress = StartCoroutine(WaitAsyncProgress(UnloadProgress, DeloadSceneFinished));
        }

        private void DeloadSceneFinished()
        {
        }
    }
}