using System;
using Cutegame.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Cutegame
{
    public class InteractableObject : MonoBehaviour, IInteractable, IPreviewable
    {
        [Header("Interactable Settings")]
        [SerializeField] private bool InteractableOnStart = true;
        [SerializeField] private bool PreviewableOnStart = true;

        private bool IsBeingPreviewed = false;
        private bool IsBeingInteracted = false;
        public bool IsInteractable { get; set; }        
        public bool IsPreviewable { get; set; }

        public Action OnStartInteraction { get; set; }
        public Action OnEndInteraction { get; set; }
        public Action OnStartPreview { get; set; }
        public Action OnEndPreview { get; set; }
        
        public UnityEvent OnInteraction = new UnityEvent();

        private void Awake()
        {
            IsInteractable = InteractableOnStart;
            IsPreviewable = PreviewableOnStart;
        }

        public void StartInteraction()
        {
            if (!IsInteractable) return;
            //Debug.Log($"Interacting with {gameObject.name}");
            OnStartInteraction?.Invoke();
            IsInteractable = false;
            IsBeingInteracted = true;
        }

        public void EndInteraction()
        {
            if (!IsBeingInteracted) return;
            //Debug.Log($"Ending interaction with {gameObject.name}");
            OnEndInteraction?.Invoke();
            IsInteractable = true;
            IsBeingInteracted = false;
        }

        public void StartPreview()
        {
            if (!IsPreviewable) return;
            //Debug.Log($"Starting preview for {gameObject.name}");
            OnStartPreview?.Invoke();
            IsBeingPreviewed = true;
            IsPreviewable = false;
        }

        public void EndPreview()
        {
            if (!IsBeingPreviewed) return;
            //Debug.Log($"Ending preview for {gameObject.name}");
            OnEndPreview?.Invoke();
            IsPreviewable = true;
            IsBeingPreviewed = false;
        }
    }
}