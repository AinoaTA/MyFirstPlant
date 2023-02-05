using System.Collections;
using UnityEngine;

namespace Cutegame
{
    public class TimedPuzzle : Minigame
    {
        public float maxTime = 45f;
        private float currentTime = 0f;
        protected bool isPlaying = false;
        private bool win = false;
        [SerializeField] bool debug = false;
        private Coroutine GameCoroutine;

        public override void StartMinigame()
        {
            if (isPlaying) return;
            isPlaying = true;

            GameCoroutine = StartCoroutine(GameTimer());

            base.StartMinigame();
        }

        protected override void SetupPoints()
        {
            // If time is more than 0, then win.
            // If 0 or less, loss.
            if (currentTime > 0f)
                pointsToAward = 1;
            else
                pointsToAward = -1;

        }

        public override void WinGame()
        {
            win = true;

            if (GameCoroutine != null)
                StopCoroutine(GameCoroutine);

            FinishMinigame();
        }
        private void OnValidate() { if (debug) { debug = false; WinGame(); } }


        private void OnDisable()
        {
            if (GameCoroutine != null) StopCoroutine(GameCoroutine);
        }

        private void OnDestroy()
        {
            if (GameCoroutine != null) StopCoroutine(GameCoroutine);
        }

        IEnumerator GameTimer()
        {
            bool playingGame = true;
            currentTime = maxTime;
            win = false;

            while (playingGame)
            {
                currentTime -= Time.deltaTime;
                yield return null;

                if (win)
                    playingGame = false;

                if (currentTime <= 0f)
                    playingGame = false;
            }

            FinishMinigame();
        }
    }
}