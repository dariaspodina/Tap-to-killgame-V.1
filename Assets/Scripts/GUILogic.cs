using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUILogic : MonoBehaviour {

    public GameObject PauseCanvas;
    GameObject pauseCanvasInstance;

    public GameObject FailCanvas;
    GameObject failCanvasInstance;

    public GameObject SuccessCanvas;
    GameObject successCanvasInstance;

    SceneMng sceneMng;
    Camera mainCamera;

    Text textScore;
    Slider timerSlider;

    GameLogic gameLogic;

    void Awake()
    {
        timerSlider = GameObject.Find("Timer").GetComponent<Slider>();
        textScore = GameObject.Find("ScoreValue").GetComponent<Text>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sceneMng = FindObjectOfType<SceneMng>();
        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void SpawnFailCanvas()
    {
        if (failCanvasInstance == null)
        {
            failCanvasInstance = Instantiate(FailCanvas, transform.position, Quaternion.identity);
            failCanvasInstance.GetComponent<Canvas>().worldCamera = mainCamera;
            GameObject.Find("RestartButton").GetComponent<Button>().onClick.AddListener(() => Restart());
            GameObject.Find("MenuButton").GetComponent<Button>().onClick.AddListener(() => LoadMenuScene());
        }
    }

    public void SpawnPauseCanvas()
    {
        if (pauseCanvasInstance == null)
        {
            pauseCanvasInstance = Instantiate(PauseCanvas, transform.position, Quaternion.identity);
            pauseCanvasInstance.GetComponent<Canvas>().worldCamera = mainCamera;
            GameObject.Find("ContinueButton").GetComponent<Button>().onClick.AddListener(() => ClosePauseCanvas());
            GameObject.Find("RestartButton").GetComponent<Button>().onClick.AddListener(() => Restart());
            GameObject.Find("MenuButton").GetComponent<Button>().onClick.AddListener(() => LoadMenuScene());
        }
    }

    public void SpawnSuccessCanvas(int score)
    {
        if (successCanvasInstance == null)
        {
            successCanvasInstance = Instantiate(SuccessCanvas, transform.position, Quaternion.identity);
            successCanvasInstance.GetComponent<Canvas>().worldCamera = mainCamera;
            GameObject.Find("PlayAgainButton").GetComponent<Button>().onClick.AddListener(() => Restart());
            GameObject.Find("MenuButton").GetComponent<Button>().onClick.AddListener(() => LoadMenuScene());
            GameObject.Find("Score").GetComponent<Text>().text = "Score: " + score.ToString();
        }
    }

    void ClosePauseCanvas()
    {
        Destroy(pauseCanvasInstance);
        gameLogic.UnPause();
    }

    void Restart()
    {
        sceneMng.ReloadCurrentScene();
        gameLogic.UnPause();
    }

    void LoadMenuScene()
    {
        sceneMng.LoadScene("MenuScene");
        gameLogic.UnPause();
    }

    public void ChangeSliderValue(float timer, float timerMax)
    {
        timerSlider.value = timer / timerMax;
    }

    public void ShowScore(int score)
    {
        textScore.text = score.ToString();
    }
}
