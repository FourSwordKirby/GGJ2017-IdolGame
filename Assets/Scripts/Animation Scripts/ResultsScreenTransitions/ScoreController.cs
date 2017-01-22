using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text ScoreDisplay;
    public Text RankDisplay;


    float score;
    
    public void SetScore(float score)
    {
        this.score = score;
    }

    //This class used to display the score on the final results screen, do so here
    public void Update()
    {
        ScoreDisplay.text = "Score: " + score;

        if (score > 100)
            RankDisplay.text = "<b>SSS</b>UGOI!!";
        else if (score > 80)
            RankDisplay.text = "<b>SS</b>UGOI!";
        else if (score > 60)
            RankDisplay.text = "<b>S</b>UGOI";
        else if (score > 40)
            RankDisplay.text = "<b>A</b>nime";
        else if (score > 20)
            RankDisplay.text = "<b>B</b>-b-baka";
    }
}
