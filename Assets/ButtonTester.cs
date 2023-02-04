using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTester : MonoBehaviour
{
    private void Start()
    {
        TextSystem.Instance.AddElement("Button one", (() =>
        {
            Debug.Log("ADD BUTTON ONE");
        }));
        TextSystem.Instance.AddElement("Button two", (() =>
        {
            Debug.Log("ADD BUTTON TWO");
        }));
        TextSystem.Instance.AddElement("Button three", (() =>
        {
            Debug.Log("ADD BUTTON THREE");
        }));
        TextSystem.Instance.Show();
    }
}
