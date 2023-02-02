using UnityEngine;

public class AdjustSafeArea : MonoBehaviour
{
    public Canvas _canvas;

    void Start()
    {
        if (_canvas == null)
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        if (_canvas == null)
        {
            GetComponent<Canvas>();
        }

        if (_canvas == null)
        {
            return;
        }

        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        var panelSafeArea = GetComponent<RectTransform>();
        if (panelSafeArea == null || Screen.safeArea == null)
        {
            return;
        }

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= _canvas.pixelRect.width;
        anchorMin.y /= _canvas.pixelRect.height;

        anchorMax.x /= _canvas.pixelRect.width;
        anchorMax.y /= _canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
    }
}