using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsoleUIButton : MonoBehaviour
{
    public UnityEvent OnClickDelegate;

    public void OnClick()
    {
        OnClickDelegate.Invoke();
    }
}