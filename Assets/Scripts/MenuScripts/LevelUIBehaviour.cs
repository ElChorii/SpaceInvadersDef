using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUIBehaviour : MonoBehaviour
{
    public static LevelUIBehaviour instance;

    [SerializeField] GameObject startGameCanvas;
    [SerializeField] GameObject loadLevelCanvas;
    [SerializeField] GameObject gameOverCanvas;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    [SerializeField] TextMeshProUGUI nextLevelScore;

    [SerializeField] LeanTweenType loadLevelAnimationIn;
    [SerializeField] LeanTweenType loadLevelAnimationOut;
    [SerializeField] float loadLevelAnimSpeedIn;
    [SerializeField] float loadLevelAnimSpeedOut;
    float inRangePosY = 0f;
    float outOfRangePosYUP = 1580;
    float outOfRangePosYDOWN = -1580;

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
        LeanTween.moveLocalY(startGameCanvas, outOfRangePosYUP, loadLevelAnimSpeedOut).setEase(loadLevelAnimationIn);
    }

    public void NextLevelIN()
    {
        EnemiesBehaviour.instance.mustMove = false;
        nextLevelScore.text = LivesPointsBehaviour.instance.currentScore.ToString();
        LeanTween.moveLocalY(loadLevelCanvas, inRangePosY, loadLevelAnimSpeedIn).setEase(loadLevelAnimationIn).setOnComplete(() =>
        {
            TimerBehaviour.instance.betweenLevelAnimation = true;
            EnemiesBehaviour.instance.Restart();
        });
    }

    public void NextLevelOUT()
    {
        LeanTween.moveLocalY(loadLevelCanvas, outOfRangePosYUP, loadLevelAnimSpeedOut).setEase(loadLevelAnimationOut).setOnComplete(() =>
        {
            loadLevelCanvas.transform.localPosition = new Vector3(loadLevelCanvas.transform.localPosition.x, outOfRangePosYDOWN, loadLevelCanvas.transform.localPosition.z);
            EnemiesBehaviour.instance.mustMove = true;
        });
    }

    public void GameOverIN()
    {
        scoreText.text = LivesPointsBehaviour.instance.currentScore.ToString();
        if (PlayerPrefs.HasKey("highScore"))
        { 
            if (LivesPointsBehaviour.instance.currentScore > PlayerPrefs.GetInt("highScore"))
            {
                LivesPointsBehaviour.instance.highScore = LivesPointsBehaviour.instance.currentScore;
                PlayerPrefs.SetFloat("highScore", LivesPointsBehaviour.instance.highScore);
                PlayerPrefs.Save();
                highScoreText.text = "Récord: " + LivesPointsBehaviour.instance.currentScore.ToString();
            }
        }
        else
        {
            if (LivesPointsBehaviour.instance.currentScore > LivesPointsBehaviour.instance.highScore)
            {
                LivesPointsBehaviour.instance.highScore = LivesPointsBehaviour.instance.currentScore;
                PlayerPrefs.SetFloat("highScore", LivesPointsBehaviour.instance.highScore);
                PlayerPrefs.Save();
                highScoreText.text = "Récord: " + LivesPointsBehaviour.instance.currentScore.ToString();
            }
        }
        EnemiesBehaviour.instance.mustMove = false;
        LeanTween.moveLocalY(gameOverCanvas, inRangePosY, loadLevelAnimSpeedIn).setEase(loadLevelAnimationIn);
    }

    public void GameOverOUT()
    {
        LivesPointsBehaviour.instance.currentScore = 0;
        LivesPointsBehaviour.instance.scoreOnScreen.text = "0";
        LivesPointsBehaviour.instance.currentLives = LivesPointsBehaviour.instance.startLives;
        EnemiesBehaviour.instance.Restart();
        LeanTween.moveLocalY(gameOverCanvas, outOfRangePosYDOWN, loadLevelAnimSpeedOut).setEase(loadLevelAnimationOut).setOnComplete(() =>
        {
            EnemiesBehaviour.instance.mustMove = true;
        });
    }
}
