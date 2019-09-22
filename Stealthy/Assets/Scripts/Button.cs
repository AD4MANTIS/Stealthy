using System;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public Light lightSource;

    public void Interact(GameObject interacter, EventArgs e)
    {
        if (e is ButtonInteractEventArgs buttonArgs)
            lightSource.enabled = buttonArgs.activate;
        else
            lightSource.enabled = !lightSource.enabled;
    }
}
