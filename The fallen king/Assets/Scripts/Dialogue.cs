using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    private bool playerInRange;
    private bool DidDialogueStart;
    private int LineIndex;
    private float TypingTime = 0.05f;
    [SerializeField] GameObject DialoguePanel;
    [SerializeField] TMP_Text DialogueText; 
    [SerializeField, TextArea(4,6)] private string[] DialogueLines;

    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Fire1"))
        {
            if(!DidDialogueStart)
            {
                StartDialogue();
            }
            else if (DialogueText.text == DialogueLines[LineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = DialogueLines[LineIndex];
            }
        }
    }

    private void NextDialogueLine()
    {
        LineIndex ++;
        if(LineIndex < DialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            DidDialogueStart = false;
            DialoguePanel.SetActive(false);
            Time.timeScale = 1f; 
        }
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;
        foreach (char ch in DialogueLines[LineIndex])
        {
            DialogueText.text += ch;
            yield return new WaitForSecondsRealtime(TypingTime);
        }
    }

    private void StartDialogue()
    {
        DidDialogueStart = true;
        DialoguePanel.SetActive(true);
        LineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player out of range");
        }
    }
}
