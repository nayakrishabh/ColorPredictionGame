using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int panelcount = 2;
    // Start is called before the first frame update
    void Start()
    {
        while (panelcount > 0) {
            PanelCreator.Instance.panelCreator();
            panelcount--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
