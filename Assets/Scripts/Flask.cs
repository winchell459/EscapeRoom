using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    public Transform liquid;
    public float maxScale = 1.18f, maxValue = 300, minScale, minValue;
    public float targetValue = 100;
    public float fillRate = 10;
    

    // Update is called once per frame
    void Update()
    {
        float value = GetValue();
        if(value < targetValue && value + fillRate * Time.deltaTime < targetValue)
        {
            //add to the liquid
            SetScale(value + fillRate * Time.deltaTime);
        }
        else if(value > targetValue && value - fillRate * Time.deltaTime > targetValue)
        {
            //subtract the liquid
            SetScale(value - fillRate * Time.deltaTime);
        }
        else
        {
            SetScale(targetValue);
        }
    }
    void SetScale(float value)
    {
        float newScale = GetScale(value);
        liquid.localScale = new Vector3(liquid.localScale.x, newScale, liquid.localScale.z);
    }

    float GetScale(float value)
    {
        float percent = (value - minValue) / (maxValue - minValue);
        return percent * (maxScale - minScale) + minScale;
    }

    public float GetValue()
    {
        return GetValue(liquid.localScale.y);
    }

    float GetValue(float scale)
    {
        float percent = (scale - minScale) / (maxScale - minScale);
        return percent * (maxValue - minValue) + minValue;
    }

    public bool CheckValue(float value)
    {
        return targetValue == value && CheckValue();
    }
    bool CheckValue()
    {
        return GetValue() == targetValue;
    }
}
