using System.Collections;
using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;

namespace Gameplay
{
    public class Controller : MonoBehaviour
    {
        public static Controller controller;

        [SerializeField] private float _maxPoints;
        [SerializeField] private float _currentPoints;
        [SerializeField] private Transform _playerPos, _plantPos;

        public Tarot tarot;
        public CameraManager cameraManager;
        public Rematch rematch;
        [HideInInspector] public Cutegame.Minigames.PuzleDeTiempo _puzle;
        public GameObject player, plant;

        [Header("UI")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _endCanvas;
        [SerializeField] private TMP_Text _content;

        private void OnEnable()
        {
            Lua.RegisterFunction("MinigameTarot", this, SymbolExtensions.GetMethodInfo(() => MinigameTarot()));
        }
        private void OnDisable()
        {
            Lua.UnregisterFunction("MinigameTarot");
        }
        private void Awake()
        {
            controller = this;
        }

        private void Start()
        {
            _puzle = Main.instance.profilePlantSelected.puzlePrefab;

            Instantiate(_puzle, _canvas.transform.position, Quaternion.identity, _canvas.transform);

            player = Instantiate(Main.instance.playerProfile.modeloPrefab, transform.position, Quaternion.identity);
            player.transform.position = _playerPos.position;
            player.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));

            plant = Instantiate(Main.instance.profilePlantSelected.modeloPrefab, transform.position, Quaternion.identity);
            plant.transform.position = _plantPos.transform.position;
            plant.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

            StartCoroutine(GameFlow());
            //UpdateEnd();
            //MinigameTarot();
            //End();
        }

        IEnumerator GameFlow()
        {
            yield return new WaitForSeconds(1);
            
            //Conejo dice lo suyo

            //La cita habla (se presenta)

            //El personaje elige respuesta

            //minipuzzle

            //habla cita en funcion del puzle

            //conejo presenta pitonisa

            //pitonisa in

            //pitonisa habla

            //juegan
            yield return null;
            cameraManager.ChooseCam(0);
            // MinigamePuzle();
            //camara se centra en cita etc etc

        }

        public void MinigamePuzle()
        {
            StartCoroutine(MinipuzleRoutine());
        }

        IEnumerator MinipuzleRoutine()
        {
            _puzle.StartMinigame();
            yield return null;

        }
        public void MinigameTarot()
        {
            StartCoroutine(TarotRoutine());
        }

        IEnumerator TarotRoutine()
        {
            cameraManager.ChooseCam("Pitonisa", true);
            yield return new WaitForSeconds(1);
            tarot.StartTarot();
        }

        public void End()
        {
            rematch.StartReMatch();
        }

        public void ModifyPoints(float points)
        {
            _currentPoints += points;
        }


        #region EndGame
        private void UpdateEnd()
        {
            _endCanvas.SetActive(true);
            _content.text = "Has hecho un total de: " + _currentPoints + " puntos. Te gustaría echar raices con esa planta?";

        }

        public void Yes() 
        {
            _endCanvas.SetActive(false);
            //End game
        }


        public void No() 
        {
            if (Main.instance.plantProfiles.Count <= 0)
            {
                _content.text = "Estarás forever plantón :(...";
            }
            else
            {
                _endCanvas.SetActive(false);
                rematch.StartReMatch();
            }
        }
        #endregion
    }
}