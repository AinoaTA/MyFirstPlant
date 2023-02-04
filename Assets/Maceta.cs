using UnityEngine;


public class Maceta : Personaje
{
    [SerializeField] SkinnedMeshRenderer _baseMaceta;
    [SerializeField] GameObject[] decorations;
    [SerializeField] bool _updateMaceta = true;
    private void Start()
    {
        if (_updateMaceta)
            SetUpMaceta();
    }

    void SetUpMaceta()
    {
        _baseMaceta.material = Main.instance.playerProfile.tiestoMat;

        for (int i = 0; i < decorations.Length; i++)
            decorations[i].SetActive(decorations[i].name == Main.instance.playerProfile.decoSelected);

        ChangeFace(Main.instance.playerProfile.faceName);
    }
}