using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public Transform pointer;
    public float reachDistance = 5;
    public float holdingDistance = 2;
    private Transform holdingObject;
    private GameObject item;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (holdingObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(pointer.position, pointer.forward, out hit, reachDistance))
                {
                    if (hit.transform.CompareTag("Grabable"))
                    {
                        holdingObject = hit.transform;
                        holdingObject.position = pointer.forward * holdingDistance + pointer.position;
                        holdingObject.parent = pointer;
                        
                        if (holdingObject.name != "key"){
                            
                            Destroy(holdingObject.GetComponent<Rigidbody>());
                        }
                        else
                        {
                            item = holdingObject.gameObject;
                            holdingObject = null;
                            Destroy(item.GetComponent<BoxCollider>());
                            Debug.Log(item);
                        }
                        
                        
                    }
                    else if (hit.transform.CompareTag("Book"))
                    {
                        if (FindObjectOfType<BookShelf>().BookSelected(hit.transform.gameObject))
                            holdingObject = hit.transform;
                    }
                    else if (hit.transform.CompareTag("InputField"))
                    {
                        Debug.Log("InputField Found");
                        hit.transform.GetComponent<UnityEngine.UI.InputField>().Select();
                    }

                    else if (hit.transform.CompareTag("Button"))
                    {
                        Debug.Log("Button Found");
                        hit.transform.GetComponent<UnityEngine.UI.Button>().Select();
                        hit.transform.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                    }
                    else if (hit.transform.CompareTag("Flask"))
                    {
                        if (FindObjectOfType<FlaskStand>().FlaskSelected(hit.transform.gameObject))
                            holdingObject = hit.transform;
                    }
                    else if (hit.transform.CompareTag("Statue"))
                    {
                        hit.transform.gameObject.SendMessage("Selected");
                    }
                    else if (hit.transform.CompareTag("Locked"))
                    {
                        hit.transform.gameObject.SendMessage("Locked");
                    }
                    else if (hit.transform.CompareTag("Door"))
                    {
                        if(item && item.name == "key")
                        {
                            hit.transform.gameObject.SendMessage("Unlock");
                            Destroy(item);
                        }
                    }
                }
            }
            else
            {
                /**
                RaycastHit hit;
                if (Physics.Raycast(pointer.position, pointer.forward, out hit, reachDistance))
                {
                    if (hit.transform.CompareTag("Door"))
                    {
                        if (item.name == "key")
                        {
                            Debug.Log("key door");
                        }
                    }
                }
                **/
                if(item.name == "key")
                {
                    //Debug.Log("key door");
                    Destroy(item.GetComponent<BoxCollider>());
                    RaycastHit hitt;
                    if (Physics.Raycast(pointer.position, pointer.forward, out hitt, reachDistance))
                    {
                        if (hitt.transform.CompareTag("Door"))
                        {
                            Debug.Log("key door");
                            Destroy(item);
                            hitt.transform.gameObject.SendMessage("Unlock");
                        }
                    }
                }
                       

                else if (holdingObject.CompareTag("Grabable"))
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
