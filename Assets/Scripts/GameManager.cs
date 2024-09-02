using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum TimerType{ MIN1, MIN2, MIN5 }

    [SerializeField]
    private GameObject countDownText;

    [SerializeField]
    private GameObject basePanel;

    [SerializeField]
    private GameObject secondPanel;

    [SerializeField]
    private GameObject bettingPanel;

    [SerializeField]
    private UiConnector uiConnector;
    [SerializeField]
    private UIConnector2 uiConnector2;

    private List<Button> nobuttonList;
    private List<Button> colorbuttonlist;
    private List<Button> bigsmallbuttonlist;

    private Transform cdpaneltranform;
    private GameObject timerPanel;
    private TextMeshProUGUI countdown;

    private float balance = 100000;

    float remainingTime = 60f;
    //private List<GameObject> panelList;
    int panelcount = 2;

    public static GameManager instance;

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
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

        Instantiate(bettingPanel, GetComponent<RectTransform>());
        bettingPanel.SetActive(false);

        uiConnector = basePanel.GetComponent<UiConnector>();
        uiConnector2 = secondPanel.GetComponent<UIConnector2>();
        nobuttonList = new List<Button>();
        colorbuttonlist = new List<Button>();
        bigsmallbuttonlist = new List<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {

        nobuttonList = uiConnector.GetButtonList(1);
        colorbuttonlist = uiConnector.GetButtonList(2);
        bigsmallbuttonlist.Add(uiConnector2.bigButton);
        bigsmallbuttonlist.Add(uiConnector2.smallButton);

        Debug.Log(nobuttonList.Count);
        for (int i = 0; i < nobuttonList.Count; i++) {
            int index = i;
            nobuttonList[index].onClick.AddListener(() => {
                Debug.Log("Button Clicked: " + nobuttonList[index].name);
                betPanel(nobuttonList[index].name);
            });
        }

        for (int i = 0; i < colorbuttonlist.Count; i++) {
            int index = i;
            colorbuttonlist[index].onClick.AddListener(() => betPanel(colorbuttonlist[index].name));
        }

        for (int i = 0; i < bigsmallbuttonlist.Count; i++) {
            int index = i;
            bigsmallbuttonlist[index].onClick.AddListener(() => betPanel(bigsmallbuttonlist[index].name));
        }

        cdpaneltranform = GameObject.FindWithTag("Timer").GetComponent<Transform>();
        Debug.Log(cdpaneltranform.gameObject.name);
        timerPanel =  Instantiate(countDownText,cdpaneltranform);
        countdown = timerPanel.GetComponent<TextMeshProUGUI>();

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop() {
        while (true) {
            ColorPredictionGame.instance.StartNewRound();
            yield return new WaitForSeconds(remainingTime);
            EndRound();
        }
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

    void betPanel(string betType) {
        Debug.Log("fshtsrhresh");
        bettingPanel.SetActive(true);    
    }
    private void EndRound() {
        ColorPredictionGame.instance.EndRound();
        ColorPredictionGame.instance.StartNewRound();
    }

    public TimerType getTimerType() {
        return TimerType.MIN1;
    }
    public float getBalance() {
        return balance;
    }
    
}
