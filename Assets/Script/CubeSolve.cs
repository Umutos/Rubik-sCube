using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSolve : MonoBehaviour
{
    [SerializeField] CubesGenerator generator;
    [SerializeField] Menu menu;

    public bool SolveDone = false;

    List<RotateSlice> slices = new List<RotateSlice>();
    List<int> senses = new List<int>();

    public void AddCubeAction(RotateSlice slice, int sens)
    {
        slices.Add(slice);
        senses.Add(sens);
    }

    IEnumerator SolvingCube()
    {
        generator.IsRotating = true;

        for (int i = slices.Count - 1; i >= 0; i--)
        {
            slices[i].SliceRotate(senses[i] * -1);

            yield return new WaitUntil(() => slices[i].EndRotation == true);

            slices[i].EndRotation = false;
            yield return null;
        }

        slices.Clear();
        senses.Clear();

        generator.IsRotating = false;
        if (generator.IsRotating == false)
        {
            SolveDone = true;
        }
    }

    public void SolveRubiksCube()
    {
        if (generator.IsRotating == false)
        {
            StartCoroutine(SolvingCube());
        }
    }

    private void Update()
    {
        if (SolveDone == true)
        {
            menu.SolvedOK();
        }
        else
        {
            menu.SolvedNO();
        }
    }

    public void ClearActions()
    {
        slices.Clear();
        senses.Clear();
    }
}
