using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostKey : Puzzle
{
    public GameObject key;
    protected override void PuzzleComplete()
    {
        key.SetActive(true);
    }
}
