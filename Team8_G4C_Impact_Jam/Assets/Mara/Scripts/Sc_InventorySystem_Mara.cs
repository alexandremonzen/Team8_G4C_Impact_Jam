using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_InventorySystem_Mara : MonoBehaviour
{
    public string m_itemHold;
    public string HoldingItem
    {
        get; set;
    }
    public List<string> m_knoledge = new List<string>();
    public string Knoledge
    {
        get; set;
    }
    public List<string> m_wordsKnown = new List<string>();
    public string WordsKnown
    {
        get; set;
    }

    public void SetItemHolding(string item)
    {
        m_itemHold = item;
    }
    public string GetItemHolding()
    {
        return m_itemHold;
    }
    public void DropHoldingItem()
    {
        m_itemHold = "";
    }
    public bool IsHoldingItem(string item = "")
    {
        if (item == "" && m_itemHold != "")
        {
            return true;
        }
        return item == m_itemHold;
    }


    public void AddKnoledge(string known)
    {
        m_knoledge.Add(known);
    }
    public void RemoveKnoledge(string known)
    {
        m_knoledge.Remove(known);
    }
    public bool HasKnowledge(string know)
    {
        return m_knoledge.Contains(know);
    }


    public void AddKnownWord(string word)
    {
        m_wordsKnown.Add(word);
    }
    public void RemoveKnownWord(string word)
    {
        m_wordsKnown.Remove(word);
    }
    public bool KnowsWord(string word)
    {
        return m_wordsKnown.Contains(word);
    }
}
