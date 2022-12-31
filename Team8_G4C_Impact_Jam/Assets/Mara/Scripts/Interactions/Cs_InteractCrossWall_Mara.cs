using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_InteractCrossWall_Mara : Cs_InteractiveObject_Mara
{
  public Cs_InteractCrossWall_Mara otherEnd;
  
  public float speed = 10.0f;
  public float archRad = 2.0f;
  float realArchRad = 0.0f;

  Vector3 center = new Vector3(0.0f, 0.0f, 0.0f);
  float startAngle = 0.0f;
  float endAngle = 0.0f;
  float angleDist = 0.0f;
  float progressSpeed = 10.0f;

  bool crossing = false;
  GameObject crossingPlayer;
  float progress = 0.0f;

  void Start()
  {
    Vector3 thisPos = transform.position;
    Vector3 endPos = otherEnd.transform.position;

    Vector3 dir = endPos - thisPos;
    float dist = dir.magnitude * 0.5f;
    dir = dir.normalized;
    Vector3 norm = new Vector3(dir.y, -dir.x, 0.0f) * Mathf.Sign(dir.x);

    center = endPos + thisPos;
    center *= 0.5f;
    center += norm * archRad;
    realArchRad = Mathf.Sqrt(archRad * archRad + dist * dist);

    Vector3 startDir = thisPos - center;
    startAngle = Mathf.Atan2(startDir.y, startDir.x);
    Vector3 endDir = endPos - center;
    endAngle = Mathf.Atan2(endDir.y, endDir.x);

    angleDist = endAngle - startAngle;
    progressSpeed = speed / Mathf.Abs(angleDist * realArchRad);
  }

  void FixedUpdate()
  {
    if (crossing)
    {
      progress += progressSpeed * Time.fixedDeltaTime;
      //crossingPlayer.transform.position = transform.position + dir * dist * progress;
      float currentAngle = startAngle + angleDist * progress;
      crossingPlayer.transform.position = center + new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0.0f) * realArchRad;

      Debug.Log(progress);

      if (progress >= 1.0f)
      {
        crossing = false;
        var collider = crossingPlayer.GetComponent<BoxCollider2D>();
        if (null != collider)
        {
          collider.enabled = true;
        }
        var plMov = crossingPlayer.GetComponent<PlayerMovement>();
        if (null != plMov)
        {
          plMov.enabled = true;
        }
        crossingPlayer = null;
      }
    }
  }

  protected override void Interacted(GameObject pl)
  {
    if (null != otherEnd && null != pl)
    {
      crossing = true;
      crossingPlayer = pl;
      progress = 0.0f;

      var collider = crossingPlayer.GetComponent<BoxCollider2D>();
      if (null != collider)
      {
        collider.enabled = false;
      }
      var plMov = crossingPlayer.GetComponent<PlayerMovement>();
      if (null != plMov)
      {
        plMov.enabled = false;
      }
    }
  }
}