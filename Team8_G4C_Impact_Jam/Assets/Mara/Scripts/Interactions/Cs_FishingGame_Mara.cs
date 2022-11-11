using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_FishingGame_Mara : MonoBehaviour
{
  GameObject player = null;
  bool gameStarted = false;

  public GameObject hookObj = null;
  public GameObject ropeObj = null;

  public GameObject exclamationMark = null;
  public SpriteRenderer pushButtonIcon = null;
  bool pushButtonActive = false;
  float pushButtonTime = 0.0f;
  public GameObject progressBar = null;
  public GameObject progressBarBackground = null;
  public float progressBarMaxSize = 1.0f;
  public Color progressBarMinColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
  public Color progressBarMaxColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);

  public Vector3 playerRelativePosition = new Vector3(0.0f, 0.0f, 0.0f);

  public Transform topLeftPondPos = null;
  public Transform bottomRightPondPos = null;

  Vector3 hookRandPos = new Vector3(0.0f, 0.0f, 0.0f);


  bool fishActive = false;
  bool hookOnWater = false;
  bool fishFighting = false;

  float timeElapsed = 0.0f;

  float timeTillFish = 0.0f;
  float minTimeTillFish = 3.0f;
  float maxTimeTillFish = 7.0f;

  float timeForFish = 0.0f;
  float minTimeForFish = 1.0f;
  float maxTimeForFish = 2.0f;

  float fishFightPower = 1.0f;
  int collectedFish = 0;

  bool actionPressed = false;


  void Update()
  {
    if (Input.GetButtonDown("Jump"))
    {
      actionPressed = true;
    }
    if (Input.GetButtonUp("Jump"))
    {
      actionPressed = false;
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (pushButtonActive)
    {
      pushButtonTime += Time.fixedDeltaTime;
      if (pushButtonTime >= 0.2f)
      {
        pushButtonTime -= 0.2f;
        if (null != pushButtonIcon)
        {
          pushButtonIcon.enabled = !pushButtonIcon.enabled;
        }
      }
    }
    else
    {
      if (null != pushButtonIcon)
      {
        pushButtonIcon.enabled = false;
      }
    }

    if (gameStarted)
    {
      if (hookOnWater)
      {
        if (fishFighting)
        {
          FightingFish();
          return;
        }

        timeElapsed += Time.fixedDeltaTime;

        if (actionPressed)
        {
          actionPressed = false;

          if (fishActive)
          {
            fishFighting = true;
            fishFightPower = 1.0f;
            pushButtonActive = true;
            if (null != progressBar)
            {
              SpriteRenderer progressBarSprite = progressBar.GetComponent<SpriteRenderer>();
              if (null != progressBarSprite)
              {
                progressBarSprite.enabled = true;
              }
            }
            if (null != progressBarBackground)
            {
              SpriteRenderer progressBarBackSprite = progressBarBackground.GetComponent<SpriteRenderer>();
              if (null != progressBarBackSprite)
              {
                progressBarBackSprite.enabled = true;
              }
            }
          }
          else
          {
            RetrieveHook();
          }
        }

        if (fishActive)
        {
          if (timeElapsed >= timeForFish)
          {
            fishActive = false;
            timeTillFish = Random.Range(minTimeTillFish, maxTimeTillFish);
            timeElapsed = 0.0f;
            if (null != exclamationMark)
            {
              SpriteRenderer exclamationSprite = exclamationMark.GetComponent<SpriteRenderer>();
              if (null != exclamationSprite)
              {
                exclamationSprite.enabled = false;
              }
            }
          }
        }
        else
        {
          if (timeElapsed >= timeTillFish)
          {
            fishActive = true;
            timeElapsed = 0.0f;
            timeForFish = Random.Range(minTimeForFish, maxTimeForFish);
            if (null != exclamationMark)
            {
              SpriteRenderer exclamationSprite = exclamationMark.GetComponent<SpriteRenderer>();
              if (null != exclamationSprite)
              {
                exclamationSprite.enabled = true;
              }

              exclamationMark.transform.position = hookRandPos + new Vector3(1.0f, 0.0f, 0.0f);
            }
          }
        }
      }
      else
      {
        if (actionPressed)
        {
          actionPressed = false;
          LaunchHook();
        }
      }
    }
  }

  void LaunchHook()
  {
    hookOnWater = true;
    timeTillFish = Random.Range(minTimeTillFish, maxTimeTillFish);
    timeElapsed = 0.0f;

    if (null != topLeftPondPos && null != bottomRightPondPos)
    {
      float safeZoneSize = 0.5f;
      Vector3 tlp = topLeftPondPos.position;
      Vector3 brp = bottomRightPondPos.position;
      Vector3 rtlp = new Vector3(Mathf.Min(tlp.x, brp.x) + safeZoneSize, Mathf.Min(tlp.y, brp.y) + safeZoneSize, -0.1f);
      Vector3 rbrp = new Vector3(Mathf.Max(tlp.x, brp.x) - safeZoneSize, Mathf.Max(tlp.y, brp.y) - safeZoneSize, -0.1f);

      float pondWidth = rbrp.x - rtlp.x;
      float pondHeight = rbrp.y - rtlp.y;
      hookRandPos = new Vector3(rtlp.x + Random.value * pondWidth, rtlp.y + Random.value * pondHeight, 0.0f);

      if (null != hookObj)
      {
        hookObj.transform.position = hookRandPos;

        SpriteRenderer hookSprite = hookObj.GetComponent<SpriteRenderer>();
        if (null != hookSprite)
        {
          hookSprite.enabled = true;
        }
      }
      if (null != ropeObj)
      {
        Vector3 plPos = player.transform.position;
        Vector3 plDir = hookRandPos - plPos;
        float angle = Mathf.Atan2(plDir.y, plDir.x) * Mathf.Rad2Deg;
        Vector3 midPos = (plPos + hookRandPos) * 0.5f;
        ropeObj.transform.position = midPos;
        ropeObj.transform.rotation = Quaternion.Euler(0, 0, angle);
        ropeObj.transform.localScale = new Vector3(plDir.magnitude, 0.05f, 1.0f);

        SpriteRenderer ropeSprite = ropeObj.GetComponent<SpriteRenderer>();
        if (null != ropeSprite)
        {
          ropeSprite.enabled = true;
        }
      }
    }
  }

  void RetrieveHook()
  {
    hookOnWater = false;

    if (null != hookObj)
    {
      SpriteRenderer hookSprite = hookObj.GetComponent<SpriteRenderer>();
      if (null != hookSprite)
      {
        hookSprite.enabled = false;
      }
    }
    if (null != ropeObj)
    {
      SpriteRenderer ropeSprite = ropeObj.GetComponent<SpriteRenderer>();
      if (null != ropeSprite)
      {
        ropeSprite.enabled = false;
      }
    }
  }

  void FightingFish()
  {
    fishFightPower -= Time.fixedDeltaTime;

    if (fishFightPower <= 0.0f)
    {
      fishFighting = false;
      fishActive = false;
      timeElapsed = 0.0f;
      RetrieveHook();
      pushButtonActive = false;
      if (null != exclamationMark)
      {
        SpriteRenderer exclamationSprite = exclamationMark.GetComponent<SpriteRenderer>();
        if (null != exclamationSprite)
        {
          exclamationSprite.enabled = false;
        }
      }
      if (null != progressBar)
      {
        SpriteRenderer progressBarSprite = progressBar.GetComponent<SpriteRenderer>();
        if (null != progressBarSprite)
        {
          progressBarSprite.enabled = false;
        }
      }
      if (null != progressBarBackground)
      {
        SpriteRenderer progressBarBackSprite = progressBarBackground.GetComponent<SpriteRenderer>();
        if (null != progressBarBackSprite)
        {
          progressBarBackSprite.enabled = false;
        }
      }
      return;
    }
    if (fishFightPower >= 3.0f)
    {
      ++collectedFish;
      fishFighting = false;
      fishActive = false;
      timeElapsed = 0.0f;
      RetrieveHook();
      StopGame();
      pushButtonActive = false;
      if (null != exclamationMark)
      {
        SpriteRenderer exclamationSprite = exclamationMark.GetComponent<SpriteRenderer>();
        if (null != exclamationSprite)
        {
          exclamationSprite.enabled = false;
        }
      }
      if (null != progressBar)
      {
        SpriteRenderer progressBarSprite = progressBar.GetComponent<SpriteRenderer>();
        if (null != progressBarSprite)
        {
          progressBarSprite.enabled = false;
        }
      }
      if (null != progressBarBackground)
      {
        SpriteRenderer progressBarBackSprite = progressBarBackground.GetComponent<SpriteRenderer>();
        if (null != progressBarBackSprite)
        {
          progressBarBackSprite.enabled = false;
        }
      }
      return;
    }

    if (actionPressed)
    {
      actionPressed = false;
      fishFightPower += 0.2f;
    }

    float progress = fishFightPower / 3.0f;
    Color barColor = progressBarMinColor * Mathf.Cos(progress * Mathf.PI * 0.5f) + progressBarMaxColor * Mathf.Sin(progress * Mathf.PI * 0.5f);
    if (null != progressBar)
    {
      SpriteRenderer progressBarSprite = progressBar.GetComponent<SpriteRenderer>();
      if (null != progressBarSprite)
      {
        progressBarSprite.color = barColor;
      }
      progressBar.transform.position = progressBarBackground.transform.position - new Vector3(progressBarMaxSize * (1.0f - progress) * 0.5f, 0.0f, 0.0f);
      progressBar.transform.localScale = new Vector3(progressBarMaxSize * progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
    }
  }

  public bool GameIsStarted()
  {
    return gameStarted;
  }

  void Restart()
  {
    fishActive = false;
    hookOnWater = false;
    fishFighting = false;

    timeElapsed = 0.0f;

    timeTillFish = 0.0f;

    timeForFish = 0.0f;

    fishFightPower = 1.0f;
    collectedFish = 0;

    actionPressed = false;

    RetrieveHook();
  }

  public void StartGame(GameObject pl)
  {
    if ((!gameStarted || null == player) && null != pl)
    {
      player = pl;
      gameStarted = true;
      player.transform.position = gameObject.transform.position + new Vector3(playerRelativePosition.x, playerRelativePosition.y, 0.0f);
      Restart();
      var plMov = player.GetComponent<PlayerMovement>();
      if (null != plMov)
      {
        plMov.enabled = false;
      }

      Debug.Log("Game Started");
    }
  }

  public void StopGame()
  {
    if (gameStarted)
    {
      gameStarted = false;
      var plMov = player.GetComponent<PlayerMovement>();
      if (null != plMov)
      {
        plMov.enabled = true;
      }
      player = null;

      Debug.Log("Game Stoped");
    }
  }
}
