using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public Character character;
    public string text;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Trigger()
    {
        Invoke("StartDialogue", 2f);
    }
    private void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
