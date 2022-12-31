using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_InteractPipe_Mara : Cs_InteractiveObject_Mara
{
  public Cs_InteractPipe_Mara otherEnd;

  public Sprite closedSprite;
  public Sprite openedSprite;

  public SpriteRenderer spriteRenderer;

  public bool opened = false;

  void Start()
  {
    if (null != spriteRenderer)
    {
      spriteRenderer.sprite = opened ? openedSprite : closedSprite;
    }
  }

  protected override void Interacted(GameObject pl)
  {
    if (!opened)
    {
      opened = true;
      if (null != spriteRenderer)
      {
        spriteRenderer.sprite = openedSprite;
      }
      return;
    }

    if (otherEnd.opened && null != pl)
    {
      pl.transform.position = otherEnd.transform.position;
    }

  }
}
