using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public GameObject waterParticles;
    public GameObject fruit;
    public GameObject plant;

    public void Water()
    {
        waterParticles.SetActive(true);
        waterParticles.transform.parent = null;
        Destroy(plant.GetComponent<BoxCollider>());
        StartCoroutine(wait());
        fruit.SetActive(true);
        Destroy(gameObject);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(6);
        
    }
}
