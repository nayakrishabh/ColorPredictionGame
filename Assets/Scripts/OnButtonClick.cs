using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClick : MonoBehaviour
{
    void onbuttonclick() {
        string buttonName = gameObject.name;
        Debug.Log("Button clicked: " + buttonName);
    }
}
