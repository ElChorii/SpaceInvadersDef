using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesPointsBehaviour : MonoBehaviour
{
    public static LivesPointsBehaviour instance;
    
    public int startLives;
    public float scoreToAdd;

    [HideInInspector] public int currentLives;
    [HideInInspector] public float highScore;
    [HideInInspector] public float currentScore;

    public TextMeshProUGUI scoreOnScreen;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        currentLives = startLives;
    }

    public void AddScore(int enemiesAmount)
    {
        currentScore += scoreToAdd * enemiesAmount;
        scoreOnScreen.text = currentScore.ToString();
    }
    
    public void LoseLive()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            LevelUIBehaviour.instance.GameOverIN();
        }
    }
}
