using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_TempMove_Mara : MonoBehaviour
{
  public float speed = 5.0f;
  // Update is called once per frame
  void FixedUpdate()
  {
    transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
    transform.position += Vector3.up * Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
  }
}
