using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
    private BettingPanel bettingPanel;

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

    private bool isGameLoopRunning = false;
    private float balance = 100000;

    float remainingTime = 5f;
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

        uiConnector = basePanel.GetComponent<UiConnector>();
        uiConnector2 = secondPanel.GetComponent<UIConnector2>();
        nobuttonList = new List<Button>();
        colorbuttonlist = new List<Button>();
        bigsmallbuttonlist = new List<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (BettingPanel.instance == null && bettingPanel != null) {
            BettingPanel.instance = bettingPanel;
        }
        bigsmallbuttonlist.Add(uiConnector2.bigButton);
        bigsmallbuttonlist.Add(uiConnector2.smallButton);
        cdpaneltranform = GameObject.FindWithTag("Timer").GetComponent<Transform>();
        Debug.Log(cdpaneltranform.gameObject.name);
        timerPanel =  Instantiate(countDownText,cdpaneltranform);
        countdown = timerPanel.GetComponent<TextMeshProUGUI>();

        StartGameLoop();
    }
    
    public void getButtonList() {
        foreach (GameObject obj in uiConnector.GetButtonList(1)) {
            nobuttonList.Add(obj.GetComponent<Button>());
        }
    }
    void StartGameLoop() {
        if (!isGameLoopRunning) {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator GameLoop() {
        if (isGameLoopRunning) yield break; // Prevent multiple loops from running
        isGameLoopRunning = true;

        while (true) {
            ColorPredictionGame.instance.StartNewRound();
            yield return new WaitForSeconds(remainingTime); // Ensure consistent timing
            ColorPredictionGame.instance.EndRound();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime <= 0) {
            remainingTime = 5f;
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdown.text = string.Format("Time Remaining : {0:00}:{1:00}",minutes,seconds);

        
    }
    public TimerType getTimerType() {
        return TimerType.MIN1;
    }
    public float getBalance() {
        return balance;
    }
    public void setBalance(float upbal) {
        balance += upbal;
    }
}
