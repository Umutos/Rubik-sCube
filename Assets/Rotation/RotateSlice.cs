using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlice : MonoBehaviour
{
    [SerializeField] GetSlice slice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LockCubes()
    {
        foreach (GameObject i in slice.slice)
        {
            i.transform.parent = transform;
        }
    }

    void UnlockCubes()
    {
        foreach (GameObject i in slice.slice)
        {
            i.transform.parent = transform.parent;
        }
    }

    public void SliceRotate()
    {
        LockCubes();

        transform.rotation *= Quaternion.AngleAxis(90, transform.worldToLocalMatrix.MultiplyVector(transform.forward));

        UnlockCubes();
    }
}
