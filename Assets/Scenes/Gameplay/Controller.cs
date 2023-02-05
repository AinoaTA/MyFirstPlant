using System.Collections;
using System.Collections.Generic;
using Cutegame.Audio;
using Cutegame.Subtitles;
using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class Controller : MonoBehaviour
    {
        public static Controller controller;

        [SerializeField] private float _maxPoints;
        [SerializeField] private float _currentPoints;
        [SerializeField] private Transform _playerPos;
        [SerializeField] private Transform[] _plantPoses;

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

        public AudioClip finalMusic;

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
            Lua.RegisterFunction("FinishGame", this, SymbolExtensions.GetMethodInfo(() => FinishGame()));
            
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
            Lua.UnregisterFunction("Final");
            Lua.UnregisterFunction("FinishGame");
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

            int i=0;
            switch (Main.instance.profilePlantSelected.nombre)
            {
                case "Cayetano Froilán":
                    i = 0;
                    break;
                case "Linda Medusa":
                    i = 1;
                    break;
                case "Lady Blue":
                    i = 2;
                    break;
                case "Erica Draven":
                    i = 3;
                    break;
            };

            // TODO DISABLED THIS BECAUSE OF ERROR
            plant.transform.SetPositionAndRotation(_plantPoses[i].position, Quaternion.Euler(new Vector3(0, 90, 0)));

            /*este es la buena !!!*/ StartCoroutine(GameFlow());

            //tarot.EndCard();
            //End();
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
            cameraManager.ChooseCam(1, true);
            yield return new WaitForSeconds(1f);
            DialogueManager.StartConversation(Main.instance.profilePlantSelected.dialoguitos.conclusionCita, player.transform);
        }
        IEnumerator Delay4()
        {
            // FADE TO BLACK. STAY LIKE THAT. THEN SHOW THE END.
            cameraManager.FinalFade();
            // Play the music
            AudioManager.Instance.PlayMusic(finalMusic);
            
            yield return new WaitForSeconds(3f);
            DialogueManager.StartConversation(Main.instance.profilePlantSelected.dialoguitos.final, _conejo.transform,player.transform);
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

        public void FinishGame()
        {
            // Se termina el juego aquí.
            Debug.Log("FINISHED");
            SceneManager.LoadScene(0);
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

        IEnumerator prueba()
        {
            yield return new WaitForSeconds(2);
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