using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DialogueStarterTest : MonoBehaviour
{
    public Transform npc;
    public Transform player;

    public PlantaScriptableObject personaje;

    public void Start()
    {
        DialogueLua.SetVariable("plantSign", personaje.signo);
        DialogueLua.SetVariable("playerSign", "Sagitario");
        DialogueManager.StartConversation(personaje.pitonisaConversation);
    }
}