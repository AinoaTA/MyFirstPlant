using UnityEngine;
using TMPro;
using System.Collections.Generic; 

namespace Tinder
{
    public class Controller : MonoBehaviour
    {
        public static Controller controller;

        [SerializeField] private List<PlantaScriptableObject> _tinderProfiles = new();
        private PlantaScriptableObject _playerProfile;

        [HideInInspector] public Camera cam;

        [Header("Settings")]
        [SerializeField] private Photo _photo;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _zodiacSign;
        [SerializeField] private TMP_Text[] _intereses;
        [SerializeField] private TMP_Text[] _desintereses;
        [SerializeField] private TMP_Text _frase;

        [Header("Preferences")]
        [SerializeField] private int _minMatches = 2;
        [SerializeField] private int _maxMatches = 4;
        List<PlantaScriptableObject> _finalMatches = new();

        List<PlantaScriptableObject> _allAccepted = new();
        List<PlantaScriptableObject> _allRejected = new();
        List<MatchesCompare> _matchesCompare = new();

        [SerializeField] List<MatchesProfiles> _matchesProfile;

        [Header("UI")]
        private int _indexSelector;
        [SerializeField] private Transform _selector;
        [SerializeField] private GameObject _adverMinMatches;
        [SerializeField] private GameObject _showMatches;
        [SerializeField] private TMP_Text _contentText;
        public bool anyMenuOpen;
        int _index;
        bool _endMatch;

        private void OnEnable()
        {
            MatchesProfiles.delegateMatch += Selector;
        }
        private void OnDisable()
        {
            MatchesProfiles.delegateMatch -= Selector;
        }
        private void Awake()
        {
            _index = -1;
            controller = this;
            cam = Camera.main;
        }

        private void Start()
        {
            _playerProfile = Main.instance.playerProfile;
            SetNewProfile();
        }

        public void Accept()
        {
            if (_adverMinMatches.activeSelf || anyMenuOpen) return;

            _matchesCompare.Add(new MatchesCompare(_tinderProfiles[_index]));
            _allAccepted.Add(_tinderProfiles[_index]);

            SetNewProfile();
        }
        public void Deny()
        {
            if (_adverMinMatches.activeSelf || _endMatch) return;

            _allRejected.Add(_tinderProfiles[_index]);
            SetNewProfile();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                CheckMatch();
            }
        }
#endif

        public void SetNewProfile()
        {
            _index++;

            if (_index >= _tinderProfiles.Count)
            {
                Debug.Log("No hay más perfiles");
                _index = -1;
                CheckPreferences();
                return;
            }

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

        private void CheckPreferences()
        {
            if (_allAccepted.Count < _minMatches)
            {
                MinMatchesAdv(true);
            }
            else
            {
                _endMatch = true;
                CheckMatch();
                SelectMatches();
            }
        }

        private void CheckMatch()
        {
            for (int i = 0; i < _matchesCompare.Count; i++)
                _matchesCompare[i].CheckInterests(_playerProfile);

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

            Main.instance.SaveMatches(_finalMatches);
        }

        public void MinMatchesAdv(bool show)
        {
            anyMenuOpen = show;
            _adverMinMatches.SetActive(show);

            if (!show)
            {
                for (int i = 0; i < _allAccepted.Count; i++)
                    if (_tinderProfiles.Contains(_allAccepted[i]))
                        _tinderProfiles.Remove(_allAccepted[i]);

                SetNewProfile();
            }
            else
                _contentText.text = "Oh no! Te faltan " + (_minMatches - _allAccepted.Count) + " matches para tener plancitas :(";
        }
        public void SelectMatches()
        {
            anyMenuOpen = true;
            _showMatches.SetActive(true);

            for (int i = 0; i < _matchesProfile.Count; i++) 
                _matchesProfile[i].SetUp(_finalMatches[i]); 
        }

        public void Selector(Transform tf, int i)
        {
            _selector.position = tf.position;
            _indexSelector = i;
        }

        public void StartPlancita()
        {
            Main.instance.profilePlantSelected = _finalMatches[_indexSelector];
            _finalMatches.RemoveAt(_indexSelector);
            Debug.Log("Cambiazo de escena bro");
            Main.instance.managerScene.LoadSceneWithLoading("Rematch");
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