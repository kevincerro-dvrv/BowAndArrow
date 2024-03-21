using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBoard : MonoBehaviour {
    public TextMeshProUGUI arrowCountText;
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI totalScoreText;

    public void SetArrowCount(int arrowCount) {
        arrowCountText.text = arrowCount.ToString();
    }

    public void SetLastScore(int score) {
        lastScoreText.text = score.ToString();
    }

    public void SetTotalScore(int score) {
        totalScoreText.text = score.ToString();
    }

    public void ButtonRestartOnClick() {
        Debug.Log("[IUBoard] ButtonRestartOnClick");
        GameManager.instance.InitializeGame();
    }

    public void ButtonRetrieveBowOnClick() {
        Debug.Log("[IUBoard] ButtonRetrieveBowOnClick");
    }

}
