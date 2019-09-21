using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void Start()
    {
    }

    private void OnDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += transform.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position -= transform.forward;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position -= transform.right;
            transform.Rotate(Vector3.up, -90f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += transform.right;
            transform.Rotate(Vector3.up, 90f);
        }
    }
}
