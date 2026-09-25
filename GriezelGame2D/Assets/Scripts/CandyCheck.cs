using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CandyCheck : MonoBehaviour
{
    [SerializeField] private TMP_Text DialogueBox;
    [SerializeField] private int dialogueWait;
    [SerializeField] private string DialogueMessageWin;
    [SerializeField] private string DialogueMessageNoWin;

    private int currentPoints;
    private GameObject[] allCandy;
    private Animator animator;

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
        animator = GetComponent<Animator>();
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (currentPoints >= allCandy.Length)
        {
            animator.Play("GhostAngryCandy");
            deliverdAllCandy?.Invoke(true);
            DialogueBox.text = DialogueMessageWin;
            yield return new WaitForSeconds(dialogueWait * 2);
            SceneManager.LoadScene("Outro");
        }
        else if (currentPoints < allCandy.Length)
        {
            animator.Play("GhostAngry");
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
