using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDetect : MonoBehaviour
{
    public GameObject InteractUI;
    public GameObject DialogueBox;
    public Dialogue dialogue;
    bool ready = false;

    void Update()
    {
        if (ready && Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueBox.activeSelf)
            {
                InteractUI.SetActive(false);
                TriggerDialogue();
            }
            else if (DialogueBox.activeSelf)
            {
                NextDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ready = true;
            InteractUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ready = false;
            InteractUI.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    public void NextDialogue()
    {
        FindAnyObjectByType<DialogueManager>().DisplayNextSentence();
    }
}
