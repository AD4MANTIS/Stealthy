using System;
using UnityEngine;

public class Kiste : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interacter, EventArgs e)
    {
        transform.Rotate(transform.forward, 180f);
        if (e is BoxInteractEventArgs boxArgs)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = boxArgs.y;
            transform.position = newPosition;
        }
    }
}
