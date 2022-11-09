using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MagnifyingGlassController : Singleton<MagnifyingGlassController>
{
    [Header("Player")]
    [SerializeField] private PlayerLanguageKnowledge _playerLanguageKnowledge;

    [Header("Icons & Images")]
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private Texture2D _normalMagnifyingGlassCursor;
    [SerializeField] private Texture2D _feedbackMagnifyingGlassCursor;

    private bool _isUsingMagnifyingGlass;

    public bool IsUsingMagnifyingGlass { get => _isUsingMagnifyingGlass; }

    private void Awake()
    {
        _isUsingMagnifyingGlass = false;
    }

    public void ChangeStatusMagnifyingGlass()
    {
        if (!_isUsingMagnifyingGlass)
        {
            Cursor.SetCursor(_normalMagnifyingGlassCursor, Vector2.zero, CursorMode.ForceSoftware);
            _isUsingMagnifyingGlass = true;
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            _isUsingMagnifyingGlass = false;
        }
    }

    public void SetCursorSelectedFeedback(bool activeStatus)
    {
        if (activeStatus)
            Cursor.SetCursor(_feedbackMagnifyingGlassCursor, Vector2.zero, CursorMode.ForceSoftware);
        else
            Cursor.SetCursor(_normalMagnifyingGlassCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void AddKnowledgmentType(LanguageKnowledgeType newKnowdlege)
    {
        _playerLanguageKnowledge.AddKnowledgmentType(newKnowdlege);
    }
}
