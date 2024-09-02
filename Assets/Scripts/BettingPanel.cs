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
    private Toggle agreementToggle;

    [SerializeField]
    private TMP_InputField InputField;
    [SerializeField]
    private TextMeshProUGUI TitleText;

    private float playerBalance;
    private TimerType selectedtimertype;
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
