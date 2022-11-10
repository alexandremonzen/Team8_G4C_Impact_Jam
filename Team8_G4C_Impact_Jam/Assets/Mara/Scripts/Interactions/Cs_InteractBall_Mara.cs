using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_InteractBall_Mara : Cs_InteractiveObject_Mara
{
  public float pushForce = 10.0f;

  public Vector3 activeForce;

  public bool debugBallUpdate = false;

  void FixedUpdate()
  {
    transform.position += activeForce * Time.fixedDeltaTime;
    activeForce *= 1 - Time.fixedDeltaTime * 0.8f;

    if (activeForce.magnitude < 0.3f)
    {
      activeForce = new Vector3(0.0f, 0.0f, 0.0f);
    }
  }

  protected override void Interacted(GameObject pl)
  {
    if (null != pl)
    {
      Vector3 dir = transform.position - pl.transform.position;

      dir = dir.normalized * pushForce;

      activeForce = new Vector3(dir.x, dir.y, 0.0f);
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player") return;

    Vector2 midNormal = new Vector2(0.0f, 0.0f);
    foreach (var item in other.contacts)
    {
      midNormal += item.normal;
    }
    midNormal /= other.contacts.Length;

    float normAngle = Mathf.Atan2(midNormal.y, midNormal.x);
    float velAngle = Mathf.Atan2(activeForce.y, activeForce.x) + Mathf.PI;

    float angleDif = normAngle - velAngle;

    velAngle += angleDif * 2.0f;

    activeForce = new Vector3(Mathf.Cos(velAngle), Mathf.Sin(velAngle), 0.0f) * activeForce.magnitude;
  }
}
