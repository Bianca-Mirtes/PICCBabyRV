using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueLine;
    [SerializeField] private Image character;
    [SerializeField] private TextMeshProUGUI characterName;

    [SerializeField] private Animator ani;

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    private bool dialogueIsActive = false;
    private float typingSpeed = 0.1f;
    public static DialogueManager _instance;

    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("DialogueManager");
                    _instance = singleton.AddComponent<DialogueManager>();
                    DontDestroyOnLoad(singleton);
                }
            }

            return _instance;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueIsActive = true;
        ani.Play("MomDialogueShow");
        lines.Clear();

        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            lines.Enqueue(line);
        }
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueLine currentLine = lines.Dequeue();

        character.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(StartTextLine(currentLine));
    }

    IEnumerator StartTextLine(DialogueLine currentLine)
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine currentLine)
    {
        dialogueLine.text = "";

        foreach (char letter in currentLine.text.ToCharArray())
        {
            dialogueLine.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        DisplayNextLine();
    }
    private void EndDialogue()
    {
        dialogueIsActive = false;
        Invoke("Hide", 3f);
    }

    private void Hide()
    {
        ani.Play("MomDialogueHide");
    }
}
