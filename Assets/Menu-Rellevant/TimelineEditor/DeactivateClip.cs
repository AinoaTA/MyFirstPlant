using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
 
[Serializable]
public class DeactivateClip : PlayableAsset, ITimelineClipAsset
{
    public ExposedReference<GameObject> Target;
 
    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        ScriptPlayable<DeactivateBehaviour> playable = ScriptPlayable<DeactivateBehaviour>.Create(graph);
 
        DeactivateBehaviour playableBehaviour = playable.GetBehaviour();
 
        playableBehaviour.Target = Target.Resolve(graph.GetResolver());
 
        return playable;
    }
 
    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }
}
