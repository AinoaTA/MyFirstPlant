using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private float _maxTimeLoading = 5f;
    private void Start()
    {
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        Main.instance.managerScene.isLoading = true;
        float t = 0;

        while (t < _maxTimeLoading)
        {
            t += 0.02f;
            _slider.value = Mathf.Lerp(0, 1, t / _maxTimeLoading);
            yield return null;
        }

        Main.instance.managerScene.isLoading = false;
    }
}