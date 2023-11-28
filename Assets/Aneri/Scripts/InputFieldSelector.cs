using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Keyboard;

public class InputFieldSelector : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public TextMeshProUGUI text;

    private Dictionary<Color, string> colorNames;

    private void Start()
    {
        InitializeColorDictionary();
    }

    private void InitializeColorDictionary()
    {
        colorNames = new Dictionary<Color, string>
        {
            { Color.red, "Red" },
            { Color.green, "Green" },
            { Color.blue, "Blue" },
            { Color.white, "White" },
            { Color.black, "Black" },
            { Color.yellow, "Yellow" },
            // Add more predefined colors here
        };
    }

    public string GetColorName(Color color)
    {
        string closestColorName = "Unknown";
        float closestColorDistance = float.MaxValue;

        foreach (KeyValuePair<Color, string> pair in colorNames)
        {
            float distance = Vector3.Distance(new Vector3(color.r, color.g, color.b), new Vector3(pair.Key.r, pair.Key.g, pair.Key.b));
            if (distance < closestColorDistance)
            {
                closestColorDistance = distance;
                closestColorName = pair.Value;
            }
        }

        return closestColorName;
    }

    public void SelectField()
    {
        keyboardManager.OutputFieldSelection(this.GetComponent<TMP_InputField>());
        /*text.text = this.GetComponent<TMP_InputField>().caretColor.ToString();*/

        Color caretColor = this.GetComponent<TMP_InputField>().caretColor;
        string colorName = GetColorName(caretColor);
        // Assuming 'text' is a reference to a TextMeshPro object
        text.text = colorName;
    }
}
