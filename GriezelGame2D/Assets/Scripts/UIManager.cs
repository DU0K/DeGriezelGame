using System;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsBox;
    [SerializeField] private int MaxPoints;
    private GameObject[] allCandy;
    private int points;

    [SerializeField] private TMP_Text timerBox;
    private float time;

    private bool hasDeliverdAllCandys = false;

    public static event Action<int> sendCurrentPoints;

    private void OnEnable()
    {
        PickUp.sendPoints += DisplayPoints;
        CandyCheck.deliverdAllCandy += CheckIfhasDeliverdAllCandys;
    }

    private void OnDisable()
    {
        PickUp.sendPoints -= DisplayPoints;
        CandyCheck.deliverdAllCandy -= CheckIfhasDeliverdAllCandys;
    }
    private void Start()
    {
        allCandy = GameObject.FindGameObjectsWithTag("Candy");
        pointsBox.text = $"Candy {points}/{allCandy.Length}";
    }
    private void Update()
    {
        if (!hasDeliverdAllCandys)
        {
            UpdateTimer();
        }
    }
    private void DisplayPoints(int addPoints)
    {
        points += addPoints;
        pointsBox.text = $"Candy {points}/{allCandy.Length}";
        sendCurrentPoints?.Invoke(points);
    }

    private void UpdateTimer()
    {
        time += Time.deltaTime;
        timerBox.text = time.ToString("F4");
    }
    private void CheckIfhasDeliverdAllCandys(bool _hasDeliverdAllCandys)
    {
        hasDeliverdAllCandys = _hasDeliverdAllCandys;
    }
}
