using System.Collections;
using System.Collections.Generic;
using Cutegame.Options;
using UnityEngine;

namespace Cutegame.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerCharacterData), menuName = "Cutegame/"+ nameof(PlayerCharacterData))]
    public class PlayerCharacterData : ScriptableObject
    {
        [SerializeField] private GameplaySettings _settings;
        
        public float Sensitivity => _settings.GetSensitivity();

        public float MovementSpeed;
        
        public float JumpingSpeed;

        public float RunningSpeed;

        public float TurningSpeed;

        public float Gravity;
    }
}