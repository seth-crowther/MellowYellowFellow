using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class NameInput : MonoBehaviour
{
    void Start()
    {
        var input = gameObject.GetComponent<TMP_InputField>();

        input.onEndEdit.AddListener(SubmitName);  
    }

    private void SubmitName(string playerName)
    {
        StreamWriter writer = new StreamWriter("scores.txt", true);
        writer.WriteLine(playerName + " " + Fellow.GetScore());
        writer.Close();
    }
}
