using UnityEngine;

namespace Cutegame
{
    [RequireComponent(typeof(Animator))]
    public class Door : InteractableObject
    {
        [SerializeField] private bool IsOpen = false;

        [SerializeField] private Animator _animatorController;

        [SerializeField] private AudioSource doorAudio;

        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private Collider _collider;

        [Header("Audios")] [SerializeField] private AudioClip openClip;
        [SerializeField] private AudioClip closeClip;
        private void SwitchOpenDoor()
        {
            // Sound?
            
            // Animation?
            if (!IsOpen)
            {
                _animatorController.SetTrigger("Open");
                doorAudio.clip = openClip;
                Debug.Log($"Opening {gameObject.name}.");
            }
            else
            {
                _animatorController.SetTrigger("Close");
                doorAudio.clip = closeClip;
                Debug.Log($"Closing {gameObject.name}.");
            }
            doorAudio.Play();
            IsOpen = !IsOpen;
            _collider.isTrigger = IsOpen;
            // Open door?
            // Change collider? just animation.
        }

        private void Preview()
        {
            _meshRenderer.material.SetInt("_EnableFresnel", 1);
        }

        private void StopPreview()
        {
            _meshRenderer.material.SetInt("_EnableFresnel", 0);
        }

        // On Animation Finished, call back the function that enables interaction agane
        public void OnAnimationFinished()
        {
            IsInteractable = true;
        }

        private void OnEnable()
        {
            OnStartInteraction += SwitchOpenDoor;
            OnStartPreview += Preview;
            OnEndPreview += StopPreview;
        }

        private void OnDisable()
        {
            OnStartInteraction -= SwitchOpenDoor;
            OnStartPreview -= Preview;
            OnEndPreview -= StopPreview;
        }
    }
}