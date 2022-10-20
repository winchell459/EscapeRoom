using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetLocked : MonoBehaviour
{
    public Vector3 closedPos, openPos;
    public float speed = 1;
    bool open = false;
    bool moving = false;

    public bool unlocked = false;
    public GameObject inputField;


    void Update()
    {
        if (moving && unlocked)
        {
            if (open) //opening
            {
                if (Vector3.Distance(transform.localPosition, openPos) < speed * Time.deltaTime) //going to get there in this frame
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
                if (Vector3.Distance(transform.localPosition, closedPos) < speed * Time.deltaTime) //going to get there in this frame
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

    public void Locked()
    {
        Debug.Log("locked");
        inputField.SetActive(true);
    }

    public void Unlocked()
    {
        Debug.Log("unlocked");
    }    
    
}
