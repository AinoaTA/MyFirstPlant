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
        Lua.RegisterFunction("SayDebug", this, SymbolExtensions.GetMethodInfo( () => SayDebug(string.Empty)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("SayDebug");
    }
}
