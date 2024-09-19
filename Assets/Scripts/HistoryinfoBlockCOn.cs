using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryinfoBlockCOn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private TextMeshProUGUI bigSmall;
    [SerializeField] private Image color;
    void Start()
    {
        
    }
    void Update()
    {
    }
    public void SetValues(string clr, int no, string size) {
        // Get hex color code from the color name
        string hexCD = GetHex(clr);

        // Attempt to change the color using the hex code
        if (!ChangeColor(hexCD)) {
            Debug.LogError("Failed to change the color with hex code: " + hexCD);
        }

        // Set other values for UI elements
        number.text = no.ToString();
        bigSmall.text = size;
    }

    public string GetHex(string clr) {
        // Convert the color string to upper case for case-insensitive matching
        switch (clr.ToUpper()) {
            case "GREEN":
                return "#52BF90"; // Ensure valid hex format with a leading '#'

            case "RED":
                return "#E03E3E";

            case "VIOLET":
                return "#B968FF";

            default:
                Debug.LogWarning("Color not recognized, returning default black.");
                return "#000000"; // Return black as a default
        }
    }

    public bool ChangeColor(string hexCode) {
        // Try to parse the hex code into a Unity Color
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hexCode, out newColor)) {
            // Apply the color to the UI element
            color.color = newColor;
            return true; // Success
        }
        else {
            Debug.LogError("Invalid hex color code: " + hexCode);
            return false; // Failure
        }
    }

}

