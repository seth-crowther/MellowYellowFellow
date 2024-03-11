using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    [SerializeField]
    private Fellow fellow;

    private string playerName;

    public string GetName() { return playerName; }

    void Start()
    {
        var input = gameObject.GetComponent<TMP_InputField>();

        input.onEndEdit.AddListener(SubmitName);  
    }

    private void SubmitName(string arg0)
    {
        playerName = arg0;
        Debug.Log(arg0);

        StreamWriter writer = new StreamWriter("scores.txt", true);
        writer.WriteLine(playerName + " " + fellow.GetScore());
        writer.Close();
    }
}
