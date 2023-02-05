using PixelCrushers.DialogueSystem;
using UnityEditor.Animations;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public SkinnedMeshRenderer faceRenderer;

    public PlantaScriptableObject personaje;

    public Animator animator;
    //
    // public string nuevaCara = "feliz";
    // public bool changeFace = false;

    public void ChangeFace(string faceName)
    {
        var texture = personaje.caritas.Find(x => x.name == faceName).sprite;
        var mat = faceRenderer.materials[0];
        mat.mainTexture = texture;
        faceRenderer.materials[0] = mat;
        Debug.Log($"Changing {gameObject.name} with face {faceName}");
    }

    public void PlayAnimation(string face)
    {
        animator.SetTrigger(face);
    }
    
    // private void OnValidate()
    // {
    //     if (!Application.isPlaying) return;
    //     if (changeFace)
    //     {
    //         ChangeFace(nuevaCara);
    //         changeFace = false;
    //     }
    // }
}
