using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject popUpScore;
    GameObject popUpScoreInstance;

    public GameObject popUpQuit;
    GameObject popUpQuitInstance;

    Camera mainCamera;
    SceneMng sceneMng;

    GameLogic gameLogic;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sceneMng = FindObjectOfType<SceneMng>();
    }
    
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (popUpQuitInstance != null)
                {
                    return;
                }
                else
                {
                    SpawnPopUpQuit();
                    return;
                }
            }
        }
    }

    // создание всплывающих окон с очками при касании и уничтожении объектов
    public void SpawnPopUpScore(string text, Vector3 pos)
    {
        pos = new Vector3(pos.x, pos.y + 0.5f, pos.z);
        popUpScoreInstance = Instantiate(popUpScore, transform.position, Quaternion.identity);
        popUpScoreInstance.transform.GetComponentInChildren<Canvas>().worldCamera = mainCamera;
        popUpScoreInstance.transform.GetChild(0).position = pos;
        popUpScoreInstance.transform.GetComponentInChildren<Text>().text = text;
        Destroy(popUpScoreInstance, 3f);
    }

    // создание popup для выхода из игры
    void SpawnPopUpQuit()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        if (gameLogic != null)
        {
            gameLogic.Pause();
        }

        popUpQuitInstance = Instantiate(popUpQuit, transform.position, Quaternion.identity);
        popUpQuitInstance.transform.GetComponentInChildren<Canvas>().worldCamera = mainCamera;
        GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(() => sceneMng.ExitGame());
        GameObject.Find("No").GetComponent<Button>().onClick.AddListener(() => CloseQuitPopUp());
    }

    void CloseQuitPopUp()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        if (gameLogic != null)
        {
            gameLogic.UnPause();
        }
        
        Destroy(popUpQuitInstance);
    }
}
