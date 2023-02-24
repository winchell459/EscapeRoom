using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public GameObject waterParticles;

    public void Water()
    {
        waterParticles.SetActive(true);
    }
}
