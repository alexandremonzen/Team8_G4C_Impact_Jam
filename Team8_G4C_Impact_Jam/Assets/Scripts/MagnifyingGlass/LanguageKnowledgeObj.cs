using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LanguageKnowledgeObj : MonoBehaviour
{
    [SerializeField] private LanguageKnowledgeType _languageKnowledgeType;
    private MagnifyingGlassController _magnifyingGlassController;

    private void Update()
    {
        _magnifyingGlassController = MagnifyingGlassController.Instance;
    }

    private void OnMouseEnter()
    {
        if (_magnifyingGlassController.IsUsingMagnifyingGlass)
        {
            _magnifyingGlassController.SetCursorSelectedFeedback(true);
        }
    }

    private void OnMouseExit()
    {
        if (_magnifyingGlassController.IsUsingMagnifyingGlass)
        {
            _magnifyingGlassController.SetCursorSelectedFeedback(false);
        }
    }

    private void OnMouseDown()
    {
        if (_magnifyingGlassController.IsUsingMagnifyingGlass)
        {
            _magnifyingGlassController.AddKnowledgmentType(_languageKnowledgeType);
        }
    }
}
