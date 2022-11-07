using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    [Header("Canva Prefab")]
    [SerializeField] private GameObject _pauseMenuCanvas;

    [Header("Pages")]
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject[] _otherMenus;
    
    private bool _paused;

    public event Action PauseGameAction;
    public event Action UnPauseGameAction;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        _paused = false;
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        _playerInputActions.General.Enable();

        _playerInputActions.General.Pause.Enable();
        _playerInputActions.General.Pause.performed += PauseOrUnpauseGame;

        PauseGameAction += PauseGame;
        UnPauseGameAction += UnpauseGame;
    }

    private void OnDisable()
    {
        _playerInputActions.General.Disable();

        _playerInputActions.General.Pause.Disable();
        _playerInputActions.General.Pause.performed -= PauseOrUnpauseGame;

        PauseGameAction -= PauseGame;
        UnPauseGameAction -= UnpauseGame;
    }

    private void DeactivateOtherPages()
    {
        foreach(GameObject gameObject in _otherMenus)
        {
            gameObject.SetActive(false);
        }
    }

    private void PauseOrUnpauseGame(InputAction.CallbackContext obj)
    {
        if (!_paused)
        {
            PauseGameAction();
        }
        else
        {
            UnPauseGameAction();
        }
    }

    public void ReturnGameButtonUI()
    {
        UnPauseGameAction();
    }

    public void PauseGame()
    {
        _paused = true;
        _pauseMenuCanvas.SetActive(true);
        _mainScreen.SetActive(true);

        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        _paused = false;
        _pauseMenuCanvas.SetActive(false);
        DeactivateOtherPages();

        Time.timeScale = 1;
    }
}