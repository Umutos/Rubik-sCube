using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCube : MonoBehaviour
{
    public Transform tr;
    public int rotateSpeed;
    public Transform pivot;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        
    }
    public void Rotation()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Rotate");
            float Horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            float Vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            pivot.Rotate(Vertical, Horizontal, 0);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private void Update()
    {
        Rotation();
    }
}
