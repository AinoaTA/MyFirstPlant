using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default_"+ nameof(PlantaScriptableObject), menuName = "Data/Ficha de Personaje")]
public class PlantaScriptableObject : ScriptableObject
{
    public Sprite imagen;
    public string nombre;
    [Range(0, 50)]
    public int edad;
    public ZodiacSign signo;
    public List<string> intereses;
    public List<string> desintereses;
    [TextArea(5, 10)]
    public string frase = "Aqui va la frase estelar";
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
