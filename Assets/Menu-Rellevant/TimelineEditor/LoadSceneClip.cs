using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
 
[Serializable]
public class LoadSceneClip : PlayableAsset, ITimelineClipAsset
{
    public string SceneToLoad;
 
    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        ScriptPlayable<LoadSceneBehaviour> playable = ScriptPlayable<LoadSceneBehaviour>.Create(graph);
 
        LoadSceneBehaviour playableBehaviour = playable.GetBehaviour();

        playableBehaviour.SceneToLoad = SceneToLoad;
 
        return playable;
    }
 
    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }
}
