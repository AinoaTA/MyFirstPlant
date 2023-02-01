using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;

    public List<PlantaScriptableObject> plantProfiles = new();
    public PlantaScriptableObject playerProfile;
    public PlantaScriptableObject profilePlantSelected;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
        }
    } 

    /// <summary>
    /// To set the choices
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public PlantaScriptableObject GetPlantProfile(int i)
    {  
        return plantProfiles[i];
    }

    public void SaveMatches(List<PlantaScriptableObject> plants) 
    {
        plantProfiles = plants;
    }
}