using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Cutegame.Subtitles
{
    [CreateAssetMenu(fileName = nameof(DialogueData) + "_Default",
        menuName = "Lost Vessel/" + nameof(DialogueData) + " Asset")]
    public class DialogueData : ScriptableObject
    {
        [SerializeField] private string setLanguage = "english";

        // This scriptable object gathers the data from the correct language file.
        // It filters it and prepares all structs for their use. 
        [SerializeField] Dictionary<string, Dialogue> _storedDialogues = new Dictionary<string, Dialogue>();

        private string fileName = "Language_ENG";
        private string languagePath = "Audio/ENG/";

        public void LoadAssetFile()
        {
            switch (setLanguage.ToLower())
            {
                case "english":
                case "en":
                case "eng":
                    fileName = "Language_ENG";
                    languagePath = "Audio/ENG/";
                    break;
                case "spanish":
                case "es":
                case "esp":
                    fileName = "Language_ESP";
                    languagePath = "Audio/ESP/";
                    break;
                case "catalan":
                case "català":
                case "cat":
                case "ca":
                    fileName = "Language_CAT";
                    languagePath = "Audio/CAT/";
                    break;
                default:
                    Debug.LogWarning("Language not supported. Loading english language file.");
                    break;
            }

            ParseText2XML();
        }

        void ParseText2XML()
        {
            var unparsedFile = Resources.Load($"Translations/{fileName}", typeof(TextAsset)) as TextAsset;

            if (unparsedFile == null)
            {
                Debug.LogError($"Could not find {fileName} in Resources/Translations/...");
                return;
            }

            #region Comments&Testing

            // Old hand-parse method. Slow and tedious to EVEN research.
            // XmlDocument xmlDoc = new XmlDocument();
            // xmlDoc.Load(new StringReader(unparsedFile.text));
            // XmlNodeList charNodes = xmlDoc.SelectNodes("//Dialogues/Phase/Character/Dialogue");
            //
            // for (int i = 0; i < charNodes.Count; i++)
            // {
            //     for (int j = 0; j < charNodes[i].Attributes.Count; j++)
            //     {
            //         Debug.Log($"{charNodes[i].Attributes[j].Name}: {charNodes[i].Attributes[j].InnerText}" );
            //     }
            //
            //     for (int j = 0; j < charNodes[i].ChildNodes.Count; j++)
            //     {
            //         Debug.Log($"{charNodes[i].ChildNodes[j].Name}: {charNodes[i].ChildNodes[j].InnerText}" );
            //     }
            // }

            // Those above are the information we care about, but how about we use a struct with XMLElement?

            // this prints al the entries, each i is each character.
            // for(int i = 0; i < charNodes.Count; i++)
            // {
            //     Debug.Log(charNodes[i].InnerText);
            // }
            // Get the character node and load each dialogue.

            // To text.
            // XmlDocument xmlDocument = new XmlDocument();
            // xmlDocument.Load($"Assets/Resources/Translations/{fileName}");

            // Get elements.
            // XmlNodeList charName = xmlDocument.GetElementsByTagName("Character");

            // Every charname has the entries of ALL IDs for every character. So charName[0] has Lobo because its the first.
            // Debug.Log(charName.Item(0).Attributes[0].InnerText); // This outputs "LOBO". As per name of the character.
            // Debug.Log(charName.Item(0).Attributes[1].InnerText); // Out of range. Probably because the category CHARACTER has only NAME

            // for (int i = 0; i < charName.Count; i++)
            // {
            //     Debug.Log(charName[i].InnerText);
            // }

            #endregion

            // Basically all above summed in one line.
            Dialogues data = FromXML<Dialogues>(unparsedFile.text);
            foreach (var character in data.Phase.Character)
            {
                foreach (var dialogue in character.Dialogue)
                {
                    if (_storedDialogues.ContainsKey(dialogue.ID)) continue;
                    _storedDialogues.Add(dialogue.ID, dialogue);
                    dialogue.Initialize(languagePath);
                    // Debug.Log($"Added dialogue {dialogue.ID}");
                }
            }
        }

        T FromXML<T>(string value)
        {
            using (TextReader reader = new StringReader(value))
            {
                return (T) new XmlSerializer(typeof(T)).Deserialize(reader);
            }
        }

        public void SetLanguage(string newLang)
        {
            setLanguage = newLang;
        }

        #region Getters

        public Dialogue GetDialogueFromID(int id)
        {
            return GetDialogueFromID(id.ToString());
        }

        // Use this. IDs could be MORE than int types.
        public Dialogue GetDialogueFromID(string id)
        {
            _storedDialogues.TryGetValue(id, out Dialogue result);
            return result;
        }

        public List<Dialogue> GetDialogueSequence(string entryID)
        {
            List<Dialogue> returnValue = new List<Dialogue>();
            string nextid = entryID;
            int its = 0;
            // DANGEROUS
            while (nextid != "-1" && its < 30)
            {
                _storedDialogues.TryGetValue(nextid, out Dialogue value);
                if (value != null)
                {
                    returnValue.Add(value);
                    nextid = value.Next_ID;
                }
                else
                {
                    if (_storedDialogues.Count <= 0)
                    {
                        Debug.LogError($"Dialogues are not loaded. Try starting from INIT scene.");
                    }
                    else
                    {
                        Debug.LogError($"Dialogues loaded incorrectly. Dialogue ID: {nextid} did not exist.");
                    }

                    break;
                }

                its++;
            }

            return returnValue;
        }

        public List<string> GetAllEvents()
        {
            return _storedDialogues.Select(x => x.Value.Event).ToList();
        }

        #endregion
    }
}