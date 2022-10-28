using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Puzzle
{
    public StatuePuzzleDoor statuePuzzleDoor;

    public float rotateRate = 90;
    public float angleTarget = 180;

    private void Update()
    {
        if(transform.localEulerAngles.y > angleTarget - 1 && transform.localEulerAngles.y < angleTarget + 1)
        {
            statuePuzzleDoor.ObjectTriggered(this);
        }
    }

    public void Selected()
    {
        transform.Rotate(90 * Vector3.up, Space.World);
    }
}
