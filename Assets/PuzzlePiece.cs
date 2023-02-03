using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public static PuzzlePiece GrabbedPiece;

    public Vector2 correctPosition;

    private Action<PuzzlePiece> correctCheck;

    public bool isDone = false;

    private float acceptationRange = 5f;

    private Image _image;


    private Color originalColor;

    public void Setup(Vector2 correctPosition, Vector2 spawnPosition, float range, Sprite sprite, Action<PuzzlePiece> checkFunction)
    {
        this.correctPosition = correctPosition;
        transform.position = spawnPosition;
        //Debug.Log($"{correctPosition}\n{spawnPosition}");
        correctCheck += checkFunction;
        acceptationRange = range;
        _image = GetComponent<Image>();
        _image.sprite = sprite;
    }
    
    public void Grab()
    {
        if (GrabbedPiece != null) GrabbedPiece.LeavePiece();

        GrabbedPiece = this;
        GrabInteraction();
    }

    private void GrabInteraction()
    {
        // Goes to the right position
        // Change COlor?
        originalColor = _image.color;
        var newCol = originalColor;
        newCol.a = 0.5f;
        _image.color = newCol;
    }

    private void FixedUpdate()
    {
        if(this == GrabbedPiece) TickWhileGrabbed(Input.mousePosition);
    }

    public void TickWhileGrabbed(Vector3 position)
    {
        //if (this == GrabbedPiece)
        //{
            transform.position = position;
        //}
    }
    
    public void LeavePiece()
    {
        GrabbedPiece = null;
        LeaveInteraction();
    }

    private void LeaveInteraction()
    {
        var dis = Vector2.Distance((Vector2)transform.localPosition, correctPosition);
        //Debug.Log($"Distance: {dis}");
        if (dis < acceptationRange)
        {
            // Set interactable to false.
            // Trigger the done event.
            correctCheck?.Invoke(this);
            transform.localPosition = correctPosition;
            isDone = true;
            _image.raycastTarget = false;
        }
        _image.color = originalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDone) return;

        if(GrabbedPiece == null)
            Grab();
        else if(GrabbedPiece == this)
            LeavePiece();
    }
}