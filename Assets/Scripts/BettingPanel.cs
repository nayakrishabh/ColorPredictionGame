using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class BettingPanel : MonoBehaviour
{
    
    [SerializeField]
    private List<Button> balanceButtons = new List<Button>();
    [SerializeField]
    private List<Button> quantityButtons = new List<Button>();
    [SerializeField]
    private Button cancelButton;
    [SerializeField]
    private Button submitButton;
    [SerializeField]
    private Button minusButton;
    [SerializeField]
    private Button addButton;
    [SerializeField]
    private Button agreeButton;
    [SerializeField]
    private Button NoButton;

    [SerializeField]
    private Toggle agreementToggle;

    [SerializeField]
    private TMP_InputField InputField;

    [SerializeField]
    private TextMeshProUGUI placeHolder;

    [SerializeField]
    private TextMeshProUGUI TitleText;

    [SerializeField]
    private TextMeshProUGUI totalBet;

    [SerializeField]
    private GameObject finalPanel;

    private Button selectedButtonBL;
    private Button selectedButtonQT;
    private float playerBalance;
    private int betAmount;
    private TimerType selectedtimertype;
    private int selbalance;
    private int selquantity;
    private bool isAgree = false;
    private bool infinal = false;
    private string betTypeBP;


    public static BettingPanel instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        playerBalance = GameManager.instance.getBalance();
        selectedtimertype = GameManager.instance.getTimerType();
        TitleText.text = string.Format("Balance = {0} ({1}) ",playerBalance,selectedtimertype);
        addlistener();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bettingpanelactive(string BT) {
        betTypeBP = BT;
        gameObject.SetActive(true);
    }
    public void bettingpanelinactive() {
        if (isAgree) {
            Debug.Log($"bet Type : {betTypeBP} - Bet amount : {betAmount}");
            ColorPredictionGame.instance.PlaceBet(betTypeBP,betAmount);
        }

        if (finalPanel.activeSelf) {
            finalpaneloff();
        }
        gameObject.SetActive(false);
    }
    void addlistener() {
        submitButton.onClick.AddListener(betAmountSetup);

        cancelButton.onClick.AddListener(bettingpanelinactive);

        NoButton.onClick.AddListener(finalpaneloff);

        agreeButton.onClick.AddListener(isagree);
        foreach(Button obj in balanceButtons) {
            obj.onClick.AddListener(()=> balanceSelection(obj));
        }
        foreach (Button obj in quantityButtons) {
            obj.onClick.AddListener(() => quantitySelection(obj));
        }
    }

    private void balanceSelection(Button buttonToSelect) {
        OnButtonClicked(buttonToSelect);
        selbalance = int.Parse(buttonToSelect.name);
        Debug.Log(selbalance);
    }
    private void quantitySelection(Button buttonToSelect) {
        OnButtonClickedQ(buttonToSelect);
        selquantity = int.Parse(buttonToSelect.name);

        Debug.Log(selquantity);
        placeHolder.text = buttonToSelect.name;
    }
    private void betAmountSetup() {
        betAmount = selbalance * selquantity;
        totalBet.text = string.Format("Total Bet : {0}", betAmount);
        finalPanel.SetActive(true);
    }
    void OnButtonClicked(Button clickedButton) {
        // Deselect the previously selected button, if any
        if (selectedButtonBL != null) {
            ResetButtonAppearance(selectedButtonBL);
        }

        // Select the new button
        SetButtonSelected(clickedButton);

        // Update the selected button
        selectedButtonBL = clickedButton;
    }
    void OnButtonClickedQ(Button clickedButton) {
        // Deselect the previously selected button, if any
        if (selectedButtonQT != null) {
            ResetButtonAppearance(selectedButtonQT);
        }

        // Select the new button
        SetButtonSelected(clickedButton);

        // Update the selected button
        selectedButtonQT = clickedButton;
    }
    void SetButtonSelected(Button button) {
        button.GetComponent<Image>().color = Color.green;
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null) {
            buttonText.color = Color.white;
        }
    }

    void ResetButtonAppearance(Button button) {
        button.GetComponent<Image>().color = Color.white;
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null) {
            buttonText.color = Color.black;
        }
    }
    void finalpaneloff() {
        isAgree = false;
        finalPanel.SetActive(false);
    }

    void isagree() {
        isAgree = true;

        if(finalPanel.activeSelf) {
            finalpaneloff();
        }
        gameObject.SetActive(false);
    }
}
