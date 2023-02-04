using System.Xml.Serialization;
using UnityEditor.Rendering;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cutegame.Subtitles
{
    // Use http://xmltocsharp.azurewebsites.net/
    [XmlRoot(ElementName = "Dialogue")]
    public struct StoredDialogue
    {
        // Maybe useless? Or not. Who knows.
        [XmlElement("ID")] public string id;

        // NOT USED? MAYBE JUST IGNORE HERE.
        public string characterName;

        [XmlElement("Text")] public string text;
        [XmlElement("Duration")] public float duration;
        [XmlElement("Sound_Path")] public string sound_path;
        [XmlElement("Music_Path")] public string music_path;
        [XmlElement("Event")] public string event_name;
        [XmlElement("Next_ID")] public string next_id;

        public StoredDialogue(
            string id, string charName, string text,
            float duration, string soundPath,
            string musicPath, string eventName, string nextId)
        {
            this.id = id;
            this.characterName = charName;
            this.text = text;
            this.duration = duration;
            this.sound_path = soundPath;
            this.music_path = musicPath;
            this.event_name = eventName;
            this.next_id = nextId;
        }
    }

    [XmlRoot(ElementName = "Dialogue")]
    public class Dialogue
    {
        [XmlAttribute(AttributeName = "ID")] public string ID { get; set; }
        [XmlElement(ElementName = "Text")] public string Text { get; set; }
        [XmlElement(ElementName = "Duration")] public string Duration { get; set; }

        public AudioClip SoundClip;
        [XmlElement(ElementName = "Sound_Path")] public string Sound_Path { get; set; }

        public AudioClip MusicClip;
        [XmlElement(ElementName = "Music_Path")] public string Music_Path { get; set; }

        [XmlElement(ElementName = "Event")] public string Event { get; set; }
        [XmlElement(ElementName = "Next_ID")] public string Next_ID { get; set; }

        public void Initialize(string languagePath)
        {
            SoundClip = Resources.Load<AudioClip>(languagePath + Sound_Path) as AudioClip;
            MusicClip = Resources.Load<AudioClip>("Audio/" + Music_Path) as AudioClip;
        }
    }

    [XmlRoot(ElementName = "Character")]
    public class Character
    {
        [XmlElement(ElementName = "Dialogue")] public List<Dialogue> Dialogue { get; set; }
        [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Phase")]
    public class Phase
    {
        [XmlElement(ElementName = "Character")]
        public List<Character> Character { get; set; }

        [XmlAttribute(AttributeName = "Number")]
        public string Number { get; set; }
    }

    [XmlRoot(ElementName = "Dialogues")]
    public class Dialogues
    {
        [XmlElement(ElementName = "Phase")] public Phase Phase { get; set; }
    }
}