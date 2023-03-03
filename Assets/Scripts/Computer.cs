using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public GameObject inputField;
    public GameObject computer;

    public void ComputerTurnOn()
    {
        Debug.Log("Computer turned on");
        Destroy(computer.GetComponent<BoxCollider>());
        inputField.SetActive(true);
    }
}
