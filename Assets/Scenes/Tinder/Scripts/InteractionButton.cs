using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractionButton : MonoBehaviour
{
    [SerializeField] UnityEvent _clickUp;
    private void OnMouseUp()
    {
        _clickUp?.Invoke();
    }
}
