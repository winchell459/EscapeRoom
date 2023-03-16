using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostKey : Puzzle
{
    public GameObject key;
    protected virtual void PuzzleComplete()
    {
        key.SetActive(true);
    }
}
