using UnityEngine;
using nvp.events;
using System;

public class Enemy : MonoBehaviour
{
    public EnemyMovement movement;

    const float interactRange = 3f;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            NvpEventController.Events(MyEvent.OnHitByPlayer).TriggerEvent(this, new EnemyHitEventArgs(this, !movement.state.HasFlag(EnemyMovement.EnemyState.SeesPlayer)));
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

public class EnemyHitEventArgs : EventArgs
{
    public bool enemyKilled;
    public Enemy enemy;

    public EnemyHitEventArgs(Enemy enemy,bool enemyKilled)
    {
        this.enemy = enemy;
        this.enemyKilled = enemyKilled;
    }
}
