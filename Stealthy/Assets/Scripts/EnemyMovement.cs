using nvp.events;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState {
        Patrol = 0b0001,
        FollowsPlayer = 0b0010,
        SeesPlayer = FollowsPlayer + 0b0001,
        LostPlayer = 0b0100,
        Rotating = LostPlayer + 0b0001,
    };

    public float fieldOfViewAngle;
    public float rotateDuration;

    [HideInInspector]
    public Vector3 personalLastSighting;
    [HideInInspector]
    public EnemyState state = EnemyState.Patrol;

    public GameObject player;
    public GameObject routeParent;

    private Transform[] routePoints;
    private int currentPointIndex = 0;
    private bool goesBackwards = false;

    private NavMeshAgent nav;

    private Vector3 currentPoint => followsPlayer ? personalLastSighting : routePoints[currentPointIndex].position;
    private bool followsPlayer => state.HasFlag(EnemyState.FollowsPlayer);

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        if (routeParent != null)
        {
            routePoints = new Transform[routeParent.transform.childCount];
            for (int index = 0; index < routePoints.Length; index++)
            {
                routePoints[index] = routeParent.transform.GetChild(index);
            }
        }
        else
        {
            routePoints = new Transform[] { transform };
        }
    }

    void Update()
    {


        CheckPlayerPosition();
        if (state != EnemyState.Rotating)
        {
            if (state == EnemyState.LostPlayer)
            {
                state = EnemyState.Rotating;
                StartCoroutine(Rotate(rotateDuration));
            }
            else
            {
                MoveTo(currentPoint);

                if (Vector3.Distance(transform.position, currentPoint) < 0.6f)
                    SetNextPoint();
            }
        }
    }

    private IEnumerator Rotate(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return new WaitForFixedUpdate();
        }

        if (state == EnemyState.Rotating)
            state = EnemyState.Patrol;

        NvpEventController.Events(MyEvent.EnemyLostPlayer).TriggerEvent(this, null);
    }

    private void SetNextPoint()
    {
        if (followsPlayer)
        {
            state = EnemyState.LostPlayer;
        }

        if (goesBackwards)
            currentPointIndex--;
        else
            currentPointIndex++;

        if (currentPointIndex == routePoints.Length)
        {
            goesBackwards = true;
            currentPointIndex--;
        }
        else if (currentPointIndex < 0)
        {
            goesBackwards = false;
            currentPointIndex = 0;
        }
    }

    private void CheckPlayerPosition()
    {
        Vector3 direction = player.transform.position + Vector3.up * 3 - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    state = EnemyState.SeesPlayer;
                    personalLastSighting = player.transform.position + Vector3.up * 3;
                    NvpEventController.Events(MyEvent.EnemySeesPlayer).TriggerEvent(this, null);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.transform.position);
    }

    private void MoveTo(Vector3 target)
    {
        var path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.SetDestination(target);
        }
    }
}
