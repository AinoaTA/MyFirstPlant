using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

namespace Tinder
{
    public class Controller : MonoBehaviour
    {
        public static Controller controller;

        [SerializeField] private PlantaScriptableObject[] _tinderProfiles;
        [SerializeField] private PlantaScriptableObject _playerProfile;
         
        [HideInInspector] public Camera cam;

        [Header("Settings")]
        [SerializeField] private Photo _photo;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _zodiacSign;
        [SerializeField] private TMP_Text[] _intereses;
        [SerializeField] private TMP_Text[] _desintereses;
        [SerializeField] private TMP_Text _frase;

        [Header("Preferences")]
        [SerializeField] private int _maxMatches = 3;
        [SerializeField] List<PlantaScriptableObject> _finalMatches = new();

        [SerializeField] List<MatchesCompare> _matchesCompare = new();

        int _index;
        private void Awake()
        {
            _index = -1;
            controller = this;
            cam = Camera.main;
        }

        private void Start()
        {
            SetNewProfile();
        }

        public void Accept()
        {
            _matchesCompare.Add(new MatchesCompare(_tinderProfiles[_index]));
            SetNewProfile();
        }
        public void Deny()
        {
            SetNewProfile();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(CheckMatch());
            }
        }
#endif

        public void SetNewProfile()
        {
            _index++;
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
        }

        private IEnumerator CheckMatch()
        {
            for (int i = 0; i < _matchesCompare.Count; i++)
            {
                _matchesCompare[i].CheckInterests(_playerProfile);

                yield return null;
            }

            while (_finalMatches.Count < _maxMatches)
            {
                int common = _matchesCompare[0]._thingsInCommon;
                int index = 0;
                PlantaScriptableObject plant = _matchesCompare[0]._profile;
                for (int i = 1; i < _matchesCompare.Count; i++)
                {
                    if (common < _matchesCompare[i]._thingsInCommon)
                    {
                        index = i;
                        common = _matchesCompare[i]._thingsInCommon; 
                        plant = _matchesCompare[i]._profile;
                    }
                }
                _finalMatches.Add(plant);
                _matchesCompare.RemoveAt(index);
            }
        }

        [System.Serializable]
        internal class MatchesCompare
        {
            [SerializeField] internal PlantaScriptableObject _profile;
            [SerializeField] internal int _thingsInCommon;

            internal MatchesCompare(PlantaScriptableObject profile)
            {
                _profile = profile;
            }

            internal void CheckInterests(PlantaScriptableObject player)
            {
                for (int i = 0; i < _profile.intereses.Count; i++)
                    for (int e = 0; e < player.intereses.Count; e++)
                        if (player.intereses[e] == _profile.intereses[i])
                            _thingsInCommon++;

                for (int i = 0; i < _profile.desintereses.Count; i++)
                    for (int e = 0; e < player.desintereses.Count; e++)
                        if (player.desintereses[e] == _profile.desintereses[i])
                            _thingsInCommon++;
            }
        }
    }
}