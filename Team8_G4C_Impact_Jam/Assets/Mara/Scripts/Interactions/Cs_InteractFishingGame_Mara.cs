using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_InteractFishingGame_Mara : Cs_InteractiveObject_Mara
{
  public Cs_FishingGame_Mara game;

  protected override void Interacted(GameObject pl)
  {
    if (null != game)
    {
      if (game.GameIsStarted())
      {
        if (null != interactButtonIcon)
        {
          interactButtonIcon.enabled = true;
        }
        game.StopGame();
      }
      else
      {
        if (null != interactButtonIcon)
        {
          interactButtonIcon.enabled = false;
        }
        game.StartGame(pl);
      }
    }
  }
}
