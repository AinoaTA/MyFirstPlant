using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    [HideInInspector] public bool isLoading;
    [SerializeField] private string loadingScene = "Loading";

    private void Start()
    {
        Main.instance.managerScene = this;
    }

    /// <summary>
    /// Load a scene with a loadingScreen
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void LoadSceneWithLoading(string sceneToLoad)
    {
        StartCoroutine(Loading(sceneToLoad));
    }

    IEnumerator Loading(string sceneToLoad)
    {
        SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Single);
        AsyncOperation nextScene = SceneManager.LoadSceneAsync(sceneToLoad);
        nextScene.allowSceneActivation = false;

        while (!nextScene.isDone)
        {
            if (nextScene.progress >= 0.9f && !isLoading)
            {
                yield return new WaitForSeconds(1f);
                nextScene.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Load a scene without loading screen. Perfect for splash screens
    /// </summary>
    public void LoadFast(string sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}