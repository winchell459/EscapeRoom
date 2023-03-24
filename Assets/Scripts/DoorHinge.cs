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
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else if (Vector3.Distance(player.position, transform.position) < nearbyRange)
            {
                anim.SetBool("character_nearby", true);
            }
            else
            {
                anim.SetBool("character_nearby", false);
            }
        }
        
    }

    public void Unlock()
    {
        anim.SetBool("character_nearby", true);
        Destroy(doorCollider);
    }
}
