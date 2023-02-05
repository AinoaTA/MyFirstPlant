using PixelCrushers.DialogueSystem;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DialogueScripts : MonoBehaviour
{
    public void SayDebug(string deb)
    {
        Debug.Log(deb);
    }
    
    private void OnEnable()
    {
        Lua.RegisterFunction("ChangeFace", this, SymbolExtensions.GetMethodInfo(() => ChangeFace(string.Empty)));
        Lua.RegisterFunction("PlayAnimation", this, SymbolExtensions.GetMethodInfo(() => PlayAnimation(string.Empty)));
        Lua.RegisterFunction("Debug/SayDebug", this, SymbolExtensions.GetMethodInfo( () => SayDebug(string.Empty)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("Debug/SayDebug");
        Lua.UnregisterFunction("ChangeFace");
        Lua.UnregisterFunction("PlayAnimation");
    }

    public void ChangeFace(string face)
    {
        //Debug.Log(DialogueManager.CurrentConversationState.subtitle.speakerInfo.Name);
        DialogueManager.CurrentConversationState.subtitle.speakerInfo.transform.gameObject.GetComponent<Personaje>().ChangeFace(face);
    }

    public void PlayAnimation(string trigger)
    {
        DialogueManager.CurrentConversationState.subtitle.speakerInfo.transform.gameObject.GetComponent<Personaje>().PlayAnimation(trigger);
    }
}
