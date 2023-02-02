using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutegame.Loading
{
    [CreateAssetMenu(fileName = "LoadSceneData", menuName = "Lost Vessel/" + nameof(LoadSceneData))]
    public class LoadSceneData : ScriptableObject
    {
        private string sceneName;

        private Scene currentScene;

        private AsyncOperation loadOperation;

        public AsyncOperation SetLoadedSceneAdditive(bool allowActivation = false)
        {
            try
            {
                currentScene = SceneManager.GetActiveScene();
                loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                loadOperation.allowSceneActivation = allowActivation;
                return loadOperation;
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not load scene because {e.Message}");
                loadOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
                loadOperation.allowSceneActivation = true;
                return loadOperation;
            }
        }

        public bool SetSceneName(string sceneName)
        {
            // Check if scene exists but okay.
            this.sceneName = sceneName;
            return true;
        }
    }
}