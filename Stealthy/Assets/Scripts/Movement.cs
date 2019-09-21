using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Camera camera;

    const float stepSize = 1f;
    const float interactRange = 3f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            TryMove(Time.deltaTime * Vector3.left);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            TryMove(Time.deltaTime * Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            TryMove(Time.deltaTime * Vector3.back);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TryMove(Time.deltaTime * Vector3.forward);
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
        transform.rotation = Quaternion.LookRotation(vector);
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
        Vector3 move = speed * vector;
        transform.position += move;
        camera.transform.position += move;
    }
}
