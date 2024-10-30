using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void ModifyScore( int scoreModified)
    {
        score = scoreModified;
        scoreText.text = "Score:" + score;
    }
}
