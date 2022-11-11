using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    public GameObject vaseExplosion;
    public GameObject hiddenObject;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<Hammer>())
        {
            vaseExplosion.SetActive(true);
            vaseExplosion.transform.parent = null;
            if(hiddenObject) hiddenObject.SetActive(true);

            Destroy(gameObject);

        }
    }
}
