using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace Gameplay
{
    public class Tarot : MonoBehaviour
    { 
        private int _indexCardSelected;
        private int _indexCardiInfo;

        [SerializeField] Transform _cardGroup;
        [SerializeField] Transform _nearPos;
        [SerializeField] float _time;
        [SerializeField] float _yPos = 0.2f;
        [SerializeField] float _zPos = 0.051f;
        [SerializeField] List<CardInfoScriptableObject> _cardInfo;
        [SerializeField] List<Card> _cards = new();
        Vector3 endRot = new(0, 0, -90);

        [SerializeField] int _maxReroll = 3;
        Vector3[] _initPoses;


        private void OnEnable()
        {
            Lua.RegisterFunction("StartVoice", this, SymbolExtensions.GetMethodInfo(() => StartVoice()));
            Lua.RegisterFunction("StartTarot", this, SymbolExtensions.GetMethodInfo(() => StartTarot()));
        }
        private void OnDisable()
        {
            Lua.UnregisterFunction("StartVoice");
            Lua.UnregisterFunction("StartTarot");
        }
        private void Start()
        { 
            _initPoses = new Vector3[_cards.Count];

            for (int i = 0; i < _cards.Count; i++)
            {
                _initPoses[i] = _cards[i].transform.localPosition;
            }
        }
         
        public void StartVoice()
        {
            StartCoroutine(Delay());
        }

        IEnumerator Delay() 
        {
            yield return new WaitForSeconds(1);
            Controller.controller.cameraManager.ChooseCam("Pitonisa", true);
            DialogueLua.SetVariable("plantSign", Main.instance.profilePlantSelected.signo);
            DialogueLua.SetVariable("playerSign", Main.instance.playerProfile.signo);
            DialogueLua.SetVariable("comentarioPitonisa", Main.instance.profilePlantSelected.comentarioPitonisa);
            DialogueManager.StartConversation("Horoscopo", Controller.controller.player.transform, Controller.controller.plant.transform);
        }

        public void StartTarot()
        { 
            StartCoroutine(StartTarotRoutine());
        }
        IEnumerator StartTarotRoutine()
        { 
            yield return new WaitForSeconds(1);
            for (int i = 0; i < _cards.Count; i++)
            {
                Vector3 end = _cards[i].transform.localPosition;
                end.y += _yPos;

                StartCoroutine(MoveAndRot(_cards[i].transform, Vector3.zero, end));
                yield return new WaitForSeconds(0.4f);
            }

            for (int i = 0; i < _cards.Count; i++)
                _cards[i].Enabled(true);
        }


        public void CardChose(int val)
        {
            _indexCardSelected = val;

            StartCoroutine(ActionCard());
        }

        IEnumerator ActionCard()
        {
            _indexCardiInfo = Random.Range(0, _cardInfo.Count);
            print("La carta es " + _cardInfo[_indexCardiInfo].cardName);
            yield return CardChoseRoutine();
            yield return MovementCard(_cards[_indexCardSelected].transform, _nearPos.localPosition);
            yield return new WaitForSeconds(1);
            yield return MoveAndRot(_cards[_indexCardSelected].transform, endRot, _initPoses[_indexCardSelected]);

            //IA Moment
            _indexCardiInfo = Random.Range(0, _cardInfo.Count);
            print("La carta es " + _cardInfo[_indexCardiInfo].cardName);

            yield return Reroll();
            _indexCardSelected = Random.Range(0, _cards.Count);
            yield return CardChoseRoutine();
            yield return MovementCard(_cards[_indexCardSelected].transform, _nearPos.localPosition);
            yield return new WaitForSeconds(1);
            yield return MoveAndRot(_cards[_indexCardSelected].transform, endRot, _initPoses[_indexCardSelected]);
            yield return new WaitForSeconds(1);
            Controller.controller.cameraManager.ChooseCam(0,true);
        }

        IEnumerator Reroll()
        {
            for (int i = 0; i < _maxReroll; i++)
            {
                for (int e = 0; e < _cards.Count; e++)
                {
                    StartCoroutine(MovementCard(_cards[e].transform, _initPoses[1], 0.25f));
                }

                yield return new WaitForSeconds(_time);

                for (int e = 0; e < _cards.Count; e++)
                {
                    StartCoroutine(MovementCard(_cards[e].transform, _initPoses[e], 0.25f));
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        IEnumerator CardChoseRoutine()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Enabled(false);

                if (i != _indexCardSelected)
                {
                    StartCoroutine(MoveAndRot(_cards[i].transform, endRot, _initPoses[i]));
                }
            }
            yield return new WaitForSeconds(1);
            yield return ShowCard(_cards[_indexCardSelected].transform);
        }
        #region IEnumerator 
        IEnumerator MoveAndRot(Transform ob, Vector3 endRot, Vector3 endPos)
        {
            float t = 0;
            Vector3 actual = ob.localPosition;
            Quaternion rot = ob.localRotation;

            while (t < _time)
            {
                t += Time.deltaTime;
                ob.SetLocalPositionAndRotation(Vector3.Lerp(actual, endPos, t / _time), Quaternion.Lerp(rot, Quaternion.Euler(endRot), t / _time));
                yield return null;
            }
        }

        IEnumerator MovementCard(Transform ob, Vector3 end, float time = 1)
        {
            float t = 0;
            Vector3 act = ob.localPosition;
            while (t < time)
            {
                t += Time.deltaTime;
                ob.localPosition = Vector3.Lerp(act, end, t / time);
                yield return null;
            }
        }

        IEnumerator ShowCard(Transform ob)
        {
            float t = 0;
            Quaternion _rot = ob.rotation;
            while (t < 1)
            {
                t += Time.deltaTime;
                ob.rotation = Quaternion.Lerp(_rot, Quaternion.Euler(new Vector3(0, 180, 0)), t / 1);
                yield return null;
            }
        }
        #endregion
    }
}