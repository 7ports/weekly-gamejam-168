using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{


    public Dialogue dialogue;

    public void TriggerDialogue(){
        FindObjectOfType<dialogueManager>().startDialogue(dialogue);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Q)){
            TriggerDialogue();
        }
    }
}
