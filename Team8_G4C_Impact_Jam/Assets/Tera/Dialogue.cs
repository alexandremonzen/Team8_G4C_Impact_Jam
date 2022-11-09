using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueFile/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public enum Character
    {
        [Tooltip("Its the player talking")]Player,
        [Tooltip("Its a cat talking")] Cat,
        [Tooltip("Its a dog talking")] Dog,
    }

    [Header("Dialogue Info")]
    [Tooltip("Who is talking")]public Character Name;
    [Tooltip("Text of the dialogue")][TextArea]public string Text;
    [Tooltip("Visual representation of the text(type of gif, frame by frame)")] public Sprite[] Animation;

    public enum AfterD
    {
        [Tooltip("End the dialogue")]End,
        [Tooltip("Plays the Replace dialogue")] NextReplace,
        [Tooltip("Plays the Replace dialogue if the player gives the item, if not it will just end the dialogue")] NextReplaceIfComplete,
        [Tooltip("End the dialogue and replace it for the next interaction")] EndReplace
    }
    [Header("End Dialogue Settings")]
    [Tooltip("What happens when you finish this dialogue")]public AfterD AfterOption;
    [Tooltip("Replace dialogue file if you have one")] public Dialogue ReplaceDialogue;
    [Tooltip("Make the object that the npc is holding appear on the map")]public bool ActivateObject;
}
