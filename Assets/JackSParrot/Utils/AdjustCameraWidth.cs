using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AdjustCameraWidth : MonoBehaviour
{
    [System.Serializable]
    public class AspectRatioCategory
    {
        public string Name;
        public int Width;
        public int Height;
        public float Aspect => Width / (float)Height;
        public float DesiredWidth;
    }

    public AspectRatioCategory CurrentAspect { get; private set; } 

    [SerializeField] List<AspectRatioCategory> _aspects = null;
    Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        float aspect = Screen.width / (float)Screen.height;
        _aspects.Sort((a, b) => a.Aspect.CompareTo(b.Aspect));
        float prevDiff = Mathf.Abs(aspect - _aspects[0].Aspect);
        CurrentAspect = _aspects[0];
        for (int i = 1; i < _aspects.Count; ++i)
        {
            var diff = Mathf.Abs(aspect - _aspects[i].Aspect);
            if (diff > prevDiff)
            {
                CurrentAspect = _aspects[i];
                break;
            }
            prevDiff = diff;
        }
        UpdateSize();
    }

    public void UpdateSize()
    {
        float unitsPerPixel = CurrentAspect.DesiredWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        _camera.orthographicSize = desiredHalfHeight;
    }
}