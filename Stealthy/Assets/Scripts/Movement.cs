using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float stepSize = 1f;
    const float interactRange = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TryMove(transform.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TryMove(-transform.forward);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -90f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 90f);
        }
        
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            Interact();
    }

    private void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {

        }
    }

    private void TryMove(Vector3 vector)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, vector, out hit, stepSize))
        {
            if (!hit.collider.gameObject.CompareTag("Environment"))
                Move(vector);
        }
        else
        {
            Move(vector);
        }
    }

    private void Move(Vector3 vector)
    {
        transform.position += vector;
    }
}
