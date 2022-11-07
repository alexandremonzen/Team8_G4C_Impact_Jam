using UnityEngine;
using UnityEngine.UI;

public sealed class FirstSelectButton : MonoBehaviour
{
    [SerializeField] private Button _firstselectButton;

    private void OnEnable()
    {
        _firstselectButton.Select();
    }
}
