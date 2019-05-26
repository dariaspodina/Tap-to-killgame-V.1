using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    Camera mainCamera;
    int score;
    
    float timer;
    float timerMax = 60f;
    
    GUILogic guiLogic;
    PopUpManager popUp;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        popUp = FindObjectOfType<PopUpManager>();
        guiLogic = FindObjectOfType<GUILogic>();
        StartCoroutine(Timer(timerMax));
        score = 0;
        guiLogic.ShowScore(score);
    }

    void Update()
    {
        Click();
    }

    void Click()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetButton("Fire1"))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Block")
                {
                    return;
                }
                else
                {
                    if (hit.collider.tag != "Block")
                    {
                        if (hit.collider.tag == "Shape" && !hit.collider.gameObject.GetComponent<ShapeScripts>().isEnemy)
                        {
                            Vector3 pos = hit.collider.gameObject.GetComponent<Transform>().position;
                            IncrementScore();
                            guiLogic.ShowScore(score);
                            Destroy(hit.collider.gameObject);
                            popUp.SpawnPopUpScore("+1", pos);
                        }
                        if (hit.collider.tag == "Shape" && hit.collider.gameObject.GetComponent<ShapeScripts>().isEnemy)
                        {
                            Vector3 pos = hit.collider.gameObject.GetComponent<Transform>().position;
                            DecrementScore();
                            GameOver();
                            guiLogic.ShowScore(score);
                            Destroy(hit.collider.gameObject);
                            popUp.SpawnPopUpScore("-1", pos);
                        }
                    }
                }
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    void IncrementScore()
    {
        score++;
    }
    void DecrementScore()
    {
        score--;
    }

    void GameOver()
    {
        if (score < 0)
        {
            Pause();
            guiLogic.SpawnFailCanvas();
            score = 0;
        }
        if (score >= 0 && timer <= 0)
        {
            Pause();
            guiLogic.SpawnSuccessCanvas(score);
        }
    }

    public IEnumerator Timer(float timerMax)
    {
        timer = timerMax;
        while (timer > 0)
        {
            guiLogic.ChangeSliderValue(timer, timerMax);
            yield return new WaitForSeconds(1f);
            timer = timer - 1f;
        }
        GameOver();
    }
}
