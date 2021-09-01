using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    public string currentScore;

    public void SetScoreText(int value)
    {
        currentScore = value.ToString();
    }
}
