using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksCubeCreator : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject center;
    List<GameObject> rubiksCube;
    public int sideLength = 3;
    int currentLength;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Quaternion.identity;
        rubiksCube = new List<GameObject>();
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
        if (currentLength != sideLength)
        {
            if (sideLength > 10)
                sideLength = 10;
            if (sideLength < 2)
                sideLength = 2;

            currentLength = sideLength;
            DeleteCube();
            Generate(currentLength);
        }
    }

    void Generate(int length)
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                for (int k = 0; k < length; k++)
                {
                    GameObject c = Instantiate(cube, new Vector3((i - length / 2f ) * 2, (j - length / 2f) * 2, (k - length / 2f) * 2), rotation);
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

        rubiksCube.Clear();
    }
}
