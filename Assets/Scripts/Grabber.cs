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
    public EscapeNetwork.EscapeNetworkPlayer networkPlayer;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (holdingObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(pointer.position, pointer.forward, out hit, reachDistance, layerMask))
                {
                    if (networkPlayer) networkPlayer.SubmitGrabberObjectClick(hit.transform.gameObject);
                    ObjectClicked(hit.transform, ref holdingObject, ref item, pointer);
                }
            }
            else
            {
                if (networkPlayer) networkPlayer.SubmitGrabberObjectClick(null);
                AlreadyHoldingObject(ref holdingObject, ref item, pointer);
            }
        }
        
    }

    public static void AlreadyHoldingObject(ref Transform holdingObject, ref GameObject item, Transform pointer)
    {
        if (item && item.name == "key")
        {
            Debug.Log("key door");
            Destroy(item.GetComponent<BoxCollider>());
            RaycastHit hitt;
            if (Physics.Raycast(pointer.position, pointer.forward, out hitt, FindObjectOfType<Grabber>().reachDistance, FindObjectOfType<Grabber>().layerMask))
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

        holdingObject = null;
    }
    public static void ObjectClicked(Transform clicked, ref Transform holdingObject, ref GameObject item, Transform pointer)
    {
        if (clicked.CompareTag("Grabable"))
        {
            holdingObject = clicked;
            holdingObject.position = pointer.forward * FindObjectOfType<Grabber>().holdingDistance + pointer.position;
            holdingObject.parent = pointer;
            if (holdingObject.name != "key")
            {
                if(holdingObject.GetComponent<Rigidbody>())
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
        else if (clicked.CompareTag("Book"))
        {
            if (FindObjectOfType<BookShelf>().BookSelected(clicked.gameObject))
                holdingObject = clicked;
        }
        else if (clicked.CompareTag("InputField"))
        {
            Debug.Log("InputField Found");
            clicked.GetComponent<UnityEngine.UI.InputField>().Select();
        }

        else if (clicked.CompareTag("Button"))
        {
            Debug.Log("Button Found");
            clicked.GetComponent<UnityEngine.UI.Button>().Select();
            clicked.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
        else if (clicked.CompareTag("Flask"))
        {
            if (FindObjectOfType<FlaskStand>().FlaskSelected(clicked.gameObject))
                holdingObject = clicked;
        }
        else if (clicked.CompareTag("Statue"))
        {
            clicked.gameObject.SendMessage("Selected");
        }
        else if (clicked.CompareTag("Selectable"))
        {
            clicked.gameObject.SendMessage("Selected");
        }
        else if (clicked.CompareTag("Locked"))
        {
            clicked.gameObject.SendMessage("Locked");
        }
        else if (clicked.CompareTag("Door"))
        {
            if (item && item.name == "key")
            {
                clicked.gameObject.SendMessage("Unlock");
                Destroy(item);
            }
        }
        else if (clicked.CompareTag("Clock"))
        {
            FindObjectOfType<Clock>().HandClicked(clicked.gameObject);
        }
    }
}
