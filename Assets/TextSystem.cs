using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextSystem : MonoBehaviour
{
    private static TextSystem _instance;

    public static TextSystem Instance
    {
        get => _instance;
        set
        {
            if (_instance != null)
            {
                Destroy(value.gameObject);
                return;
            }

            _instance = value;
        }
    }
    
    public LayoutElement loElement;
    public float sizePerButton = 50f;
    public VerticalLayoutGroup buttonsRoot;

    public List<SelectableOption> buttons;

    [Header("Prefab")]
    public SelectableOption prefab;

    private CanvasFadeIn _fadeIn;

    public Action OnUpdatedElements;

    private void Awake()
    {
        Instance = this;
        _fadeIn = GetComponent<CanvasFadeIn>();
    }

    public void AddElement(string flair, Action invocation)
    {
        // Instantiate element
        // Add action on click
        // Add to list
        // Resize
        var a = Instantiate(prefab, buttonsRoot.transform, false);
        a.SetClickAction(flair, invocation);
        buttons.Add(a);
        OnUpdatedElements?.Invoke();
    }

    public void Show()
    {
        // Show this item.
        _fadeIn.Show();
        // Change player inputs to UI.
        // Set the focus on the first element of the list.
        if (buttons.Count > 0)
            FindObjectOfType<EventSystem>().firstSelectedGameObject = buttons[0].gameObject;
        // DO SOMETHING ELSE?
        // InputManager.
    }
    
    void ResizeLayout()
    {
        loElement.preferredHeight = buttons.Count * sizePerButton;
    }

    private void OnEnable()
    {
        OnUpdatedElements += ResizeLayout;
    }

    private void OnDisable()
    {
        OnUpdatedElements -= ResizeLayout;
    }
}