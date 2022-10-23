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
    [SerializeField] GameObject Face;
    [SerializeField] GameObject Camera;

    [SerializeField] CubeSolve solver;
    [SerializeField] Menu menu;

    [SerializeField] Slider slider;
    [SerializeField] Text sliderValue;
    [SerializeField] Slider shuffleSlider;
    [SerializeField] Text shuffleSliderValue;

    List<GameObject> rubiksCube;
    List<GameObject> slices;
    List<GameObject> faces;

    public int sideLength = 3;
    public int shuffleLength = 0;
    int currentLength;
    public bool IsRotating = false;

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
        faces = new List<GameObject>();

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
        if (!IsRotating)
        {
            sideLength = (int)slider.value;
            sliderValue.text = sideLength.ToString();
            shuffleLength = (int)shuffleSlider.value;
            shuffleSliderValue.text = shuffleLength.ToString();

            if (currentLength != sideLength)
            {
                currentLength = sideLength;
                DeleteCube();
                Generate(currentLength);

                //Camera.transform.position = new Vector3(0, 1, -10 - sideLength * 2);
            }
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

        GenerateFaces(length);
    }

    void GenerateFaces(int size)
    {
        GameObject face1 = Instantiate(Face, new Vector3(0, size, 0), Quaternion.identity);
        face1.transform.localScale = new Vector3(size * 2, 1, size * 2);
        face1.transform.parent = center.transform;
        face1.tag = "Face1";
        faces.Add(face1);
        GameObject face2 = Instantiate(Face, new Vector3(0, -size, 0), Quaternion.identity);
        face2.transform.localScale = new Vector3(size * 2, 1, size * 2);
        face2.transform.parent = center.transform;
        face2.tag = "Face2";
        faces.Add(face2);
        GameObject face3 = Instantiate(Face, new Vector3(size, 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
        face3.transform.localScale = new Vector3(size * 2, 1, size * 2);
        face3.transform.parent = center.transform;
        face3.tag = "Face3";
        faces.Add(face3);
        GameObject face4 = Instantiate(Face, new Vector3(- size, 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
        face4.transform.localScale = new Vector3(size * 2, 1, size * 2);
        face4.transform.parent = center.transform;
        face4.tag = "Face4";
        faces.Add(face4);
        GameObject face5 = Instantiate(Face, new Vector3(0, 0, size), Quaternion.AngleAxis(90, Vector3.right));
        face5.transform.localScale = new Vector3(size * 2, 1, size * 2);
        face5.transform.parent = center.transform;
        face5.tag = "Face5";
        faces.Add(face5);
        GameObject face6 = Instantiate(Face, new Vector3(0, 0, - size), Quaternion.AngleAxis(90, Vector3.right));
        face6.transform.localScale = new Vector3(size * 2, 1, size * 2);
        face6.transform.parent = center.transform;
        face6.tag = "Face6";
        faces.Add(face6);
    }

    void DeleteCube()
    {
        for (int i = 0; i < rubiksCube.Count; i++)
            Destroy(rubiksCube[i]);

        for (int i = 0; i < slices.Count; i++)
            Destroy(slices[i]);

        for (int i = 0; i < faces.Count; i++)
            Destroy(faces[i]);

        rubiksCube.Clear();
        slices.Clear();
        faces.Clear();
    }

    IEnumerator ShuffleManager(int numberOfShuffles)
    {
        IsRotating = true;

        for (int j = 0; j < numberOfShuffles; j++)
        {
            int i = Random.Range(0, slices.Count);
            RotateSlice s = slices[i].GetComponent<RotateSlice>();
            int k = Random.Range(0, 2);
            if (k == 0)
            {
                solver.AddCubeAction(s, 1);
                s.SliceRotate(1);
            }
            else
            {
                solver.AddCubeAction(s, -1);
                s.SliceRotate(-1);
            }

            yield return new WaitUntil(() => s.EndRotation == true);

            s.EndRotation = false;
            yield return null;
        }

        IsRotating = false;
    }

    public void Shuffle()
    {
        if(!IsRotating)
            StartCoroutine(ShuffleManager(shuffleLength));
    }

    public void Restart()
    {
        solver.ClearActions();
        DeleteCube();
        Generate(currentLength);
        menu.SolvedNO();
        solver.SolveDone = false;
        IsRotating = false;
    }
}