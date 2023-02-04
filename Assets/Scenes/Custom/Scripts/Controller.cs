using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace Custom
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] TextAsset _interesesDesinteres; 
        public List<string> allInteresesDesintereses; 

        int[] _zodiacNumbers = new int[12];

        [SerializeField] PlantaScriptableObject _playerProfile;

        [Header("UI Personality")]
        [SerializeField] private GameObject _canvasPersonality;
        [SerializeField] private GameObject _canvasFisico;
        [SerializeField] TMP_Dropdown _zodiacSign;

        [SerializeField] TMP_Dropdown _intereses1;
        [SerializeField] TMP_Dropdown _intereses2;
        [SerializeField] TMP_Dropdown _intereses3;

        [SerializeField] TMP_Dropdown _desintereses1;
        [SerializeField] TMP_Dropdown _desintereses2;
        [SerializeField] TMP_Dropdown _desintereses3;


        [SerializeField] TMP_InputField _inputFieldName;
        [SerializeField] TMP_InputField _inputFieldFrase;
        [SerializeField] Slider _age;
        [SerializeField] TMP_Text _ageText;

        [Header("UI Físico")]
        [SerializeField] private SpriteRenderer _face;
        [SerializeField] private Sprite[] _faceMat;
        private int _indexFace;

        [SerializeField] private MeshRenderer _base;
        [SerializeField] private Material[] _baseMat;
        private int _indexBase;

        [SerializeField] private GameObject[] _decorations;
        bool _block;
        private void Awake()
        {
            allInteresesDesintereses = _interesesDesinteres.text.Split('\n').ToList(); 
        } 

        public void StartPersonality() 
        {
            _canvasFisico.SetActive(false);
            _canvasPersonality.SetActive(true);
            if (!_block)
            {
                _block = true;
                SetUpDropdowns();
                SetSlider();
            }
        }

        public void StartFisico() 
        {
            _canvasFisico.SetActive(true);
            _canvasPersonality.SetActive(false); 
        }

        #region custom

        public void LeftMaterial() 
        {
            _indexBase++;
            if (_indexBase >= _baseMat.Length) _indexBase = 0; 
            _base.material = _baseMat[_indexBase]; 
        }

        public void RightMaterial()
        {
            _indexBase--;
            if (_indexBase <0 ) _indexBase = _baseMat.Length-1;
            _base.material = _baseMat[_indexBase];
        } 

        public void LeftSprite()
        {
            _indexFace++;
            if (_indexFace >= _faceMat.Length) _indexFace = 0;
            _face.sprite = _faceMat[_indexFace];
        }

        public void RightSprite()
        {
            _indexFace--;
            if (_indexFace < 0) _indexFace = _baseMat.Length - 1;
            _face.sprite = _faceMat[_indexFace];
        }

        #endregion

        #region personality
        private void SetUpDropdowns()
        {
            _intereses1.ClearOptions();
            _intereses2.ClearOptions();
            _intereses3.ClearOptions();
            _desintereses1.ClearOptions();
            _desintereses2.ClearOptions();
            _desintereses3.ClearOptions();

            _intereses1.AddOptions(allInteresesDesintereses);
            _intereses2.AddOptions(allInteresesDesintereses);
            _intereses3.AddOptions(allInteresesDesintereses);
            _desintereses1.AddOptions(allInteresesDesintereses);
            _desintereses2.AddOptions(allInteresesDesintereses);
            _desintereses3.AddOptions(allInteresesDesintereses);

            List<string> zodiacNames = new();
            for (int i = 0; i < _zodiacNumbers.Length; i++)
            {
                zodiacNames.Add(((ZodiacSign)i).ToString());
            }

            _zodiacSign.ClearOptions();
            _zodiacSign.AddOptions(zodiacNames);
        }

        public void SetSlider()
        {
            _ageText.text = _age.value.ToString();
        }

        public void SaveValues()
        {
            _playerProfile.intereses.Clear();
            _playerProfile.desintereses.Clear();

            _playerProfile.nombre = _inputFieldName.text;
            _playerProfile.frase = _inputFieldFrase.text;

            _playerProfile.edad = (int)_age.value;
            _playerProfile.signo = (ZodiacSign)_zodiacSign.value;

            _playerProfile.intereses.Add(allInteresesDesintereses[_intereses1.value]);
            _playerProfile.intereses.Add(allInteresesDesintereses[_intereses2.value]);
            _playerProfile.intereses.Add(allInteresesDesintereses[_intereses3.value]);

            _playerProfile.desintereses.Add(allInteresesDesintereses[_desintereses1.value]);
            _playerProfile.desintereses.Add(allInteresesDesintereses[_desintereses2.value]);
            _playerProfile.desintereses.Add(allInteresesDesintereses[_desintereses3.value]);

            Main.instance.playerProfile = _playerProfile;
            Main.instance.managerScene.LoadSceneWithLoading("tinder");
        }
        #endregion
    }
}