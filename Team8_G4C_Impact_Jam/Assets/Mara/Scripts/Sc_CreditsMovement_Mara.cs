using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sc_CreditsMovement_Mara : MonoBehaviour
{
  public Image fadeInOut;
  public float creditsSpeed = 5.0f;
  public GameObject logo;
  public GameObject canvas;
  bool running = false;
  public float fadeInOutTime = 1.5f;
  public float waitTillFadeOutTime = 1.5f;
  float fadeInOutCurrentTime = 0.0f;
  bool fadeIn = true;
  bool fadeOut = false;
  // Update is called once per frame
  void Update()
  {
    if (running)
    {
      transform.position += new Vector3(0.0f, creditsSpeed * Time.deltaTime, 0.0f);

      if (logo.transform.position.y - canvas.transform.position.y >= 0.0f)
      {
        running = false;
        fadeOut = true;
      }
    }
    else
    {
      if (fadeIn)
      {
        fadeInOutCurrentTime += Time.deltaTime;
        fadeInOut.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - fadeInOutCurrentTime / fadeInOutTime);
        if (fadeInOutCurrentTime >= fadeInOutTime)
        {
          running = true;
          fadeIn = false;
          fadeInOutCurrentTime = 0.0f;
        }
      }
      if (fadeOut)
      {
        fadeInOutCurrentTime += Time.deltaTime;
        if (fadeInOutCurrentTime >= waitTillFadeOutTime)
        {
          fadeInOut.color = new Color(0.0f, 0.0f, 0.0f, (fadeInOutCurrentTime - waitTillFadeOutTime) / fadeInOutTime);
          if (fadeInOutCurrentTime >= fadeInOutTime + waitTillFadeOutTime)
          {
            fadeOut = false;
            fadeInOutCurrentTime = 0.0f;
            SceneManager.LoadScene("UI_Test");
          }
        }
      }
    }
  }
}
