using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{

    public Transform pointer;   
    public float reachDistance = 5;
    public float holdingDistance = 2;
    private Transform holdingObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(holdingObject == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(pointer.position, pointer.forward, out hit, reachDistance))
                {
                    if (hit.transform.CompareTag("Grabable"))
                    {
                        holdingObject = hit.transform;
                        holdingObject.position = pointer.forward * holdingDistance + pointer.position;
                        holdingObject.parent = pointer;
                        Destroy(holdingObject.GetComponent<Rigidbody>());
                    }

                    else if (hit.transform.CompareTag("Book"))
                    {
                        if (FindObjectOfType<BookShelf>().BookSelected(hit.transform.gameObject))
                        {
                            holdingObject = hit.transform;
                        }

                    }
                    else if (hit.transform.GetComponent<UnityEngine.UI.InputField>())
                    {
                        Debug.Log("InputField found");
                        hit.transform.GetComponent<UnityEngine.UI.InputField>().Select();
                    }

                    else if (hit.transform.CompareTag("Button"))
                    {
                        Debug.Log("Button found");
                        hit.transform.GetComponent<UnityEngine.UI.Button>().Select();
                        hit.transform.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                    }
                }

            }
            else
            {
                if(holdingObject.CompareTag("Grabable"))
                {
                    holdingObject.parent = null;
                    holdingObject.gameObject.AddComponent<Rigidbody>();
                }
                //stop bookshelf selection mode
                
                holdingObject = null;
            }
        }
    }
}
