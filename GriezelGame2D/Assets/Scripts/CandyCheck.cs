using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class CandyCheck : MonoBehaviour
{
    [SerializeField] private TMP_Text DialogueBox;
    [SerializeField] private int dialogueWait;
    [SerializeField] private string DialogueMessageWin;
    [SerializeField] private string DialogueMessageNoWin;

    private bool dialogueFinished = false;
    private int currentPoints;
    private GameObject[] allCandy;

    public static event Action<bool> deliverdAllCandy;

    private void OnEnable()
    {
        UIManager.sendCurrentPoints += GetCurrentPoints;
    }

    private void OnDisable()
    {
        UIManager.sendCurrentPoints -= GetCurrentPoints;
    }
    private void Start()
    {
        allCandy = GameObject.FindGameObjectsWithTag("Candy");
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (currentPoints >= allCandy.Length)
        {
            deliverdAllCandy?.Invoke(true);
            DialogueBox.text = DialogueMessageWin;
            yield return new WaitForSeconds(dialogueWait);
        }
        else if (currentPoints < allCandy.Length)
        {
            DialogueBox.text = DialogueMessageNoWin;
            yield return new WaitForSeconds(dialogueWait);
            DialogueBox.text = "";
        }
    }

    private void GetCurrentPoints(int _currentPoints)
    {
        currentPoints = _currentPoints;
    }
}
