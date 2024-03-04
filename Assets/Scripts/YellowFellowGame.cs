using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFellowGame : MonoBehaviour
{
    [SerializeField]
    GameObject highScoreUI;

    [SerializeField]
    GameObject mainMenuUI;

    [SerializeField]
    GameObject gameUI;

    [SerializeField]
    GameObject winUI;

    [SerializeField]
    Fellow playerObject;

    GameObject[] pellets;

    Ghost[] ghosts;


    enum GameMode
    {
        InGame,
        MainMenu,
        HighScores
    }

    GameMode gameMode = GameMode.MainMenu;

    void Start()
    {
        pellets = GameObject.FindGameObjectsWithTag("Pellet");
        ghosts = FindObjectsOfType<Ghost>();
        StartMainMenu();
    }

    void Update()
    {
        switch(gameMode)
        {
            case GameMode.MainMenu:     UpdateMainMenu(); break;
            case GameMode.HighScores:   UpdateHighScores(); break;
            case GameMode.InGame:       UpdateMainGame(); break;
        }
    }

    void UpdateMainMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            StartHighScores();
        }
    }

    void UpdateHighScores()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartMainMenu();
        }
    }

    void UpdateMainGame()
    {
       if (playerObject.PelletsEaten() == pellets.Length)
       {
            Debug.Log("Level Completed!");
       }

       if (playerObject.IsDead())
       {
            ResetGame();
       }
    }

    void StartMainMenu()
    {
        gameMode                        = GameMode.MainMenu;
        mainMenuUI.gameObject.SetActive(true);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
    }


    void StartHighScores()
    {
        gameMode                = GameMode.HighScores;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }

    void StartGame()
    {
        gameMode                = GameMode.InGame;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    void ResetGame()
    {
        playerObject.ResetPos();

        foreach (Ghost ghost in ghosts)
        {
            ghost.ResetPos();
        }
    }
}
