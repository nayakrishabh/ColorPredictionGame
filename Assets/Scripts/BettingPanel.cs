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
    private int selbalance = 1;
    private int selquantity = 1;
    private bool isAgree = false;
    private bool infinal = false;
    private string betTypeBP;
    private int i = 1;
    private GameManager gm;
    public static BettingPanel instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        gm = GameManager.instance;
    }
    void Start()
    {
        TitleText.text = string.Format("Balance = {0} ({1}) ", gm.getBalance(), gm.getTimerType());
        addlistener();
    }
    void Update()
    {
        placeHolder.text = (selbalance * selquantity).ToString();
        TitleText.text = string.Format("Balance = {0} ({1}) ", gm.getBalance(), gm.getTimerType());
    }
    public void bettingpanelactive(string BT) {
        betTypeBP = BT;
        gameObject.SetActive(true);
    }
    public void bettingpanelinactive() {

        if (isAgree) {
            ColorPredictionGame.instance.PlaceBet(betTypeBP,betAmount);
            gm.setBalance(-betAmount);
            Debug.Log(gm.getBalance());
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
        minusButton.onClick.AddListener(() => addsubbal(minusButton));
        addButton.onClick.AddListener(() => addsubbal(addButton));
    }

    private void balanceSelection(Button buttonToSelect) {
        OnButtonClicked(buttonToSelect);
        selbalance = int.Parse(buttonToSelect.name);
    }
    private void quantitySelection(Button buttonToSelect) {
        OnButtonClickedQ(buttonToSelect);
        selquantity = int.Parse(buttonToSelect.name);
    }
    private void addsubbal(Button buttonselcted) {
        if (buttonselcted.name == "+Button") {
            
            while (i>=0) {
                selquantity = int.Parse(quantityButtons[i].name);
                i++;
                if (i >= 5) {
                    i = 5;
                }
                return;
            }
            
        }
        else if (buttonselcted.name == "-Button") {
            while (i >= 0) {
                selquantity = int.Parse(quantityButtons[i].name);
                i--;
                if (i < 0) {
                    i = 0;
                }
                return;
            }
        }
    }

    private void betAmountSetup() {
        betAmount = selbalance * selquantity;
        totalBet.text = string.Format("Total Bet : {0}", betAmount);
        finapanelon();

    }
    void OnButtonClicked(Button clickedButton) {
        if (selectedButtonBL != null) {
            ResetButtonAppearance(selectedButtonBL);
        }
        SetButtonSelected(clickedButton);
        selectedButtonBL = clickedButton;
    }
    void OnButtonClickedQ(Button clickedButton) {
        if (selectedButtonQT != null) {
            ResetButtonAppearance(selectedButtonQT);
        }
        SetButtonSelected(clickedButton);
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
    void finapanelon() {
        finalPanel.SetActive(true);
        infinal = true;
        buttoninstractability();
    }
    void buttoninstractability() {
        if (infinal) {
            foreach (Button obj in balanceButtons) {
                obj.GetComponent<Button>().interactable = false;
            }
            foreach (Button obj in quantityButtons) {
                obj.GetComponent<Button>().interactable = false;
            }
            submitButton.interactable = false;
            cancelButton.interactable= false;
            minusButton.interactable= false;
            addButton.interactable= false;
            agreementToggle.interactable= false;
        }
        else if(!infinal) {
            foreach (Button obj in balanceButtons) {
                obj.GetComponent<Button>().interactable = true;
            }
            foreach (Button obj in quantityButtons) {
                obj.GetComponent<Button>().interactable = true;
            }
            submitButton.interactable = true;
            cancelButton.interactable = true;
            minusButton.interactable = true;
            addButton.interactable = true;
            agreementToggle.interactable = true;
        }
    }
    void finalpaneloff() {
        isAgree = false;
        infinal = false;
        finalPanel.SetActive(false);
        buttoninstractability();
    }

    void isagree() {

        isAgree = true;
        bettingpanelinactive();
        if (finalPanel.activeSelf) {
            finalpaneloff();
        }
        
    }
}
