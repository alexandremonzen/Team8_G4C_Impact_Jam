using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_InteractiveObject_Mara : MonoBehaviour
{
    protected GameObject player;
    protected bool isColliding;

    public bool debugUpdate = false;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (isColliding)
            {
                Interacted(player);
            }
        }
    }

    protected virtual void Interacted(GameObject pl) {}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            isColliding = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject == player)
        {
            player = null;
            isColliding = false;
        }
    }
}
