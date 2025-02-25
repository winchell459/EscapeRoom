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
                if (Physics.Raycast(pointer.position, pointer.forward, out hit, reachDistance))
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

    public void ClearHolding()
    {
        if (holdingObject && networkPlayer)
        {
            Transform temp = holdingObject;
            AlreadyHoldingObject(ref holdingObject, ref item, pointer);
            Destroy(holdingObject);
        }
        
    }

    public static void AlreadyHoldingObject(ref Transform holdingObject, ref GameObject item, Transform pointer)
    {
        if (item && item.name == "key")
        {
            Debug.Log("key door");
            Destroy(item.GetComponent<BoxCollider>());
            RaycastHit hitt;
            if (Physics.Raycast(pointer.position, pointer.forward, out hitt, FindObjectOfType<Grabber>().reachDistance))
            {
                if (hitt.transform.CompareTag("Door"))
                {
                    Debug.Log("key door");
                    Destroy(item);
                    hitt.transform.gameObject.SendMessage("Unlock");
                }
            }
        }

        if (item && item.name == "watering can")
        {
            Destroy(item.GetComponent<BoxCollider>());
            RaycastHit hittt;
            if (Physics.Raycast(pointer.position, pointer.forward, out hittt, FindObjectOfType<Grabber>().reachDistance, FindObjectOfType<Grabber>().layerMask))
            {
                Debug.Log("watering can let go, hit smth");
                Debug.Log(hittt.transform.gameObject.tag);
                if (hittt.transform.CompareTag("Plant"))
                {
                    Debug.Log("plant hit");
                    Destroy(item);
                    pointer.gameObject.SendMessage("Water");
                    
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

    public static void ObjectClicked(Transform clicked, ref Transform holdingObject, ref GameObject item, Transform pointer)
    {
        if (clicked.CompareTag("Grabable"))
        {
            holdingObject = clicked;
            holdingObject.position = pointer.forward * FindObjectOfType<Grabber>().holdingDistance + pointer.position;
            holdingObject.parent = pointer;
            item = holdingObject.gameObject;
            if (holdingObject.name != "key" && holdingObject.name != "watering can")
            {
                Debug.Log(item);
                if (holdingObject.GetComponent<Rigidbody>())
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
            FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().movementPause = true;
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
        else if (clicked.CompareTag("Computer"))
        {
            clicked.gameObject.SendMessage("ComputerTurnOn");
        }
        else if (clicked.CompareTag("Plant"))
        {
            if (clicked.transform.CompareTag("Plant"))
            {
                Debug.Log("plant hit");
                holdingObject = null;
                item.SendMessage("Water");

            }
        }
        else if (clicked.CompareTag("Clock"))
        {
            clicked.parent.GetComponent<Clock>().HandClicked(clicked.gameObject);
        }
    }
}
