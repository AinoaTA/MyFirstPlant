using System.Collections;
using UnityEngine;

namespace Cutegame
{
    public class TimedPuzzle : Minigame
    {
        public float maxTime = 45f;
        private float currentTime = 0f;
        private bool isPlaying = false;
        private bool win = false;

        private Coroutine GameCoroutine;

        public override void StartMinigame()
        {
            if (isPlaying) return;
            isPlaying = true;
            
            base.StartMinigame();
            
            GameCoroutine = StartCoroutine(GameTimer());
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
        }

        private void OnDisable()
        {
            StopCoroutine(GameCoroutine);
        }

        private void OnDestroy()
        {
            StopCoroutine(GameCoroutine);
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