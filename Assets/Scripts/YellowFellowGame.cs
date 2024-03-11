using TMPro;
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
    Fellow fellow;

    GameObject[] pellets;

    GhostStateManager[] ghosts;

    float difficulty = 1.0f;

    public GhostStateManager[] GetGhosts() {  return ghosts; }

    enum GameMode
    {
        InGame,
        MainMenu,
        HighScores,
        Win
    }

    GameMode gameMode = GameMode.MainMenu;

    public bool IsInGame() {  return gameMode == GameMode.InGame; }

    void Start()
    {
        pellets = GameObject.FindGameObjectsWithTag("Pellet");
        ghosts = FindObjectsOfType<GhostStateManager>();
        StartMainMenu();
    }

    void Update()
    {
        switch(gameMode)
        {
            case GameMode.MainMenu:     UpdateMainMenu(); break;
            case GameMode.HighScores:   UpdateHighScores(); break;
            case GameMode.InGame:       UpdateMainGame(); break;
            case GameMode.Win:          UpdateWinScreen(); break;
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
       if (fellow.PelletsEaten() == pellets.Length)
       {
            Debug.Log("Level Completed!");
       }

        if (fellow.GetLives() <= 0)
        {
            StartWinScreen();
        }
    }

    void UpdateWinScreen()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetGame();
            StartMainMenu();
        }
    }

    void StartMainMenu()
    {
        gameMode                        = GameMode.MainMenu;
        mainMenuUI.gameObject.SetActive(true);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
    }


    void StartHighScores()
    {
        gameMode                = GameMode.HighScores;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
    }

    void StartGame()
    {
        gameMode                = GameMode.InGame;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        winUI.gameObject.SetActive(false);

        fellow.Init();
        foreach (GameObject obj in pellets) { obj.SetActive(true); } 
    }

    void StartWinScreen()
    {
        gameMode = GameMode.Win;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(true);

        foreach (GhostStateManager ghost in ghosts) { ghost.GetAgent().isStopped = true; }

        TextMeshProUGUI text = winUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "Game Over!\nYour score was: " + fellow.GetScore();
    }

    public void ResetGame()
    {
        fellow.ResetPos();

        foreach (GhostStateManager ghost in ghosts)
        {
            ghost.ResetPos();
        }
    }

    public void NextLevel()
    {

    }
}
