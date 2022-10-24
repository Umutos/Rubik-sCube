using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlice : MonoBehaviour
{
    [SerializeField] GetSlice slice;

    public bool EndRotation = false;
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

    IEnumerator Rotation(int rotationSens)
    {
        LockCubes();

        Quaternion newRotation = transform.rotation * Quaternion.AngleAxis(90 * rotationSens, transform.worldToLocalMatrix.MultiplyVector(transform.forward));

        float time = 0.5f;
        float lapse = 0;
        while (lapse < time)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, lapse / time);
            lapse += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1);

        UnlockCubes();

        EndRotation = true;

        yield return new WaitForSecondsRealtime(0.2f);

        EndRotation = false;
    }

    public void SliceRotate(int rotationSens = 1)
    {
        StartCoroutine(Rotation(rotationSens));
    }
}
