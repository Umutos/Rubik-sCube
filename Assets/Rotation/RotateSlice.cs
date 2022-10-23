using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlice : MonoBehaviour
{
    [SerializeField] GetSlice slice;
    float rotationSpeed = 100f;
    Quaternion newRotation = Quaternion.identity;
    private void Update()
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

    IEnumerator Rotation()
    {
        LockCubes();

        Quaternion newRotation = transform.rotation * Quaternion.AngleAxis(90, transform.worldToLocalMatrix.MultiplyVector(transform.forward));

        float time = 1f;
        float lapse = 0;
        while (lapse < time)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, lapse / time);
            lapse += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1);

        UnlockCubes();
    }

    public void SliceRotate()
    {
        StartCoroutine(Rotation());
    }
}
