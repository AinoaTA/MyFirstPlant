using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Cutegame.UI
{
    public class SubtitlePanel : MonoBehaviour
    {
        private static SubtitlePanel _privateInstance;

        public static SubtitlePanel Instance
        {
            get => _privateInstance;
            set
            {
                if (_privateInstance != null)
                {
                    Destroy(value.gameObject);
                    return;
                }

                _privateInstance = value;
            }
        }

        [SerializeField] private TMPro.TMP_Text _messageText;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetText(string text)
        {
            _messageText.text = text;
        }

        public void Show()
        {
            // Show it with animation? Canvas Fade In?
            // Maybe set the alpha to 0 here?
            _canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            // Hide it with animation? Canvas Fade Out?
            _canvasGroup.alpha = 0f;
        }
    }
}