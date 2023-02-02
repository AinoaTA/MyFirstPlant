using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cutegame
{
    public class InteractModule : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private LayerMask interactLayerMask;
        [SerializeField] private Camera camReference;
        private List<Collider> detectedCollisions = new List<Collider>();

        private InteractableObject _si;
        private InteractableObject selectedInteractable
        {
            get => _si;
            set
            {
                if (_si != null)
                {
                    _si.EndPreview();
                }

                _si = value;
            }
        }

        private void Update()
        {
            // Maybe reduce the number of calls per second. Or tie it to camera movement.
            // Or reduce the checks.
            CastAndDetect();
            PreviewInteractable();
        }

        private void PreviewInteractable()
        {
            if (selectedInteractable != null && selectedInteractable.IsPreviewable)
            {
                selectedInteractable.StartPreview();
            }
        }

        private void UseInteractable()
        {
            //Debug.Log($"Using {selectedInteractable.gameObject.name}.");
            if (selectedInteractable == null) return;
            selectedInteractable.StartInteraction();
        }

            // E uses it?
        public void OnInteract()
        {
            UseInteractable();
        }

        private void CastAndDetect()
        {
            // Not only cast a raycast maybe more?
            var a = Physics.OverlapSphere(_sphereCollider.transform.position, _sphereCollider.radius, interactLayerMask);
            if (a.Length <= 0)
            {
                // None and then reset.
                selectedInteractable = null;
                detectedCollisions = new List<Collider>();
                return;
            }

            foreach (var item in a)
            {
                if(item.GetComponent<InteractableObject>() != null)
                    detectedCollisions.Add(item);
            }
            
            // Order by proximity to the center of the screen and select the closest one.
            var closest = detectedCollisions.
                OrderBy(x => Vector2.Distance((Vector2)camReference.WorldToViewportPoint(x.transform.position), new Vector2(0.5f,0.5f)))
                .First();

            if (selectedInteractable != null && selectedInteractable.gameObject == closest.gameObject) return;

            selectedInteractable = closest.GetComponent<InteractableObject>();
        }
    }
}