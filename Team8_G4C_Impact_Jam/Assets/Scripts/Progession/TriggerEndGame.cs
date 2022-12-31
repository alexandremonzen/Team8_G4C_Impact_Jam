using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class TriggerEndGame : MonoBehaviour
{
    private FadeController _fadeController;
    private ChangeScene _changeScene;

    private void Awake()
    {
        _fadeController = FadeController.Instance;
        _changeScene = GetComponent<ChangeScene>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerMovement playerMovement = col.GetComponent<PlayerMovement>();
        if(playerMovement)
        {
            playerMovement.RemoveAllMovement();
            StartCoroutine(StartEndGame());
        }
    }

    private IEnumerator StartEndGame()
    {
        _fadeController.FadeFunctionMethod(3, FadeType.In);
        yield return new WaitForSeconds(3);

        _changeScene.TrocarScene("EndCutscene");
    }
}
