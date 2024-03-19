using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBoard : MonoBehaviour
{
    public TextMeshProUGUI arrowCount;
    public TextMeshProUGUI lastScore;
    public TextMeshProUGUI totalScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetArrowCount(int arrowCount) {
        this.arrowCount.text = arrowCount.ToString();
    }

    public void SetLastScore(int lastScore) {
        this.lastScore.text = lastScore.ToString();
    }

    public void SetTotalScore(int totalScore) {
        this.totalScore.text = totalScore.ToString();
    }

    public void OnRestartClicked() {
        GameManager.instance.InitializeGame();
    }

    public void OnRecoverBowClicked() {

    }
}
