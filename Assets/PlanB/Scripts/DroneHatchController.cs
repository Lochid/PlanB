using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHatchController : MonoBehaviour
{
    private AudioSource audioController;
    public DialogueSystem dialogue;
    private List<(float, string)> dialogueTexts = new List<(float, string)>()
    {
        (2, "I'm sure it can be opened"),
        (2.5f, "Shit")
    };

    private void Start()
    {
        audioController = GetComponent<AudioSource>();  
    }
    public void TryToOpen()
    {
        if(dialogue!=null)
        {
            dialogue.StartShowingText(new List<(float, string)>(dialogueTexts));
        }
        if(audioController!=null)
        {
            audioController.Play();
        }
    }
}
