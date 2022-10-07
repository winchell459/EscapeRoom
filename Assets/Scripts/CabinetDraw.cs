using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDraw : MonoBehaviour
{
    public Vector3 closedPos, openPos;
    public float speed = 1;
    bool open = false;
    bool moving = false;

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            
            if (open) //opening
            {
                if(Vector3.Distance(transform.localPosition, openPos) < speed * Time.deltaTime)
                {
                    transform.localPosition = openPos;
                    moving = false;
                }
                else
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, openPos, speed * Time.deltaTime);
                }
            }
            else //closing
            {
                if (Vector3.Distance(transform.localPosition, closedPos) < speed * Time.deltaTime)
                {
                    transform.localPosition = closedPos;
                    moving = false;
                }
                else
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, closedPos, speed * Time.deltaTime);
                }
            }
        }
    }

    public void Selected()
    {
        if (!moving)
        {
            open = !open;
            moving = true;
        }
    }
}
