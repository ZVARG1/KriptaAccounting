using TMPro;
using UnityEngine;

public class DynamicField : MonoBehaviour
{
    public TMP_Text label;
    public TMP_InputField input;

    public void Setup(string fieldName, string value = "")
    {
        label.text = fieldName;
        input.text = value;
    }

    public string Value => input.text;
}