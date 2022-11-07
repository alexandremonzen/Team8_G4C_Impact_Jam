using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralButtonsUI : MonoBehaviour
{

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void RestartScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
