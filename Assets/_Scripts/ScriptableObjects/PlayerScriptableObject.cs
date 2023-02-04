using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default_PlayerData", menuName = "Data/Player")]
public class PlayerScriptableObject : PlantaScriptableObject
{
    [Header("Player Settings")] public string variable = "Hello World.";
}