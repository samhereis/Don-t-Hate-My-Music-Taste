using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageToPlayer : MonoBehaviour
{
    // A sible message to player

    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void ShowMessage(string message)
    {
        _text.text = message;
    }
}
