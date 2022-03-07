using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
