using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClick : MonoBehaviour
{
    public void betPanel(string betType) {

        BettingPanel.instance.bettingpanelactive(betType);
       
        
    }
}
