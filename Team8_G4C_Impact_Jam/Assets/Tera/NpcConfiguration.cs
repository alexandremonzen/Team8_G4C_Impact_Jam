using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcConfiguration : MonoBehaviour, IInteractable
{
    [Header("NPC Info")]
    [SerializeField] private string _name;

    [Header("Quest")]
    [SerializeField] private ItemType _requiredItem;
    [SerializeField] private GameObject _itemToSpawn;

    [Header("UI")]
    [SerializeField] private Dialogue _activeDialogue;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private TextMeshProUGUI _titleUI;
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private Image _titleImageUI;
    [SerializeField] private Sprite[] _titleImages;
    [SerializeField] private TMP_FontAsset[] _fonts;

    [SerializeField] private bool _isTranslated;
    private bool _inDialogue;
    private bool _active;

    private void Awake()
    {
        _inDialogue = false;
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        playerInteraction.PlayerMovement.RemoveAllMovement();
        if (!_inDialogue)
        {
            StartDialogue();
            return;
        }

        if (_inDialogue && !_active)
        {
            switch (_activeDialogue.AfterOption)
            {
                case Dialogue.AfterD.End:
                    _inDialogue = false;
                    playerInteraction.PlayerMovement.ReturnAllMovement();
                    _dialogueUI.GetComponent<Animator>().SetBool("Active", _inDialogue);
                    break;

                case Dialogue.AfterD.EndReplace:
                    _inDialogue = false;
                    if(_isTranslated)
                        _activeDialogue = _activeDialogue.ReplaceDialogue;
                    _dialogueUI.GetComponent<Animator>().SetBool("Active", _inDialogue);
                    playerInteraction.PlayerMovement.ReturnAllMovement();
                    break;

                case Dialogue.AfterD.NextReplace:
                    if (_isTranslated)
                    {
                        _activeDialogue = _activeDialogue.ReplaceDialogue;
                        StartDialogue();
                    }
                    else
                        _inDialogue = false;
                    break;

                case Dialogue.AfterD.NextReplaceIfComplete:
                    if (playerInteraction.PlayerHoldItem.ActualHoldingItem && _isTranslated)
                    {
                        if (playerInteraction.PlayerHoldItem.ActualHoldingItem.ItemType == _requiredItem)
                        {
                            _activeDialogue = _activeDialogue.ReplaceDialogue;
                            playerInteraction.PlayerHoldItem.DisappearActualItem();
                            StartDialogue();
                        }
                        else
                        {
                            _inDialogue = false;
                            playerInteraction.PlayerMovement.ReturnAllMovement();
                            _dialogueUI.GetComponent<Animator>().SetBool("Active", _inDialogue);
                        }
                    }
                    else
                    {
                        _inDialogue = false;
                        _dialogueUI.GetComponent<Animator>().SetBool("Active", _inDialogue);
                        playerInteraction.PlayerMovement.ReturnAllMovement();
                    }
                    break;
            }

            return;
        }

        if (_active)
        {
            StopAllCoroutines();
            _active = false;
            _textUI.text = _activeDialogue.Text;
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _active = true;
        char[] array = sentence.ToCharArray();
        _textUI.text = array[0].ToString();
        for (int i = 1; i < array.Length; ++i)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            _textUI.text += array[i];
            yield return null;
        }
        _active = false;
        yield return null;
    }

    public void StartDialogue()
    {
        _inDialogue = true;
        _dialogueUI.GetComponent<Animator>().SetBool("Active", _inDialogue);
        switch (_activeDialogue.Name)
        {
            case Dialogue.Character.Player:
                _titleUI.text = "YOU";
                _titleImageUI.sprite = _titleImages[0];
                _textUI.font = _fonts[0];
                break;
            case Dialogue.Character.Cat:
                _titleUI.text = _name;
                _titleImageUI.sprite = _titleImages[1];
                if (!_isTranslated)
                    _textUI.font = _fonts[1];
                else
                    _textUI.font = _fonts[0];
                break;
            case Dialogue.Character.Dog:
                _titleUI.text = _name;
                _titleImageUI.sprite = _titleImages[2];
                if (!_isTranslated)
                    _textUI.font = _fonts[2];
                else
                    _textUI.font = _fonts[0];
                break;
        }
        if (_activeDialogue.ActivateObject)
            _itemToSpawn.SetActive(true);

        StartCoroutine(TypeSentence(_activeDialogue.Text));
    }
}
