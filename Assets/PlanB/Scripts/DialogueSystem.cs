using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text dialogueText;

    string nextText;
    float nextAwaiting;

    List<(float, string)> phraseQueue;

    void Start()
    {
        ClearTexts();
    }

    void ClearTexts()
    {
        dialogueText.text = "";
        nextText = "";
        nextAwaiting = 0;
    }

    void ShowNextText()
    {
        if (phraseQueue.Count > 0)
        {
            var (awaiting, phrase) = phraseQueue[0];
            Invoke("ShowText", nextAwaiting);
            phraseQueue.RemoveAt(0);
            nextText = phrase;
            nextAwaiting = awaiting;
        } else
        {
            Invoke("ClearTexts", nextAwaiting);
        }
    }

    void ShowText()
    {
        dialogueText.text = nextText;
        ShowNextText();
    }

    public void StartShowingText(List<(float, string)> phraseQueue)
    {
        ClearTexts();
        CancelInvoke();
        this.phraseQueue = phraseQueue;
        ShowNextText();
    }
}
