using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public UIBoard uiBoard;

    private int playerScore;
    private int shotArrowsCount;

    public bool RoundEnded { get { return shotArrowsCount >= 10; }}

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        InitializeGame();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Score(int points) {
        playerScore += points;
        Debug.Log($"[GameManager] Score puntos frecha {points} puntuación total {playerScore}");
        uiBoard.SetTotalScore(playerScore);
        uiBoard.SetLastScore(points);
    }

    public void RegisterArrowShot() {
        shotArrowsCount = Mathf.Clamp(++shotArrowsCount, 0, 10);
        uiBoard.SetArrowCount(shotArrowsCount);
    }

    public void InitializeGame() {
        playerScore = 0;
        shotArrowsCount = 0;
        uiBoard.SetTotalScore(playerScore);
        uiBoard.SetLastScore(0);
        uiBoard.SetArrowCount(shotArrowsCount);
    }
}
