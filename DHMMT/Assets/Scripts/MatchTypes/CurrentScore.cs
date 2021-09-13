using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    // On a matchType_Page current score data when main player wins

    public string currentScore;

    public void SetScoreText(int value)
    {
        currentScore = value.ToString();
    }
}
