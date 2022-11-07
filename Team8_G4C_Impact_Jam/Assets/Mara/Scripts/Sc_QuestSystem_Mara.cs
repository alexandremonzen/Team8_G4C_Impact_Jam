using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Quest
{
    public string itemNeeded;
    public List<string> knowledgeNeeded;
    public List<string> wordsNeeded;
}

[System.Serializable]
public class Sc_QuestSystem_Mara : MonoBehaviour
{
    public Sc_InventorySystem_Mara m_inventory;
    public Dictionary<string, Quest> m_quests = new Dictionary<string, Quest>();
    //[SerializeField] public Dictionary<string, int> m_ques = new Dictionary<string, int>();
    //public Quest q = new Quest();
    // Start is called before the first frame update
    void Start()
    {
        //m_inventory = this.gameObject.GetComponent<Sc_InventorySystem_Mara>();
        //if (m_inventory == null)
        //{
        //    m_inventory = this.gameObject.AddComponent<Sc_InventorySystem_Mara>();
        //}
    }

    public void AddQuest(string questName, Quest quest)
    {
        m_quests.Add(questName, quest);
    }
    public void RemoveQuest(string questName)
    {
        m_quests.Remove(questName);
    }
    public bool HasQuest(string questName)
    {
        return m_quests.ContainsKey(questName);
    }
    public bool HasCompletedQuest(string questName)
    {
        if (null == m_inventory)
        {
            Debug.Log("NO INVENTORY!");
            return false;
        }

        Quest q = m_quests[questName];
        if (!m_inventory.IsHoldingItem(q.itemNeeded))
        {
            return false;
        }
        foreach (string kl in q.knowledgeNeeded)
        {
            if (!m_inventory.HasKnowledge(kl))
            {
                return false;
            }
        }
        foreach (string word in q.wordsNeeded)
        {
            if (!m_inventory.KnowsWord(word))
            {
                return false;
            }
        }

        return true;
    }
}
