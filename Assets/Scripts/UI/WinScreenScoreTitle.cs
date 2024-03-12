using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreenScoreTitle : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "Game Over!\nYour score was: " + Fellow.GetScore();
    }
}
