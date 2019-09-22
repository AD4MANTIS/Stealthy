using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Move Camera")]
    public float panSpeed;
    public float middleMousePanSpeed;
    public float panBoarderPercent;

    [Header("Zoom Camer")]
    public float scrollSpeedMouse;
    public float scrollSpeedTrigger;
    public float maxYPosition;
    public float minYPosition;
    public float maxZoom;
    public float minZoom;
    public GameObject nodeUI;

    private float currentZoom;
    private Vector3 startPosition;

    private void Start()
    {
        currentZoom = maxZoom;
        startPosition = transform.position;
        ZoomCamera();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.gameIsOver)
        {
            this.enabled = false;
            currentZoom = maxZoom;
            ZoomCamera();
            transform.position = startPosition;
            return;
        }

        // Pan across the screen when the Middle mouse button is held
        if (Input.GetKey("mouse 2"))
            MoveCameraMiddleMouse();
        else
            MoveCamera();

        // Zoom In / Out
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeedMouse;

        if (scroll == 0f)
            scroll = Input.GetAxis("Triggers") * scrollSpeedTrigger;

        if (scroll != 0)
        {
            currentZoom -= scroll * Time.deltaTime * 1000;
            ZoomCamera();
        }
    }

    private void MoveCameraMiddleMouse()
    {
        transform.Translate(Vector3.right * Time.deltaTime * middleMousePanSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
        transform.Translate(Vector3.forward * Time.deltaTime * middleMousePanSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
    }

    private void MoveCamera()
    {
        float input       = Input.GetAxisRaw("Vertical");
        bool mouseAtTop   = default;
        bool mouseAtRight = default;

        // First check if a vertical Axis ist beeing used, then check if the mouse is at the edge of the screen
        if (input != 0f || (mouseAtTop = Input.mousePosition.y >= Screen.height * (1 - panBoarderPercent)) || Input.mousePosition.y <= Screen.height * panBoarderPercent)
        {
            if (input == 0f)
                input = mouseAtTop ? 1 : -1;

            transform.Translate(Time.deltaTime * input * panSpeed * Vector3.forward, Space.World);
        }

        input = Input.GetAxisRaw("Horizontal");
        if (input != 0f || (mouseAtRight = Input.mousePosition.x >= Screen.width * (1 - panBoarderPercent)) || Input.mousePosition.x <= Screen.width * panBoarderPercent)
        {
            if (input == 0f)
                input = mouseAtRight ? 1 : -1;

            transform.Translate(Time.deltaTime * input * panSpeed * Vector3.right, Space.World);
        }
    }

    private void ZoomCamera()
    {
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Vector3 newRotation = transform.eulerAngles;
        newRotation.x = currentZoom;
        transform.eulerAngles = newRotation;

        nodeUI.transform.eulerAngles = newRotation;

        Vector3 newPosition = transform.position;
        newPosition.y = ((currentZoom - minZoom) / (maxZoom - minZoom) * (maxYPosition - minYPosition)) + minYPosition;
        transform.position = newPosition;
    }
}
