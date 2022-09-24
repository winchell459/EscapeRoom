using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskStand : Puzzle
{
    public Flask[] flasks;
    public float[] flaskTargets;
    public float pourAmount = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Flask selectedFlask;
    public bool FlaskSelected(GameObject flaskObj)
    {
        Flask flask = GetFlask(flaskObj);

        Debug.Log($"{flask.name} was selected");

        if (!selectedFlask)
        {
            if(selectedFlask.GetValue() <= 0)
            {
                Debug.Log("empty");
            }
            selectedFlask = flask;
            return false;
        }
        else if(flask)
        {
            if (flask.GetValue() >= flask.maxValue)
            {
                Debug.Log("too full D:");
            }

            selectedFlask = null;
            return false;
        }
        else
        {
            
            //check to make sure there is enough space and fluid to pour

            //subtract the pour amount in 1, add in the other

            selectedFlask = null;
            return false;
        }
    }

    Flask GetFlask(GameObject flaskObj)
    {
        foreach(Flask flask in flasks)
        {
            if (flask.gameObject == flaskObj) return flask;
        }
        return null;
    }
}
