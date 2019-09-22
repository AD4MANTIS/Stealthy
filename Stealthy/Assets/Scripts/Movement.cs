using nvp.events;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Camera gameCamera;

    private Kiste currentKiste = null;
    private Button currentButton = null;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            TryMove(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            TryMove(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            TryMove(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TryMove(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            Interact();
    }

    private void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            Kiste kiste = hit.collider.gameObject.GetComponent<Kiste>();
            if (kiste != null)
            {
                GoInBox(kiste);
                return;
            }

            Button button = hit.collider.gameObject.GetComponent<Button>();
            if(button != null)
            {
                currentButton = button;
                button.Interact(gameObject, new ButtonInteractEventArgs(true));
            }
        }
    }

    private void TryMove(Vector3 vector)
    {
        if (currentKiste)
            LeaveBox();

        if (currentButton)
        {
            currentButton.Interact(gameObject, new ButtonInteractEventArgs(false));
            currentButton = null;
        }

        vector = vector * Time.deltaTime * speed;
        transform.rotation = Quaternion.LookRotation(vector);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, vector, out hit, 1f))
        {
            if (!hit.collider.gameObject.CompareTag("Environment"))
                Move(vector);
        }
        else
        {
            Move(vector);
        }
    }

    private void GoInBox(Kiste kiste)
    {
        NvpEventController.Events(MyEvent.HideInBox).TriggerEvent(this, null);
        currentKiste = kiste;
        kiste.Interact(gameObject, new BoxInteractEventArgs(transform.position.y * transform.localScale.y, true));

        transform.localScale *= 0.7f;

        transform.position = kiste.gameObject.transform.position;
    }

    private void LeaveBox()
    {
        NvpEventController.Events(MyEvent.LeaveBox).TriggerEvent(this, null);
        currentKiste.Interact(gameObject, new BoxInteractEventArgs(transform.position.y * transform.localScale.y, false));
        transform.localScale /= 0.7f;
        currentKiste = null;
    }

    private void Move(Vector3 vector)
    {
        transform.position += vector;
        gameCamera.transform.position += vector;
    }
}
