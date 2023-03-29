using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public GameObject inputField;
    public GameObject computer;
    public GameObject offScreen;
    public GameObject onScreen;
    public GameObject offHexPlatform;
    public GameObject hexPlatform;
    public GameObject offLinePlatform;
    public GameObject linePlatform;
    public GameObject offFruitPlatform;
    public GameObject fruitPlatform;
    public GameObject hexObject;
    public GameObject lineObject;

    public void ComputerTurnOn()
    {
        Debug.Log("Computer turned on");
        Destroy(computer.GetComponent<BoxCollider>());
        inputField.SetActive(true);
        offScreen.SetActive(false);
        onScreen.SetActive(true);
        
    }

    public void CorrectPassword()
    {
        offHexPlatform.SetActive(false);
        hexPlatform.SetActive(true);
        offLinePlatform.SetActive(false);
        linePlatform.SetActive(true);
        offFruitPlatform.SetActive(false);
        fruitPlatform.SetActive(true);
        hexObject.SetActive(true);
        lineObject.SetActive(true);
    }
}
