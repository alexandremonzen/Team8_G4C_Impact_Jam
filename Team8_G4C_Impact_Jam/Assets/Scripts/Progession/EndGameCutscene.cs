using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class EndGameCutscene : MonoBehaviour
{
    [Header("NPCs")]
    [SerializeField] private NpcConfiguration _apollo;
    [SerializeField] private NpcConfiguration _monchis;
    [SerializeField] private NpcConfiguration _agatha;
    [SerializeField] private NpcConfiguration _pinguica;
    [SerializeField] private NpcConfiguration _misty;
    [SerializeField] private NpcConfiguration _nininha;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _pickaxesClip;
    [SerializeField] private AudioClip _wallDestroyClip;
    private AudioManager _audioManager;

    private FadeController _fadeController;

    private void Awake()
    {
        _fadeController = FadeController.Instance;
        _audioManager = AudioManager.Instance;
    }

    private void Start()
    {
        _fadeController.FadeFunctionMethod(3, FadeType.Out);
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2);
        _apollo.StartDialogue();

        yield return new WaitForSeconds(1);
        _agatha.StartDialogue();

        yield return new WaitForSeconds(0.5f);
        _monchis.StartDialogue();

        yield return new WaitForSeconds(1);
        _nininha.StartDialogue();

        yield return new WaitForSeconds(0.5f);
        _misty.StartDialogue();

        yield return new WaitForSeconds(1.5f);
        _pinguica.StartDialogue();

        yield return new WaitForSeconds(1.5f);
        _fadeController.FadeFunctionMethod(3, FadeType.In);
        
        yield return new WaitForSeconds(3f);

        _audioManager.PlayAudio2D(_pickaxesClip[0]);
        yield return new WaitForSeconds(1.5f);
        _audioManager.PlayAudio2D(_pickaxesClip[1]);
        yield return new WaitForSeconds(1f);
        _audioManager.PlayAudio2D(_pickaxesClip[2]);
        yield return new WaitForSeconds(0.25f);
        _audioManager.PlayAudio2D(_wallDestroyClip);
        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene(0);

        yield return null;
    }
}
