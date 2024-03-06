using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDetect : MonoBehaviour
{
    public GameObject InteractUI;
    public GameObject DialogueBox;
    public Dialogue dialogue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueBox.activeSelf)
            {
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
            InteractUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
