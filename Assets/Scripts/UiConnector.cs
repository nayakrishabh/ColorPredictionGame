using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiConnector : MonoBehaviour
{
    [SerializeField]
    private List<Button> noButtons = new List<Button>();
    [SerializeField]
    private List<Button> colorButtons = new List<Button>();
    [SerializeField]
    private List<Button> bigsmallButtons = new List<Button>();


    public List<Button> GetButtonList(int index) {
        switch (index) {
            case 1: return noButtons;
            case 2: return colorButtons;
            case 3: return bigsmallButtons;
            default: return null;
        }
    }
}
