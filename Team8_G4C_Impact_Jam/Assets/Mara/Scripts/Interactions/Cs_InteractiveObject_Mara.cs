using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_InteractiveObject_Mara : MonoBehaviour
{
  public SpriteRenderer interactButtonIcon = null;

  protected GameObject player;
  protected bool isColliding;

  public bool debugUpdate = false;

  protected virtual void Update()
  {
    if (null != player && null != interactButtonIcon)
    {
      interactButtonIcon.transform.position = player.transform.position + new Vector3(0.0f, 2.25f, 0.0f);
    }

    if (Input.GetButtonDown("Interact"))
    {
      if (isColliding)
      {
        Interacted(player);
      }
    }

    if (debugUpdate)
    {
      Debug.Log("Parent Update");
    }
  }

  protected virtual void Interacted(GameObject pl) { }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      player = other.gameObject;
      isColliding = true;

      if (null != interactButtonIcon)
      {
        interactButtonIcon.enabled = true;
        interactButtonIcon.transform.position = player.transform.position + new Vector3(0.0f, 2.25f, 0.0f);
      }
    }
  }
  void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag == "Player" && other.gameObject == player)
    {
      player = null;
      isColliding = false;

      if (null != interactButtonIcon)
      {
        interactButtonIcon.enabled = false;
      }
    }
  }
}
