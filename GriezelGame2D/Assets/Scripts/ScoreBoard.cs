using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text scoreBoard;

    private void Start()
    {
        string nameAndScore = PlayerPrefs.GetString("nameAndScore", "Geen scores bekend");
        string formattedScoreBoard = nameAndScore.Replace(",", "\n").Replace(":", " - ");
        scoreBoard.text = formattedScoreBoard;
    }

}
