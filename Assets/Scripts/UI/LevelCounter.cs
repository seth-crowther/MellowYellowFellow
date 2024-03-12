using TMPro;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    [SerializeField]
    YellowFellowGame game;

    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = "Level\n" + game.GetLevel();
    }
}
