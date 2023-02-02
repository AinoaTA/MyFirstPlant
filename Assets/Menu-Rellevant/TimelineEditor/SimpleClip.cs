using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
 
[Serializable]
public class SimpleClip : PlayableAsset, ITimelineClipAsset
{
    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<SimpleBehaviour>.Create(graph);
    }
 
    // Make sure we disable all blending since we aren't handling that in the mixer
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }
}