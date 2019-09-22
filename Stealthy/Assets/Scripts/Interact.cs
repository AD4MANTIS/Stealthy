using System;
using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject interacter, EventArgs e);
}

public class BoxInteractEventArgs : EventArgs
{
    public float y;
    public bool enter;

    public BoxInteractEventArgs(float y, bool enter)
    {
        this.y = y;
        this.enter = enter;
    }
}

public class ButtonInteractEventArgs : EventArgs
{
    public bool activate;

    public ButtonInteractEventArgs(bool activate)
    {
        this.activate = activate;
    }
}