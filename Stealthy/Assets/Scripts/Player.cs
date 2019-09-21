using UnityEngine;
using nvp.events;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        NvpEventController.Events("OnHitByPlayer").GameEventHandler += Player_GameEventHandler;
    }

    private void Player_GameEventHandler(object sender, System.EventArgs e)
    {
        Debug.Log("OnHitByPlayer");
    }

    private void OnDisable()
    {
        NvpEventController.Events("OnHitByPlayer").GameEventHandler -= Player_GameEventHandler;
    }

    void Update()
    {
        
    }

    private void Hit(object param1, object param2)
    {
        Debug.Log("Hit");
    }
}
