using System.Collections;
using System.Collections.Generic;
using Cutegame.Subtitles;
using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;

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
        [SerializeField] GameObject _conejo;

        [Header("UI")] [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _endCanvas;
        [SerializeField] private TMP_Text _content;

        private List<string> chisteString = new List<string>()
        {
            "¿Qué es lo peor que te puede ocurrir cuando vienes aquí? ¡Que te den PLANTÓN!",
            "Noto al público algo seco esta noche. Pediré que os rieguen debidamente.",
            "¡Guau! ¿Eso que noto es un brote, o es que te alegras de verme?",
            "Y la planta le dijo a la maceta, ¿en tu invernadero o en el mío?"
        };

        private void OnEnable()
        {
            //Lua.RegisterFunction("MinigameTarot", this, SymbolExtensions.GetMethodInfo(() => MinigameTarot()));
            Lua.RegisterFunction("PlantaPresenta", this, SymbolExtensions.GetMethodInfo(() => PlantaPresenta()));
            Lua.RegisterFunction("MinigamePuzle", this, SymbolExtensions.GetMethodInfo(() => MinigamePuzle()));
            Lua.RegisterFunction("ConejoPresentaTarot", this, SymbolExtensions.GetMethodInfo(() => ConejoPresentaTarot()));
            Lua.RegisterFunction("ConejoBye", this, SymbolExtensions.GetMethodInfo(() => ConejoBye()));
            Lua.RegisterFunction("CambiarScena", this, SymbolExtensions.GetMethodInfo(() => CambiarScena()));
            Lua.RegisterFunction("Conclusiones", this, SymbolExtensions.GetMethodInfo(() => Conclusiones()));
            Lua.RegisterFunction("Final", this, SymbolExtensions.GetMethodInfo(() => Final()));
        }

        private void OnDisable()
        {
            //Lua.UnregisterFunction("MinigameTarot");
            Lua.UnregisterFunction("PlantaPresenta");
            Lua.UnregisterFunction("MinigamePuzle");
            Lua.UnregisterFunction("ConejoPresentaTarot");
            Lua.UnregisterFunction("ConejoBye");
            Lua.UnregisterFunction("CambiarScena");
            Lua.UnregisterFunction("Conclusiones");
        }

        private void Awake()
        {
            controller = this;
        }

        private void Start()
        {
            DialogueLua.SetVariable("chisteMalo", chisteString[Random.Range(0, chisteString.Count)]);
            _puzle = Main.instance.profilePlantSelected.puzlePrefab;

            _puzle = Instantiate(_puzle, _canvas.transform.position, Quaternion.identity, _canvas.transform);

            player = Instantiate(Main.instance.playerProfile.modeloPrefab, transform.position, Quaternion.identity);
            player.transform.position = _playerPos.position;
            player.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));

            plant = Instantiate(Main.instance.profilePlantSelected.modeloPrefab, transform.position,
                Quaternion.identity);
            plant.transform.position = _plantPos.transform.position;
            plant.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

            //StartCoroutine(GameFlow());
            //UpdateEnd();
            //MinigameTarot();
            //End();
            //MinigameTarot();
            tarot.EndCard();
        }

        public void ConejoBye() 
        {
            StartCoroutine(Delay2());
        }
        public void Conclusiones()
        {
            StartCoroutine(Delay3());
        }
        public void Final()
        {
            StartCoroutine(Delay4());
        }
        IEnumerator Delay3()
        {
            yield return new WaitForSeconds(1f);
            cameraManager.ChooseCam(1, true);
            DialogueManager.StartConversation(Main.instance.profilePlantSelected.dialoguitos.conclusionCita, player.transform);
        }
        IEnumerator Delay4()
        {
            yield return new WaitForSeconds(1f);
            cameraManager.ChooseCam(1, true);
            DialogueManager.StartConversation(Main.instance.profilePlantSelected.dialoguitos.final, player.transform);
        }
        IEnumerator Delay2() 
        {
            yield return new WaitForSeconds(1.25f);
            DialogueManager.StartConversation(Main.instance.profilePlantSelected.dialoguitos.laCuenta, player.transform);
        }

        public void CambiarScena() 
        {
            cameraManager.ChooseCam(2, true);
        }
        IEnumerator GameFlow()
        {
            cameraManager.ChooseCam(1);
            yield return new WaitForSeconds(1);
            //Conejo dice lo suyo
            DialogueManager.StartConversation("SaludoConejo", _conejo.transform, player.transform);

            //El personaje elige respuesta

            //minipuzzle

            //habla cita en funcion del puzle

            //conejo presenta pitonisa

            //pitonisa in

            //pitonisa habla

            //juegan 

            // MinigamePuzle();
            //camara se centra en cita etc etc 
        }


        public void PlantaPresenta()
        {
            cameraManager.ChooseCam(2,true);
            StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {
            //La cita habla (se presenta)
            yield return new WaitForSeconds(1);
            DialogueManager.StartConversation(Main.instance.profilePlantSelected.dialoguitos.intro, player.transform, plant.transform); 
        }

        public void MinigamePuzle()
        {
            StartCoroutine(MinipuzleRoutine());
        }

        IEnumerator MinipuzleRoutine()
        {
            _puzle.Setup(
                () => { Debug.Log($"Puzzle {gameObject.name} started"); },
                AddPoints
                );
            yield return null;
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
            tarot.StartVoice();
        }

        public void End()
        {
            rematch.StartReMatch();
        }

        public void ModifyPoints(float points)
        {
            _currentPoints += points;
        }


        public void ConejoPresentaTarot() 
        { 
            StartCoroutine(DelayConejo()); 
        }

        IEnumerator DelayConejo() 
        {//primero camera o conejo?
            cameraManager.ChooseCam(1, true);
            yield return new WaitForSeconds(0.5f);
            DialogueManager.StartConversation("AnuncioPitonisa", _conejo.transform, player.transform);
           
        }
        #region Dataloading

        public void AddPoints(int points)
        {
            var a = DialogueLua.GetVariable("Puntos").AsInt;
            
            if(points > 0)
                DialogueLua.SetVariable("Puzzle", true);
            
            a += points;
            DialogueLua.SetVariable("Puntos", a);
            Debug.Log($"Tienes {a} pontos");

            DialogueManager.StartConversation(Main.instance.profilePlantSelected.dialoguitos.puzzle,
                plant.transform, player.transform);
        }

        public void ChangeFace(string face)
        {
            
        }
      
        #endregion


        #region EndGame

        private void UpdateEnd()
        {
            _endCanvas.SetActive(true);
            _content.text = "Has hecho un total de: " + _currentPoints +
                            " puntos. Te gustaría echar raices con esa planta?";
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