using UnityEngine;
using nvp.events;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        NvpEventController.Events("OnHitByPlayer").TriggerEvent(this, null);
    }

    public void PointerEntered()
    {
    }
}
