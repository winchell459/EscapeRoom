using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleDoor : Puzzle
{
    public Animator anim;
    protected override void PuzzleComplete()
    {
        //FindObjectOfType<GameHandler>().ObjectTriggered(this);
        anim.SetBool("open", true);
    }
}
