using UnityEngine;
using nvp.events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        NvpEventController.Events(MyEvent.OnHitByPlayer).GameEventHandler += Player_GotHit;
    }

    private void OnDisable()
    {
        NvpEventController.Events(MyEvent.OnHitByPlayer).GameEventHandler -= Player_GotHit;
    }

    private void Player_GotHit(object sender, System.EventArgs e)
    {
        if (e is EnemyHitEventArgs enemyHitArgs && enemyHitArgs.enemyKilled)
        {
            enemyHitArgs.enemy.Kill();
        }
        else
        {
            NvpEventController.Events(MyEvent.PlayerDies).TriggerEvent(sender, e);
        }
    }

    bool sawEnemy = false;

    private void Update()
    {
        if (sawEnemy)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Enemy>() != null)
                NvpEventController.Events(MyEvent.PlayerSeesEnemy).TriggerEvent(this, null);
        }
        sawEnemy = true;
    }
}
