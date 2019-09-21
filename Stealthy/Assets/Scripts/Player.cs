using UnityEngine;
using nvp.events;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
