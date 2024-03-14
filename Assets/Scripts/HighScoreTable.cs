using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField]
    private string highScoreFile = "scores.txt";

    [SerializeField]
    private Font scoreFont;

    [SerializeField]
    TextMeshProUGUI namesColumn;

    [SerializeField]
    TextMeshProUGUI scoresColumn;

    const int numScoresShown = 10;

    struct HighScoreEntry 
    {
        public int score;
        public string name;
    }

    List<HighScoreEntry> allScores = new List<HighScoreEntry>();
    void OnEnable()
    {
        LoadHighScoreTable();
        SortHighScoreEntries();
        CreateHighScoreText();
        TrimTextFile();
    }

    public void LoadHighScoreTable()
    {
        allScores.Clear();

        using (TextReader file = File.OpenText(highScoreFile))
        {
            string text = null;
            while ((text = file.ReadLine()) != null)
            {
                string[] splits = text.Split(' ');
                HighScoreEntry entry;
                entry.name = splits[0];
                entry.score = int.Parse(splits[1]);
                allScores.Add(entry);
            }
        }
    }

    public void SortHighScoreEntries()
    {
        allScores.Sort((x, y) => y.score.CompareTo(x.score));
    }

    private void CreateHighScoreText()
    {
        string names = "";
        string scores = "";

        // Only show top numScoresShown high scores

        int maxEntries = Mathf.Min(numScoresShown, allScores.Count);

        for (int i = 0; i < maxEntries; i++)
        {
            names += allScores[i].name + "\n";
            scores += allScores[i].score + "\n";
        }

        namesColumn.text = names;
        scoresColumn.text = scores;
    }

    void TrimTextFile()
    {
        // Trim text file to top numScoresShown entries so it doesn't increase in size forever
        StreamWriter writer = new StreamWriter("scores.txt");

        int maxEntries = Mathf.Min(numScoresShown, allScores.Count);

        for (int i = 0; i < maxEntries; i++)
        {
            writer.WriteLine(allScores[i].name + " " + allScores[i].score);
        }
        
        writer.Close();
    }
}
