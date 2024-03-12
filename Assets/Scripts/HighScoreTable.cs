using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField]
    private string highScoreFile = "scores.txt";

    [SerializeField]
    private Font scoreFont;

    struct HighScoreEntry 
    {
        public int score;
        public string name;
    }

    List<HighScoreEntry> allScores = new List<HighScoreEntry>();

    private void Start()
    {
        LoadHighScoreTable();
        SortHighScoreEntries();
        CreateHighScoreText();
    }

    private void Update()
    {
        
    }

    public void LoadHighScoreTable()
    {
        using (TextReader file = File.OpenText(highScoreFile))
        {
            string text = null;
            while ((text = file.ReadLine()) != null)
            {
                Debug.Log(text);
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
        for (int i = 0; i < allScores.Count; i++)
        {
            GameObject g = new GameObject();
            g.transform.parent = transform;

            Text t = g.AddComponent<Text>();
            t.text = allScores[i].name + "\t\t" + allScores[i].score;
            t.font = scoreFont;
            t.fontSize = 50;
            t.color = Color.white;

            g.transform.localPosition = new Vector3(0, (-i) * 6, 0);
            g.transform.localRotation = Quaternion.identity;
            g.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            g.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 100);
        }
    }
}
