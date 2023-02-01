using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tinder
{
    public class MatchesProfiles : MonoBehaviour
    {
        [SerializeField] public Image _photo;
        [SerializeField] public TMP_Text _name;
        [SerializeField] public TMP_Text _zodiacSign;
        [SerializeField] public TMP_Text[] _intereses;
        [SerializeField] public TMP_Text[] _desintereses;
        [SerializeField] public TMP_Text _frase;

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
    }
}