using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGrabber : MonoBehaviour
{
    public Camera cam;

    public PuzzlePiece grabbedPiece;

    public LayerMask layerMask;
    private void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //     bool actualHit = Physics.Raycast(ray, out RaycastHit hit, 100, layerMask);
        //     
        //     //Debug.Log("Got clicked");
        //     if (actualHit)
        //     {
        //         Debug.Log("Hit was not null");
        //         var a = hit.collider.gameObject.GetComponent<PuzzlePiece>();
        //         if (a != null)
        //         {
        //             Debug.Log("GrabbedPiece");
        //             a.Grab();
        //             grabbedPiece = a;
        //         }
        //     }
        // }
        // else if (grabbedPiece != null)
        //     grabbedPiece.TickWhileGrabbed(Input.mousePosition);
    }
}
