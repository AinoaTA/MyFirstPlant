using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Controller : MonoBehaviour
    {
        public static Controller controller;

        [SerializeField] private float _maxPoints;
        [SerializeField] private float _currentPoints;
        [SerializeField] private Transform _playerPos, _plantPos;

        [SerializeField] public Tarot tarot;
        [SerializeField] public CameraManager cameraManager;
        [SerializeField] public Rematch rematch;
        [SerializeField] public Cutegame.Minigames.PuzleDeTiempo _puzle;
        GameObject _player, _plant;

        private void Awake()
        {
            controller = this;
        }

        private void Start()
        {
            _player = Instantiate(Main.instance.playerProfile.modeloPrefab, transform.position, Quaternion.identity);
            _player.transform.position = _playerPos.position;
            _player.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));

            _plant = Instantiate(Main.instance.profilePlantSelected.modeloPrefab, transform.position, Quaternion.identity);
            _plant.transform.position = _plantPos.transform.position;
            _plant.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

            StartCoroutine(GameFlow());
            // MinigameTarot();
            //End();
        }

        IEnumerator GameFlow() 
        { 
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
            MinigamePuzle();
            //camara se centra en cita etc etc

        }

        public void MinigamePuzle() 
        {
            StartCoroutine(MinipuzleRoutine());
        }

        IEnumerator MinipuzleRoutine() 
        {
            _puzle.SpawnPieces();
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
    }
}