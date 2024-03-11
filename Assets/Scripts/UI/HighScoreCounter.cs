using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class HighScoreCounter : MonoBehaviour
{
    TextMeshProUGUI text;
    int highScore = int.MinValue;
    string highScoreName;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateHighScore();
        text.text = "High Score\n" + highScore;
    }

    void Update()
    {
        
    }

    void UpdateHighScore()
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
}
