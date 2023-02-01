using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Custom
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] TextAsset _intereses, _desintereses;
        public List<string> allIntereses;
        public List<string> allDesintereses;

        [System.Serializable]
        public struct EditProfile 
        { 
            public ZodiacSign zodiacSigns;
            public List<string> intereses;
            public List<string> desintereses;
        }

        [SerializeField] private EditProfile _editedProfile;

        private void Start()
        { 
            allIntereses = _intereses.text.Split('\n').ToList();
            allDesintereses = _desintereses.text.Split('\n').ToList();
        } 
    }
}