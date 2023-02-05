using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Rematch : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private MatchesProfiles _matches;
        [SerializeField] private Transform _selector;
        [SerializeField] private CanvasGroup _canvasGroup;
        private int _indexSelector;

        #region enables
        private void OnEnable()
        {
            MatchesProfiles.delegateMatch += Selector;
        }
        private void OnDisable()
        {
            MatchesProfiles.delegateMatch -= Selector;
        }
        #endregion

        private void Start()
        {
            Canvas(false);
        } 
         
        public void StartReMatch()
        {
            StartCoroutine(ReMatchRoutine());
        }

        IEnumerator ReMatchRoutine()
        {
            print("??");
            Canvas(true);
            for (int i = 0; i < Main.instance.plantProfiles.Count; i++)
            {
                MatchesProfiles gm = Instantiate(_matches, transform.position, Quaternion.identity, _parent);
                gm.id = _parent.childCount - 1;
                gm.SetUp(Main.instance.plantProfiles[i]);
            }
            yield return null;
            Selector(_parent.GetChild(0).transform, 0);
        }

        public void Selector(Transform tf, int i)
        {
            _selector.position = tf.position;
            _indexSelector = i;
        }

        public void StartPlancita()
        {
            Main.instance.profilePlantSelected = Main.instance.plantProfiles[_indexSelector];
            Main.instance.plantProfiles.RemoveAt(_indexSelector);
            Debug.Log("Escena encontronazo!!!");
            Main.instance.managerScene.LoadSceneWithLoading("Game");
        }
         
        void Canvas(bool v)
        {
            _canvasGroup.alpha = v ? 1 : 0;
            _canvasGroup.blocksRaycasts = v;
            _canvasGroup.interactable = v;
        }
    }
}