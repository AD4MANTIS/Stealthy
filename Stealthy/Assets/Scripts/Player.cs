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
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        GetComponent<Movement>().enabled = false;
        yield return new WaitForSecondsRealtime(7f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
