using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cutegame.Audio;
using Cutegame.Subtitles;
using UnityEngine;

namespace Cutegame
{
    public class ObjectWithDialogue : InteractableObject
    {
        [SerializeField] protected string dialogueID = "DEFAULT";

        // Trigger some kid of dialogue?
        [SerializeField] protected MeshRenderer _meshRenderer;

        [SerializeField] protected AudioClip pickupSound;

        private void TriggerDialogue()
        {
            // Tell the subtitle manager to go.
            SubtitleManager.Instance.StartDialogueSequence(dialogueID);
            try
            {
                AudioManager.Instance.PlaySound(pickupSound);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message + $" \nwhen picking up {gameObject.name}");
            }
            
            OnInteraction?.Invoke();
            
            // Disable object?
            this.gameObject.SetActive(false);
        }

        private void StartPreview()
        {
            // Enable material instance
            //_meshRenderer.material.SetInt("_EnableFresnel", 1);
            var a = new Material(_meshRenderer.materials[0]);
            a.SetInt("_EnableFresnel", 1);
            _meshRenderer.material = a;
            //updateVisual = StartCoroutine(StartTracking());
            //var cameraPos = Camera.main.ScreenToWorldPoint(screenPoint.position);
            //cameraPos.z = transform.position.z;
            //Debug.DrawLine(transform.position,cameraPos , Color.white, 10f);
        }

        private void EndPreview()
        {
            var a = new Material(_meshRenderer.materials[0]);
            a.SetInt("_EnableFresnel", 0);
            _meshRenderer.material = a;
            //_meshRenderer.material.SetInt("_EnableFresnel", 0);
        }

        // IEnumerator StartTracking()
        // {
        //     while (true)
        //     {
        //         var objectPoint = transform.position;
        //         objectPoint = _camera.WorldToScreenPoint(objectPoint);
        //         objectPoint = transform.position - objectPoint;
        //         var objectLength = objectPoint.magnitude;
        //         
        //         var sP = screenPoint.transform.position;
        //         Ray ray = _camera.ScreenPointToRay(sP);
        //         Vector3 finalPoint = ray.direction * objectLength;
        //         
        //         lineRenderer.SetPosition(0, transform.position);
        //         lineRenderer.SetPosition(1, finalPoint);
        //         yield return null;
        //     }
        // }

        protected virtual void OnEnable()
        {
            OnStartInteraction += TriggerDialogue;
            OnStartPreview += StartPreview;
            OnEndPreview += EndPreview;
        }

        protected virtual void OnDisable()
        {
            OnStartInteraction -= TriggerDialogue;
            OnStartPreview -= StartPreview;
            OnEndPreview -= EndPreview;
        }
    }
}