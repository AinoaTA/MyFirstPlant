using System;
using System.Collections;
using System.Collections.Generic;
using Cutegame.Audio;
using Cutegame.UI;
using Cutegame.Subtitles;
using Cutegame.UI;
using UnityEngine;

namespace Cutegame.Subtitles
{
    public class SubtitleManager : MonoBehaviour
    {
        private static SubtitleManager _instance;

        public static SubtitleManager Instance
        {
            get => _instance;
            private set
            {
                if (_instance != null)
                {
                    Destroy(value.gameObject);
                    return;
                }

                _instance = value;
            }
        }

        // Subtitle file here.It has been loaded AS LONG AS you've went through the INIT scene.
        // Maybe force load could be done if not, just to test. Beware of that
        // TODO WARNING MIGHT NOT WORK CORRECTLY IF NO INIT SCENE WAS LOADED.
        [SerializeField] private DialogueData _dialogueData;

        // load all subtitles for the sequence here. Then when one ends, just pop the other one.
        private static Queue<Dialogue> _sequence = new Queue<Dialogue>();

        private Coroutine showSubtitleCoroutine;

        [SerializeField] private RectTransform root;
        [SerializeField] private SubtitlePanel prefabPanel;
        private SubtitlePanel subtitlePanelInstance;

        // Private and Debug region
        private string debugID = "1";

        private void Awake()
        {
            // This would be part of the hud but nah.
            Instance = this;

            subtitlePanelInstance = Instantiate(prefabPanel, root.transform, false);
            subtitlePanelInstance.Hide();
        }

        public void StartDialogueSequence(string id)
        {
            QueueDialogueSequence(id);
            ShowSequenceWithTimer();
        }

        private void QueueDialogueSequence(string id)
        {
            var values = _dialogueData.GetDialogueSequence(id);
            foreach (var dialogue in values)
            {
                _sequence.Enqueue(dialogue);
            }
        }

        private void ShowSequenceWithTimer()
        {
            // Coroutine?
            if (showSubtitleCoroutine != null) return;

            showSubtitleCoroutine = StartCoroutine(ShowTextSequence());
        }

        #if UNITY_EDITOR || DEBUG
        private void OnGUI()
        {
            debugID = GUILayout.TextField(debugID, 8);
            if (GUILayout.Button("Load ID Sequence"))
            {
                StartDialogueSequence(debugID);
            }
        }
        #endif

        IEnumerator ShowTextSequence()
        {
            // Be showing all enqueued texts until no more are there. When queue is empty, hide the subtitle bar.
            // Show subtitle text.
            if (_sequence.Count > 0)
                subtitlePanelInstance.Show();

            float duration = 2f;

            while (_sequence.Count > 0)
            {
                // get the next dialogue.
                var currentDialogue = _sequence.Dequeue();
                // Set the text. And wait.
                subtitlePanelInstance.SetText(currentDialogue.Text);
                // Wait for default or the duration.
                float.TryParse(currentDialogue.Duration.Replace('.', ','), out duration);
                duration += 0.5f;
                Debug.Log($"Waiting for {duration} seconds.");
                
                // Play the sound,
                var audioClip = currentDialogue.SoundClip; // Get the clip somehow. If clip is null just ignore.
                if (audioClip != null)
                {
                    AudioManager.Instance.PlayVO(audioClip);
                }
                
                // Play the music,
                audioClip = currentDialogue.MusicClip; // Get the clip somehow. If clip is null just ignore.
                if (audioClip != null)
                {
                    AudioManager.Instance.PlayMusic(audioClip);
                }

                // Do the event.
                // This does not allow for skipping btw.
                yield return new WaitForSecondsRealtime(duration);
                // TODO Dispatch event here.
                // currentDialogue.Event;
                try
                {
                    //QuestManager.Instance.ExecuteEvent(currentDialogue.Event);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message + " \nin dialogue events.");
                }
            }

            // Hide subtitle text.
            subtitlePanelInstance.Hide();
            showSubtitleCoroutine = null;
        }
    }
}