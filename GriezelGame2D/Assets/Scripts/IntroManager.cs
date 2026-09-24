using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private TMP_Text ghostDialogueBox;
    [SerializeField] private int dialogueWait;
    [SerializeField] private string[] ghostDialogueMessage;

    private bool dialogueFinished = false;

    private void Start()
    {
        StartCoroutine(Dialogue());
    }
    private IEnumerator Dialogue()
    {
        for (int i = 0; i < ghostDialogueMessage.Length; i++)
        {
            ghostDialogueBox.text = ghostDialogueMessage[i];
            yield return new WaitForSeconds(dialogueWait);
            if (i == ghostDialogueMessage.Length - 1)
            {
                dialogueFinished = true;
            }
        }
    }

    private void Update()
    {
        if (dialogueFinished)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
