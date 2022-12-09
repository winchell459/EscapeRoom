using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    public float openAngle, closedAngle;
    [SerializeField] bool open;
    public float velocity = 2;
    private float currentAngle;
    public Transform hinge;

    private void Start()
    {
        currentAngle = hinge.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (open && Mathf.Abs(openAngle - currentAngle) >= velocity * Time.deltaTime)
        {
            
            currentAngle += Mathf.Sign(openAngle - currentAngle) * velocity * Time.deltaTime;
            Debug.Log($"Opening {currentAngle}");
        }
        else if (open)
        {
            Debug.Log("Open");
            currentAngle = openAngle;
        }else if (!open && Mathf.Abs(closedAngle - currentAngle) >= velocity * Time.deltaTime)
        {
            Debug.Log("Closing");
            currentAngle += Mathf.Sign(closedAngle - currentAngle) * velocity * Time.deltaTime;
        }
        else if (!open)
        {
            Debug.Log("Closed");
            currentAngle = closedAngle;
        }
        hinge.localEulerAngles = new Vector3(hinge.localEulerAngles.x,currentAngle, hinge.localEulerAngles.z);
    }
}
