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
    TextMeshProUGUI winScreenTitle;

    [SerializeField]
    HighScoreCounter hsc;

    [SerializeField]
    Fellow fellow;

    GameObject[] pellets;
    GameObject[] powerups;
    GhostStateManager[] ghosts;

    int level = 1;

    public int GetLevel() { return level; }

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
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
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
            Debug.Log("Next level");
            level++;
            fellow.ResetPelletsEaten();
            ResetCharsPos();
            foreach (GameObject obj in pellets) { obj.SetActive(true); }
            foreach (GameObject obj in powerups) { obj.SetActive(true); }
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

        ResetGame();
    }

    void StartWinScreen()
    {
        gameMode = GameMode.Win;
        mainMenuUI.gameObject.SetActive(false);
        highScoreUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(true);

        foreach (GhostStateManager ghost in ghosts) { ghost.GetAgent().isStopped = true; }

        winScreenTitle.text = "Game Over!\nYour score was: " + fellow.GetScore();
    }

    public void ResetGame()
    {
        ResetCharsPos();

        foreach (GameObject obj in powerups) { obj.SetActive(true); }
        foreach (GameObject obj in pellets) { obj.SetActive(true); }

        hsc.UpdateHighScore();
        fellow.Init();
    }

    public void ResetCharsPos()
    {
        fellow.ResetPos();

        foreach (GhostStateManager ghost in ghosts)
        {
            ghost.SwitchState(StateType.WAITING);
        }
    }
}
