using UnityEngine;
using TMPro;

namespace Tinder
{
    public class Controller : MonoBehaviour
    {
        public static Controller controller;


        [SerializeField] private PlantaScriptableObject[] _tinderProfiles;



        [HideInInspector] public Camera cam;

        [Header("Settings")]
        [SerializeField] private Photo _photo;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _zodiacSign;
        [SerializeField] private TMP_Text[] _intereses;
        [SerializeField] private TMP_Text[] _desintereses;
        [SerializeField] private TMP_Text _frase;

        [SerializeField] PlantaScriptableObject[] _acceptedProfiles;


        int _index;
        private void Awake()
        {
            controller = this;
            cam = Camera.main;
        }

        private void Start()
        {
            SetNewProfile();
        }

        public void Accept()
        {
            SetNewProfile();
        }
        public void Deny()
        {
            SetNewProfile();
        }

        public void SetNewProfile()
        {
            if (_index >= _tinderProfiles.Length)
            {
                Debug.Log("No hay más perfiles");
                return;
            }

            Debug.Log("New profile Loaded");
            _photo.sp.sprite = _tinderProfiles[_index].imagen;
            _name.text = _tinderProfiles[_index].nombre + " " + _tinderProfiles[_index].edad.ToString();
            _zodiacSign.text = _tinderProfiles[_index].signo.ToString();

            for (int i = 0; i < _intereses.Length; i++)
            {
                _intereses[i].text = _tinderProfiles[_index].intereses.Count <= i ? "" : _tinderProfiles[_index].intereses[i];
                _desintereses[i].text = _tinderProfiles[_index].desintereses.Count <= i ? "" : _tinderProfiles[_index].desintereses[i];
            }
            _frase.text = _tinderProfiles[_index].frase;

            _index++;
            //  if (_index >= _tinderProfiles.Length) _index = 0;
        }
    }
}