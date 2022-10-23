using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private Quaternion camRotation;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        camRotation = transform.localRotation;
    }

    void Rotation()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            camRotation.x += Input.GetAxis("Mouse Y") * rotateSpeed;
            camRotation.y += Input.GetAxis("Mouse X") * rotateSpeed;

            transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
