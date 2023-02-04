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
        Lua.RegisterFunction("SayDebug", this, SymbolExtensions.GetMethodInfo( () => SayDebug(string.Empty)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("SayDebug");
        Lua.UnregisterFunction("ChangeFace");
    }

    public void ChangeFace(string face)
    {
        // 
        Debug.Log(DialogueManager.currentConversant.gameObject.name);
        DialogueManager.currentConversant.gameObject.GetComponent<Personaje>().ChangeFace(face);
    }
}
