using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerLanguageKnowledge : MonoBehaviour
{
    [SerializeField] private List<LanguageKnowledgeType> _languageKnowledges;
    private bool _alreadyHaveThisKnowledment;


    public void AddKnowledgmentType(LanguageKnowledgeType newKnowledgment)
    {
        _alreadyHaveThisKnowledment = false;

        foreach (LanguageKnowledgeType knowledgeType in _languageKnowledges)
        {
            if (knowledgeType == newKnowledgment)
            {
                _alreadyHaveThisKnowledment = true;
                break;
            }
        }

        if (!_alreadyHaveThisKnowledment)
        {
            _languageKnowledges.Add(newKnowledgment);
        }
    }

    public bool SearchForLanguageKnowledges(LanguageKnowledgeType languageKnowledge)
    {
        foreach (LanguageKnowledgeType knowledgeType in _languageKnowledges)
        {
            if (knowledgeType == languageKnowledge)
            {
                return true;
            }
        }

        return false;
    }
}
