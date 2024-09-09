using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPredictionGame : MonoBehaviour {
    private enum ColorType { GREEN, VIOLET, RED }
    private enum SizeType { SMALL, BIG }

    private ColorType selectedColor;
    private int selectedNumber;
    private SizeType selectedSize;

    public static ColorPredictionGame instance;

    private const float RoundInterval = 60f; // 1-minute interval
    private bool isRoundActive = false;

    // Placeholder for bets (using Dictionary for quick lookup)
    private Dictionary<string, float> bets = new Dictionary<string, float>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        
    }


    public void StartNewRound() {
        isRoundActive = true;
        Debug.Log("New round started. Place your bets!");
    }

    public void EndRound() {
        isRoundActive = false;

        // Generate the random outcome
        selectedColor = (ColorType)Random.Range(0, 3);
        selectedNumber = Random.Range(0, 10);
        selectedSize = (selectedNumber <= 4) ? SizeType.SMALL : SizeType.BIG;

        Debug.Log($"Round result: {selectedColor} - {selectedNumber} - {selectedSize}");

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
                return selectedColor.ToString() == betKey;

            case "SMALL":
            case "BIG":
                return selectedSize.ToString() == betKey;

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
                return selectedNumber.ToString() == betKey;

            default:
                return false;
        }
    }
}
