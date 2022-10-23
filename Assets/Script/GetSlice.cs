using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSlice : MonoBehaviour
{
    public List<GameObject> slice = new List<GameObject>();
    
    public int TypeOfSlice;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            slice.Add(other.gameObject);
        }

        switch (TypeOfSlice)
        {
            case 1:

                for (int i = 0; i < slice.Count; i++)
                {
                    other.gameObject.GetComponent<GetSlicesFromCube>().sliceX = gameObject;
                }

                break;
            case 2:
                for (int i = 0; i < slice.Count; i++)
                {
                    other.gameObject.GetComponent<GetSlicesFromCube>().sliceY = gameObject;
                }
                break;
            case 3:
                for (int i = 0; i < slice.Count; i++)
                {
                    other.gameObject.GetComponent<GetSlicesFromCube>().sliceZ = gameObject;
                }
                break;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            slice.Remove(other.gameObject);
        }

    }
}
