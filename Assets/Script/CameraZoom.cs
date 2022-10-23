using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    public float speedZoom;
    private float targetZoom;
    private float zoomFactor = 10f;
    public bool OrthoORPerspec;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(scrollData);

        if (OrthoORPerspec == true)
        {
            cam.orthographic = true;
            targetZoom -= scrollData * zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom, 4.5f, 800);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * speedZoom);
        }
        else
        {
            cam.orthographic = false;
            Vector3 ZBOUB = target.position - transform.position;
            ZBOUB.Normalize();
            transform.position = transform.position + ZBOUB * scrollData * speedZoom;
        }

    }
}
