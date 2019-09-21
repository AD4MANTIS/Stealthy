using UnityEngine;
using nvp.events;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
            NvpEventController.Events(MyEvent.OnHitByPlayer).TriggerEvent(this, null);
    }
}
