using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : Puzzle
{
    public GameObject spawnObject;
    
    protected override void PuzzleComplete()
    {
        if (!puzzleComplete)
        {
            spawnObject.SetActive(true);
        }
        puzzleComplete = true;
    }
}
