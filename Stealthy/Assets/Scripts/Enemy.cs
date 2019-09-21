using UnityEngine;
using nvp.events;

public class Enemy : MonoBehaviour
{
    public Vector3[] route;

    private void OnTriggerEnter(Collider other)
    {
        NvpEventController.Events(MyEvent.OnHitByPlayer).TriggerEvent(this, null);
    }

    private void Update()
    {
        
    }
}
