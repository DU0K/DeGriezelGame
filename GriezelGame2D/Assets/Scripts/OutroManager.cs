using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    [SerializeField] private TMP_Text time;
    [SerializeField] private TMP_Text user;

    private void Start()
    {
        setStats();
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Login");
    }

    private void setStats()
    {
        float _time = PlayerPrefs.GetFloat("currentTime");
        string _user = PlayerPrefs.GetString("userName");

        time.text = _time.ToString("00.0000", System.Globalization.CultureInfo.InvariantCulture);
        user.text = _user;

        string existingScores = PlayerPrefs.GetString("nameAndScore", "");

        List<PlayerScore> scoreList = new List<PlayerScore>();

        if (!string.IsNullOrEmpty(existingScores))
        {
            string[] scoreEntries = existingScores.Split(',');
            foreach (string entry in scoreEntries)
            {
                string[] details = entry.Split(':');
                if (details.Length == 2 && float.TryParse(details[1], out float parsedTime))
                {
                    scoreList.Add(new PlayerScore { Name = details[0], Time = parsedTime });
                }
            }
        }

        scoreList.Add(new PlayerScore { Name = _user, Time = _time });
        scoreList = scoreList.OrderBy(s => s.Time).ToList();

        string updatedScores = string.Join(",", scoreList.Select(s => $"{s.Name}:{s.Time.ToString("00.0000", CultureInfo.InvariantCulture)}"));

        PlayerPrefs.SetString("nameAndScore", updatedScores);
        PlayerPrefs.Save();
    }

    private class PlayerScore
    {
        public string Name;
        public float Time;
    }

}
