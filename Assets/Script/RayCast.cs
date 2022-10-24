using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] CubeSolve solver;
    Camera cam;
    public int face = 0;
    public LayerMask mask;
    public LayerMask faceMask;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    float timer;
    public GameObject cube;
    bool isTurning;
    void Start()
    {
        cam = Camera.main;
        timer = 0;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position,
        Color.blue);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, faceMask))
        {
            if (hit.transform.tag == "Face1")
            {
                face = 1;
            }
            if (hit.transform.tag == "Face2")
            {
                face = 2;
            }
            if (hit.transform.tag == "Face3")
            {
                face = 3;
            }
            if (hit.transform.tag == "Face4")
            {
                face = 4;
            }
            if (hit.transform.tag == "Face5")
            {
                face = 5;
            }
            if (hit.transform.tag == "Face6")
            {
                face = 6;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 100, mask))
            {
                cube = hit2.transform.gameObject;
            }
        }

        if (!solver.generator.IsRotating)
        {
            if (timer <= 0)
                Swipe();
            else
                timer -= Time.deltaTime;
        }
    }
    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
        }
        if (LeftSwipe(currentSwipe) )
        {
            timer = 0.5f;
            switch (face)
            {
                case 1:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), 1);
                    break;
                case 2:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), -1);
                    break;
                case 3:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), -1);
                    break;
                case 4:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), -1);
                    break;
                case 5:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), -1);
                    break;
                case 6:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), -1);
                    break;
            }
            
            Debug.Log("Left");
            cube = null;
            currentSwipe = Vector2.zero;
        }
        if (RightSwipe(currentSwipe))
        {
            timer = 0.5f;
            switch (face)
            {
                case 1:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), -1);
                    break;
                case 2:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), 1);
                    break;
                case 3:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), 1);
                    break;
                case 4:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), 1);
                    break;
                case 5:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), 1);
                    break;
                case 6:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceY.GetComponent<RotateSlice>(), 1);
                    break;
            }
           
            Debug.Log("Right");
            cube = null;
            currentSwipe = Vector2.zero;

        }
        if (UpSwipe(currentSwipe))
        {
            timer = 0.5f;
            switch (face)
            {
                case 1:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), 1);
                    break;
                case 2:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), 1);
                    break;
                case 3:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), 1);
                    break;
                case 4:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), -1);
                    break;
                case 5:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), -1);
                    break;
                case 6:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), 1);
                    break;
            }
            
            Debug.Log("Up");
            cube = null;
            currentSwipe = Vector2.zero;
        }
        if (DownSwipe(currentSwipe))
        {
            timer = 0.5f;
            switch (face)
            {
                case 1:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), -1);
                    break;
                case 2:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), -1);
                    break;
                case 3:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), -1);
                    break;
                case 4:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceX.GetComponent<RotateSlice>(), 1);
                    break;
                case 5:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), 1);
                    break;
                case 6:
                    Rotation(cube.GetComponent<GetSlicesFromCube>().sliceZ.GetComponent<RotateSlice>(), -1);
                    break;
            }
            
            Debug.Log("Down");
            cube = null;
            currentSwipe = Vector2.zero;
        }

    }

    void Rotation(RotateSlice slice, int sens)
    {
        solver.AddCubeAction(slice, sens);
        slice.SliceRotate(sens);
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    bool DownSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f;
    }
    bool UpSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f;
    }
}
