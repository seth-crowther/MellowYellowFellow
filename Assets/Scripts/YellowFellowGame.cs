using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class YellowFellowGame : MonoBehaviour
{
    static Fellow fellow;

    static GameObject[] pellets;
    static GameObject[] powerups;
    static GhostStateManager[] ghosts;

    static int level = 1;

    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    RawImage fade;

    float fadeAlpha;

    enum Fade
    { 
        IN,
        OUT,
        NONE
    }

    Fade currentFadeState = Fade.NONE;

    public static int GetLevel() { return level; }
    public static GhostStateManager[] GetGhosts() {  return ghosts; }
    public static int NumPellets() { return pellets.Length; }

    public void DeathSound()
    {
        StartCoroutine(soundManager.DeathSound());
    }

    void Start()
    {
        pellets = GameObject.FindGameObjectsWithTag("Pellet");
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        ghosts = FindObjectsOfType<GhostStateManager>();
        fellow = FindObjectOfType<Fellow>();
    }

    void Update()
    {
        if (currentFadeState == Fade.IN) { fadeAlpha -= Time.deltaTime; }
        else if (currentFadeState == Fade.OUT) { fadeAlpha += Time.deltaTime; }
        else if (currentFadeState == Fade.NONE) { fadeAlpha = 0; }

        fadeAlpha = Mathf.Clamp(fadeAlpha, 0.0f, 1.0f);

        fade.color = new Color(0, 0, 0, fadeAlpha);
    }

    public IEnumerator NextLevel()
    {
        Fellow.SetDead(true);
        StartCoroutine(soundManager.LevelCompleteSound());
        yield return StartCoroutine(FadeOut());
        level++;
        ResetCharsPos();
        foreach (GameObject obj in pellets) { obj.SetActive(true); }
        foreach (GameObject obj in powerups) { obj.SetActive(true); }
        yield return StartCoroutine(FadeIn());
        currentFadeState = Fade.NONE;
        Fellow.SetDead(false);
    }

    public static void ResetCharsPos()
    {
        fellow.ResetPos();

        foreach (GhostStateManager ghost in ghosts)
        {
            ghost.SwitchState(StateType.WAITING);
        }
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        currentFadeState = Fade.IN;
        yield return new WaitUntil(() => fadeAlpha < 0.05f);
    }

    public IEnumerator FadeOut()
    {
        currentFadeState = Fade.OUT;
        yield return new WaitUntil(() => fadeAlpha > 0.95f);
    }
}
