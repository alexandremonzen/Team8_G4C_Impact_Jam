using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcConfiguration : MonoBehaviour
{
    public string Name;
    public bool IsTranslated;
    public bool HasItem;
    private bool InDialogue;
    private bool Active;
    public Dialogue ActiveDialogue;
    public GameObject DialogueUI;
    public Sprite[] TitleImages;
    public TextMeshProUGUI TitleUI;
    public TextMeshProUGUI TextUI;
    public Image TitleImageUI;
    public GameObject Item;

    void Update()
    {
        DialogueUI.GetComponent<Animator>().SetBool("Active", InDialogue);
        if (Physics2D.OverlapCircle(transform.position, 3).CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !InDialogue)
            StartDialogue();

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)) && InDialogue && !Active)
            switch (ActiveDialogue.AfterOption)
            {
                case Dialogue.AfterD.End:
                    InDialogue = false;
                    break;
                case Dialogue.AfterD.EndReplace:
                    InDialogue = false;
                    ActiveDialogue = ActiveDialogue.ReplaceDialogue;
                    break;
                case Dialogue.AfterD.NextReplace:
                    ActiveDialogue = ActiveDialogue.ReplaceDialogue;
                    StartDialogue();
                    break;
                case Dialogue.AfterD.NextReplaceIfComplete:
                    if (HasItem)
                    {
                        ActiveDialogue = ActiveDialogue.ReplaceDialogue;
                        StartDialogue();
                    }
                    else
                        InDialogue = false;
                    break;
            }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && Active)
            {
                StopAllCoroutines();
                Active = false;
                TextUI.text = ActiveDialogue.Text;
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        Active = true;
        char[] array = sentence.ToCharArray();
        TextUI.text = array[0].ToString();
        for (int i = 1; i < array.Length; ++i)
        {
            yield return new WaitForSeconds(0.02f);
            TextUI.text += array[i];
        }
        Active = false;

    }

    public void StartDialogue()
    {
        InDialogue = true;
        switch(ActiveDialogue.Name)
        {
            case Dialogue.Character.Player:
                TitleUI.text = "YOU";
                TitleImageUI.sprite = TitleImages[0];
                break;
            case Dialogue.Character.Cat:
                TitleUI.text = Name;
                TitleImageUI.sprite = TitleImages[1];
                break;
            case Dialogue.Character.Dog:
                TitleUI.text = Name;
                TitleImageUI.sprite = TitleImages[2];
                break;
        }
        if (ActiveDialogue.ActivateObject)
            Item.SetActive(true);
        StartCoroutine(TypeSentence(ActiveDialogue.Text));
    }
}
