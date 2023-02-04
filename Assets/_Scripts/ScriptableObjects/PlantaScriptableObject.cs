using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default_"+ nameof(PlantaScriptableObject), menuName = "Data/Ficha de Personaje")]
public class PlantaScriptableObject : ScriptableObject
{
    public Sprite imagen;
    public string nombre;
    public string tipoPlanta;
    [Range(0, 50)]
    public int edad;
    public ZodiacSign signo;
    public List<string> intereses;
    public List<string> desintereses;
    [TextArea(5, 10)]
    public string frase = "Aqui va la frase estelar";
    
    [Header("Modelo")]
    public GameObject modeloPrefab;
    public Cutegame.Minigames.PuzleDeTiempo puzlePrefab;

    [Header("Custom")]
    public Material tiestoMat;
    public string decoSelected;
    public string faceName;

    public List<Carita> caritas;
    
    [Header("Dialogo")] 
    [ConversationPopup(true)] public string starterConversation;
    [ConversationPopup(true)] public string dinnerConversation;
    [ConversationPopup(true)] public string pitonisaConversation;
    [ConversationPopup(true)] public string cartasDelTarot;
    [ConversationPopup(true)] public string conclusión;

    public string comentarioPitonisa = "";
}

[Serializable]
public struct Carita
{
    public string name;
    public Texture2D sprite;
}

public enum ZodiacSign
{
    Capricornio = 0,
    Acuario, 
    Piscis, 
    Aries,
    Tauro,
    Géminis,
    Cáncer,
    Leo, 
    Virgo,
    Libra,
    Escorpio,
    Sagitario
}
