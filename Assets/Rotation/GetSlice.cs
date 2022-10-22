using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSlice : MonoBehaviour
{
    public List<GameObject> slice = new List<GameObject>();
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
        slice.Add(other.gameObject);
        
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            slice.Remove(other.gameObject);
        }

    }
}
