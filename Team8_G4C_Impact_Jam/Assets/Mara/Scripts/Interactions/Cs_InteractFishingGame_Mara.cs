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
      if (game.GameIsStarted()) game.StopGame();
      else game.StartGame(pl);
    }
  }
}
