using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public SkinnedMeshRenderer faceRenderer;

    public PlantaScriptableObject personaje;
    //
    // public string nuevaCara = "feliz";
    // public bool changeFace = false;

    public void ChangeFace(string faceName)
    {
        var texture = personaje.caritas.Find(x => x.name == faceName).sprite;
        var mat = faceRenderer.materials[0];
        mat.mainTexture = texture;
        faceRenderer.materials[0] = mat;
    }

    private void OnEnable()
    {
        Lua.RegisterFunction("ChangeFace", this, SymbolExtensions.GetMethodInfo(() => ChangeFace(string.Empty)));
    }
    //
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
