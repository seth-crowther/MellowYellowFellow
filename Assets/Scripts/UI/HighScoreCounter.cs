using System;
using System.IO;
using TMPro;
using UnityEngine;

public class HighScoreCounter : MonoBehaviour
{
    [SerializeField]
    Fellow fellow;

    TextMeshProUGUI text;
    int highScore = int.MinValue;
    string highScoreName;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateHighScore();
        SetText(highScore);
    }

    void Update()
    {
        // Update high score live if current player exceeds it
        if (Fellow.GetScore() > highScore)
        {
            SetText(Fellow.GetScore());
        }
    }

    // Want to avoid reading from file every frame so only call when level is loaded
    public void UpdateHighScore()
    {
        StreamReader scoresFile = new StreamReader("scores.txt");

        while (!scoresFile.EndOfStream)
        {
            string line = scoresFile.ReadLine();
            string[] nameScore = line.Split(' ');

            int tempScore = Int32.Parse(nameScore[1]);
            if (tempScore > highScore)
            {
                highScoreName = nameScore[0];
                highScore = tempScore;
            }
        }

        scoresFile.Close();
    }

    void SetText(int score)
    {
        text.text = "High Score\n" + score;
    }
}
