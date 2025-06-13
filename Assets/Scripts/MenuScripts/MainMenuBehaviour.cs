using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public static MainMenuBehaviour instance;
    
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject loadingCanvas;
    [SerializeField] GameObject[] buttons;
    [SerializeField] TextMeshProUGUI highScoreText;

    [SerializeField] LeanTweenType loadingLevelAnimationIn;
    [SerializeField] LeanTweenType loadingLevelAnimationOut;
    [SerializeField] float loadingLevelAnimSpeedIn;
    [SerializeField] float loadingLevelAnimSpeedOut;
    float inRangePosY = 0f;
    float outOfRangePosY = -1580f;
    [SerializeField] LeanTweenType buttonsAnimation;

    [SerializeField] float buttonsAnimationSpeed;
    [SerializeField] Vector3 buttonsAnimationScaleVariation;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        highScoreText.text = PlayerPrefs.GetFloat("highScore").ToString();
        LeanTween.scale(buttons[0], buttons[0].transform.localScale + buttonsAnimationScaleVariation, buttonsAnimationSpeed).setEase(buttonsAnimation).setLoopPingPong();
        LeanTween.scale(buttons[1], buttons[1].transform.localScale + buttonsAnimationScaleVariation, buttonsAnimationSpeed + 0.4f).setEase(buttonsAnimation).setLoopPingPong();
    }

    public void StartGameIN()
    {
        LeanTween.moveLocalY(loadingCanvas, inRangePosY, loadingLevelAnimSpeedIn).setEase(loadingLevelAnimationIn).setOnComplete(() =>
        {
            mainMenuCanvas.SetActive(false);
            StartGameOUT();
        });
    }

    public void StartGameOUT()
    {
        LeanTween.moveLocalY(loadingCanvas, outOfRangePosY, loadingLevelAnimSpeedOut).setEase(loadingLevelAnimationOut);
    }

    public void LoadLevelIN()
    {
        LeanTween.moveLocalY(loadingCanvas, inRangePosY, loadingLevelAnimSpeedIn).setEase(loadingLevelAnimationOut).setOnComplete(() =>
        {
            SceneManager.LoadScene("Space");
        });
    }

    public void LoadLevelOUT()
    {
        SceneManager.LoadScene("Space");
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
