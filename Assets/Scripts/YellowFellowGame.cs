using UnityEngine;

public class YellowFellowGame : MonoBehaviour
{
    static Fellow fellow;

    static GameObject[] pellets;
    static GameObject[] powerups;
    static GhostStateManager[] ghosts;

    static int level = 1;

    public int GetLevel() { return level; }

    public GhostStateManager[] GetGhosts() {  return ghosts; }
    public static int NumPellets() { return pellets.Length; }

    void Start()
    {
        pellets = GameObject.FindGameObjectsWithTag("Pellet");
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        ghosts = FindObjectsOfType<GhostStateManager>();
        fellow = FindObjectOfType<Fellow>();
    }

    public static void NextLevel()
    {
        level++;
        ResetCharsPos();
        foreach (GameObject obj in pellets) { obj.SetActive(true); }
        foreach (GameObject obj in powerups) { obj.SetActive(true); }
    }

    public static void ResetCharsPos()
    {
        fellow.ResetPos();

        foreach (GhostStateManager ghost in ghosts)
        {
            ghost.SwitchState(StateType.WAITING);
        }
    }
}
