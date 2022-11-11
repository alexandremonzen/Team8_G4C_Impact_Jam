using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendToMainMenu_Mara : MonoBehaviour
{
  void Start()
  {
    SceneManager.LoadScene("UI_Test");
  }
}
