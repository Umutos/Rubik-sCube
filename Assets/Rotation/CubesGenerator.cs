using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubesGenerator : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject center;
    [SerializeField] GameObject XSlice;
    [SerializeField] GameObject YSlice;
    [SerializeField] GameObject ZSlice;
    [SerializeField] GameObject Camera;
    [SerializeField] Slider slider;
    [SerializeField] Text sliderValue;
    List<GameObject> rubiksCube;
    List<GameObject> slices;
    public int sideLength = 3;
    int currentLength;
    Quaternion rotation;
    Quaternion rotationSliceX;
    Quaternion rotationSliceY;
    Quaternion rotationSliceZ;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Quaternion.identity;
        rotationSliceX = Quaternion.AngleAxis(0, Vector3.up);
        rotationSliceY = Quaternion.AngleAxis(90, Vector3.right);
        rotationSliceZ = Quaternion.AngleAxis(90, Vector3.up);
        rubiksCube = new List<GameObject>();
        slices = new List<GameObject>();
        if (sideLength > 10)
            sideLength = 10;
        if (sideLength < 2)
            sideLength = 2;
        currentLength = sideLength;
        Generate(currentLength);
    }

    // Update is called once per frame
    void Update()
    {
        sideLength = (int)slider.value;
        sliderValue.text = sideLength.ToString();

        if (currentLength != sideLength)
        {
            if (sideLength > 10)
                sideLength = 10;
            if (sideLength < 2)
                sideLength = 2;

            currentLength = sideLength;
            DeleteCube();
            Generate(currentLength);

            Camera.transform.position = new Vector3(0, 1, -10 - sideLength * 2);
        }
    }

    void Generate(int length)
    {
        for (int i = 0; i < length; i++)
        {
            GameObject slicex = Instantiate(XSlice, new Vector3(0, 0, (i - length / 2f) * 2 + 1), rotationSliceX);
            slicex.transform.parent = center.transform;
            slices.Add(slicex);
            GameObject slicey = Instantiate(YSlice, new Vector3(0, (i - length / 2f) * 2 + 1, 0), rotationSliceY);
            slicey.transform.parent = center.transform;
            slices.Add(slicey);
            GameObject slicez = Instantiate(ZSlice, new Vector3((i - length / 2f) * 2 + 1, 0, 0), rotationSliceZ);
            slicez.transform.parent = center.transform;
            slices.Add(slicez);

            for (int j = 0; j < length; j++)
            {
                for (int k = 0; k < length; k++)
                {
                    GameObject c = Instantiate(cube, new Vector3((i - length / 2f) * 2 + 1, (j - length / 2f) * 2 + 1, (k - length / 2f) * 2 + 1), rotation);
                    //c.transform.localScale = new Vector3(3f / length, 3f / length, 3f / length);
                    c.transform.parent = center.transform;
                    rubiksCube.Add(c);
                }
            }
        }
    }

    void DeleteCube()
    {
        for (int i = 0; i < rubiksCube.Count; i++)
            Destroy(rubiksCube[i]);

        for (int i = 0; i < slices.Count; i++)
            Destroy(slices[i]);

        rubiksCube.Clear();
        slices.Clear();
    }

    public void SetLength(int newLength)
    {
        sideLength = newLength;
    }

    public void Shuffle()
    {
        int i = Random.Range(0, slices.Count);
        RotateSlice s = slices[i].GetComponent<RotateSlice>();
        s.SliceRotate();
    }
}
