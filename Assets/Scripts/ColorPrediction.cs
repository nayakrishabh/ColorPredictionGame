using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPredictionGame : MonoBehaviour {

    [SerializeField] private GameObject historyBlock;
    private enum ColorType { GREEN, VIOLET, RED }
    private enum SizeType { SMALL, BIG }

    private ColorType _selectedColor;
    private int _selectedNumber;
    private SizeType _selectedSize;
    private Transform parentTransform; 

    public static ColorPredictionGame instance;

    private const float RoundInterval = 60f; // 1-minute interval
    private bool isRoundActive = false;

    // Placeholder for bets (using Dictionary for quick lookup)
    private Dictionary<string, float> bets = new Dictionary<string, float>();
    private List<GameObject> listofhistory = new List<GameObject>();
    private List<MixedData>  selecteditems = new List<MixedData>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void Start() {
        if (parentTransform == null) {
            parentTransform = HistorySystem.instance.getparentTransform();
        }
    }
    public void StartNewRound() {
        if (isRoundActive) {
            Debug.LogWarning("Round is already active. Cannot start a new round.");
            return;
        }

        isRoundActive = true;
        Debug.Log("New round started. Place your bets!");
    }

    public void EndRound() {
        if (!isRoundActive) {
            Debug.LogWarning("No round active. Cannot end a round.");
            return;
        }

        isRoundActive = false;
        Debug.Log("Round ended.");
        // Generate the random outcome
        _selectedColor = (ColorType)Random.Range(0, 3);
        _selectedNumber = Random.Range(0, 10);
        _selectedSize = (_selectedNumber <= 4) ? SizeType.SMALL : SizeType.BIG;

        selecteditems.Add(new MixedData(_selectedColor.ToString(), _selectedNumber, _selectedSize.ToString()));


        GameObject historyB = Instantiate(historyBlock, parentTransform);
        Transform historyT = historyB.transform;
        HistoryinfoBlockCOn historyIBC = historyT.GetComponent<HistoryinfoBlockCOn>();

        historyT.SetAsFirstSibling();
        historyIBC.SetValues(_selectedColor.ToString(), _selectedNumber,_selectedSize.ToString());
        listofhistory.Add(historyB);

        Debug.Log($"Round result: {_selectedColor} - {_selectedNumber} - {_selectedSize}");

        // Evaluate the bets placed
        EvaluateBets();

        // Clear bets after evaluation
        bets.Clear();
    }
    public void PlaceBet(string betType, float amount) {
        if (!isRoundActive) {
            Debug.LogWarning("Betting is closed. Please wait for the next round.");
            return;
        }

        // If bet type exists, add the amount to the existing bet; otherwise, create a new entry
        if (bets.ContainsKey(betType)) {
            bets[betType] += amount;
        }
        else {
            bets[betType] = amount;
        }
        Debug.Log($"Bet placed: {betType} with amount: {amount}");
    }

    private void EvaluateBets() {
        foreach (var bet in bets) {
            bool isWinningBet = IsWinningBet(bet.Key);

            if (isWinningBet) {
                Debug.Log($"Winning bet: {bet.Key} with amount: {bet.Value}");
                // Payout logic can be added here, e.g., double the bet amount
                GameManager.instance.setBalance(bet.Value);
            }
            else {
                Debug.Log($"Losing bet: {bet.Key} with amount: {bet.Value}");
            }
        }
    }

    private bool IsWinningBet(string betKey) {
        switch (betKey) {
            case "GREEN":
            case "VIOLET":
            case "RED":
                return _selectedColor.ToString() == betKey;

            case "SMALL":
            case "BIG":
                return _selectedSize.ToString() == betKey;

            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
                return _selectedNumber.ToString() == betKey;

            default:
                return false;
        }
    }
    public List<MixedData> getHistoryData() {
        return selecteditems;
    }
    public bool getRoundActive() {
        return isRoundActive;
    }
}
public class MixedData {
    public string selectedColor;
    public int selectedNO;
    public string SelectedSize;

    public MixedData(string selectC, int selectN, string selectS) {
        selectedColor = selectC;
        selectedNO = selectN;
        SelectedSize = selectS;
    }
}