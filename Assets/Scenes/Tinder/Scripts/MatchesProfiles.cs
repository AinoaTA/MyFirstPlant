using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class MatchesProfiles : MonoBehaviour
{
    [HideInInspector] public int id;
    public delegate void Match(Transform f, int i);
    public static Match delegateMatch;

    public Image _photo;
    public TMP_Text _name;
    public TMP_Text _zodiacSign;
    public TMP_Text[] _intereses;
    public TMP_Text[] _desintereses;
    public TMP_Text _frase;

    public void SetUp(PlantaScriptableObject plant)
    {
        _photo.sprite = plant.imagen;
        _name.text = plant.nombre + " " + plant.edad.ToString();
        _zodiacSign.text = plant.signo.ToString();

        for (int i = 0; i < _intereses.Length; i++)
        {
            _intereses[i].text = plant.intereses.Count <= i ? "" : plant.intereses[i];
            _desintereses[i].text = plant.desintereses.Count <= i ? "" : plant.desintereses[i];
        }

        _frase.text = plant.frase;
    }

    public void SelectProfile()
    {
        delegateMatch?.Invoke(transform, id);
    }
}
