using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHinge : MonoBehaviour
{
    public Animator anim;
    public float nearbyRange = 2;
    private Transform player;
    public bool locked = false;
    public BoxCollider doorCollider;

    private void Start()
    {
        anim.SetBool("character_nearby", false);
    }

    private void Update()
    {
        if (!locked)
        {
            
            bool characterNearby = false;
            foreach(GameObject character in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(Vector3.Distance(character.transform.position, transform.position) < nearbyRange)
                {
                    characterNearby = true;
                    break;
                }
            }

            anim.SetBool("character_nearby", characterNearby);

        }

    }

    public void Unlock()
    {
        anim.SetBool("character_nearby", true);
        Destroy(doorCollider);
    }
}
