using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiConnector : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> noButtons = new List<GameObject>();
    [SerializeField]
    private List<GameObject> colorButtons = new List<GameObject>();


    public List<GameObject> GetButtonList(int index) {
        switch (index) {
            case 1:
                return noButtons;
            case 2:
                return colorButtons;
            default: return null;
        }
    }
}
