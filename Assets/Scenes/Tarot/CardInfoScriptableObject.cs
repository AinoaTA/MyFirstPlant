using UnityEngine;

[CreateAssetMenu(fileName = "Default_" + nameof(PlantaScriptableObject), menuName = "Data/Ficha de Carta Tarot")]
public class CardInfoScriptableObject : ScriptableObject
{
    public string cardName;
    [TextArea]
    public string cardInfo;
    public Material materialCard;
}