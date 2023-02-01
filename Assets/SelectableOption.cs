using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectableOption : MonoBehaviour
{
    public TMP_Text flairText;
    private Action storedAction;
    
    public void SetClickAction(string flair, Action action)
    {
        flairText.text = flair;
        storedAction = action;
    }

    public void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ShootAction);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(ShootAction);

    }

    private void ShootAction()
    {
        storedAction?.Invoke();
    }
    
}
