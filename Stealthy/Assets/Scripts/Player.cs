using UnityEngine;
using nvp.events;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        NvpEventController.Events(MyEvent.OnHitByPlayer).GameEventHandler += Player_GameEventHandler;
    }

    private void OnDisable()
    {
        NvpEventController.Events(MyEvent.OnHitByPlayer).GameEventHandler -= Player_GameEventHandler;
    }

    private void Player_GameEventHandler(object sender, System.EventArgs e)
    {
        Debug.Log("OnHitByPlayer");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Enemy>() != null)
                NvpEventController.Events(MyEvent.PlayerSeesEnemy).TriggerEvent(this, null);
        }
    }

    private void Hit(object param1, object param2)
    {
        Debug.Log("Hit");
    }
}
