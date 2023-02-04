using System;
using UnityEngine;
using PixelCrushers.DialogueSystem;
namespace Cutegame
{
    public class Minigame : MonoBehaviour
    {
        protected int pointsToAward = 0;
        private Action OnStartMinigame;
        private Action<int> OnEndMinigame;

        public void Setup(Action startAction, Action<int> endAction)
        {
            OnStartMinigame += startAction;
            OnEndMinigame += endAction;
        }

        public virtual void StartMinigame()
        {
            // Start action could be disable some processes or UI.
            OnStartMinigame?.Invoke();
        }

        protected virtual void SetupPoints()
        {
            // Here every minigame will setup the points, or do the required checkings.
        }

        public virtual void WinGame()
        {
            // Do win game.
        }

        protected void FinishMinigame()
        {
            // Decide if it's a win or a loss.
            SetupPoints();
            // Invoke points.
            OnEndMinigame?.Invoke(pointsToAward);
 
            gameObject.SetActive(false);
        }
    }
}