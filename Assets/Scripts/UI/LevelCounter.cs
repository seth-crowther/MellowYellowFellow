using TMPro;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = "Level\n" + YellowFellowGame.GetLevel();
    }
}
