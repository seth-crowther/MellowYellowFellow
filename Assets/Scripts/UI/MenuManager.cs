using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuUI;

    [SerializeField]
    GameObject highScoreUI;

    [SerializeField]
    GameObject winUI;
    enum Menu
    {
        MAIN,
        HIGHSCORE,
        WIN
    }

    Menu currentMenu;

    void Start()
    {

        if (Fellow.GetLives() <= 0)
        {
            SwitchMenu(winUI);
        }
        else
        {
            SwitchMenu(mainMenuUI);
        }
    }

    void Update()
    {
        switch (currentMenu)
        {
            case Menu.MAIN: UpdateMainMenu(); break;
            case Menu.HIGHSCORE: UpdateHighScore(); break;
            case Menu.WIN: UpdateWin(); break;
        }
    }

    public void SwitchMenu(GameObject newMenu)
    {
        mainMenuUI.SetActive(false);
        winUI.SetActive(false);
        highScoreUI.SetActive(false);

        newMenu.SetActive(true);
        
        if (newMenu == mainMenuUI) { currentMenu = Menu.MAIN; }
        else if (newMenu == highScoreUI) { currentMenu = Menu.HIGHSCORE; }
        else if (newMenu == winUI) { currentMenu = Menu.WIN; }
    }

    void UpdateMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchMenu(highScoreUI);
        }
    }

    void UpdateHighScore()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchMenu(mainMenuUI);
        }
    }

    void UpdateWin()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchMenu(mainMenuUI);
        }
    }
}
