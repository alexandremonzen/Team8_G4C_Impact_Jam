using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_InteractiveHolder_Mara : Cs_InteractiveObject_Mara
{
  public Sprite closedSprite;
  public Sprite openedSprite;

  public SpriteRenderer spriteRenderer;

  public bool opened = false;
  public bool canClose = false;

  void Start()
  {
    if (null != spriteRenderer)
    {
      spriteRenderer.sprite = opened ? openedSprite : closedSprite;
    }
  }

  protected override void Interacted(GameObject pl)
  {
    if (!opened || canClose)
    {
      opened = !opened;
    }

    if (null != spriteRenderer)
    {
      spriteRenderer.sprite = opened ? openedSprite : closedSprite;
    }
  }
}
