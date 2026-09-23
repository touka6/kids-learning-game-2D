using TMPro;
using UnityEngine;

public class BubbleData : MonoBehaviour
{
    private string value;
    public TMP_Text bubbleText;

    public void SetValue(string newValue)
    {
        value = newValue;
        bubbleText.text = newValue;
    }
    public string GetValue()
    {
        // Debug.Log($"value: {value}, bubbleText: {bubbleText.text}");
        return value;
    }
}
