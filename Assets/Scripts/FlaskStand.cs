using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskStand : Puzzle
{
    public Flask[] flasks;
    public float[] flaskTargets;
    public float pourAmount = 15;
    public GameObject paper;

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


        if (!selectedFlask) //first selection
        {
            selectedFlask = flask;

            if (flask.GetValue() <= flask.minValue)
            {
                Debug.Log("empty");
                selectedFlask = null;
                return false;
            }

            return false;
        }

        else if (flask) //second selection
        {
            if (flask.GetValue() >= flask.maxValue)
            {
                Debug.Log("too full");
                selectedFlask = null;
                return false;
            }

            selectedFlask.targetValue -= pourAmount;
            flask.targetValue += pourAmount;


            selectedFlask = null;

        }

        selectedFlask = null;
        checkFlask();
        return false;
    }

    Flask GetFlask(GameObject flaskObj)
    {
        foreach (Flask flask in flasks)
        {
            if (flask.gameObject == flaskObj) return flask;
        }
        return null;
    }

    void checkFlask()
    {
        bool solved = true;
        for (int i = 0; i < flasks.Length; i++)
        {
            if (flasks[i].targetValue != flaskTargets[i])
            {
                solved = false;
            }
        }
        if (solved)
        {
            Debug.Log("correct");
            paper.SetActive(true);
            //FindObjectOfType<GameHandler>().ObjectTriggered(this);
        }
    }
}
