using System;
using System.Collections;
using System.Collections.Generic;
using Cutegame.Subtitles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cutegame
{
    public class Initiator : MonoBehaviour
    {
        [SerializeField] private DialogueData DialogueDataObject;

        public void Awake()
        {
            //DontDestroyOnLoad(this.gameObject);
            // Load default settings.
            
            // Load user settings above the defaults if any exist. If none, create them.
            
            // Then try to load texts? Maybe wait until game scene load. Just in case.
            //DialogueDataObject.LoadAssetFile();
            
            // Load next scene when this is loaded?
            // Yeah. Load main menu when all of this is done. Maybe this scene can have a loading bar or something beautiful.
            SceneManager.LoadScene(1);
        }
    }
}