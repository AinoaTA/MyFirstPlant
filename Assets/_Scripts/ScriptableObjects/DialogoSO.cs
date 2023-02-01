using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Default_Dialogo", menuName = "Data/Personajes/Dialogo")]
public class DialogoSO : ScriptableObject
{
    [Header("Pertenencia")] public PlantaScriptableObject personajePropietario;
    [Header("Config")] [TextArea(10, 60)] public string dialogue;
    [Space(5)]
    public List<string> dialogueTags;
}
