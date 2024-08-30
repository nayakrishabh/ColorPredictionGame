using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject countDownText;

    [SerializeField]
    private GameObject basePanel;

    [SerializeField]
    private GameObject secondPanel;

    private Transform cdpaneltranform;
    private GameObject timerPanel;
    private TextMeshProUGUI countdown;
    float remainingTime = 60f;
    //private List<GameObject> panelList;
    int panelcount = 2;

    // Start is called before the first frame update
    void Start()
    {
        //panelList = new List<GameObject>();

        while (panelcount > 0) {

            GameObject tempPanel = PanelCreator.Instance.panelCreator();

            if (panelcount == 2) {
                Instantiate(basePanel, tempPanel.transform);

            }
            else if (panelcount == 1) {
                Instantiate(secondPanel, tempPanel.transform);
            }
            panelcount--;
        }
        cdpaneltranform = GameObject.FindWithTag("Timer").GetComponent<Transform>();
        Debug.Log(cdpaneltranform.gameObject.name);
        timerPanel =  Instantiate(countDownText,cdpaneltranform);
        countdown = timerPanel.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime <= 0) {
            remainingTime = 60f;
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdown.text = string.Format("Time Remaining : {0:00}:{1:00}",minutes,seconds);


    }
}
